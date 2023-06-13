using Godot;
using System;
using System.Collections.Generic;

namespace AudioSystem;
public class AudioPlaybackManager
{
    RandomNumberGenerator rng = new RandomNumberGenerator();    // Randomness purpouse
    List<Timer> timers = new List<Timer>();                     // Stores timers for sequence of sounds

    // Return an available and play-ready audio channel
    public AudioStreamPlayer2D ReturnAvailableChannel(int object_id)
    {
        Godot.Collections.Array<Godot.Node> channels = SoundApi.audio_emitter_manager.audio_emitters[object_id].GetNode<Godot.Node2D>("Audio").GetChildren(); // Get all the audio players
        int                                 channels_size = channels.Count-1;
        AudioStreamPlayer2D                 audio_stream_player;

        // Check all present audio channels for available ones
        for(int each = 0; each <= channels_size; each++)
        {
            AudioStreamPlayer2D channel = (AudioStreamPlayer2D) channels[each];
            if(!channel.Playing)
            {
                audio_stream_player = channel;
                //GD.Print("audio_playback_manager : Returning available audio player");
                return audio_stream_player;
            }
        }

        // If there's no available ones, create a new temporal channel
        return CreateNewChannel(object_id);
    }

    // Create a new audio channel
    public AudioStreamPlayer2D CreateNewChannel(int object_id)
    {
        // Need to make the temporal part work yet
        AudioStreamPlayer2D audio_stream_player = new AudioStreamPlayer2D(); // TO DO: Make Player3D
        audio_stream_player.Name                = "AudioTmp";
        SoundApi.audio_emitter_manager.audio_emitters[object_id].AddChild(audio_stream_player);
        //GD.Print("audio_playback_manager : All audio channels on use, creating a new one");
        audio_stream_player.Finished += () => DeleteChannel(audio_stream_player);
        return audio_stream_player;
    }

    // Automatically delete the channel after being used
    private void DeleteChannel(AudioStreamPlayer2D channel)
    {   
        channel.QueueFree();
    }

    // Plays audio
    public void PlayAudio(int asset_id, int object_id)
    {
        //GD.Print("audio_playback_manager : Playing audio | ID : " + asset_id + " | " + SoundApi.audio_list.all_audio[asset_id].file_name);
        AudioStreamPlayer2D audio_stream_player = ReturnAvailableChannel(object_id);
        
        if(audio_stream_player.Playing)
        {
            audio_stream_player      = new AudioStreamPlayer2D();
            audio_stream_player.Name = "AudioTmp";
            SoundApi.audio_emitter_manager.audio_emitters[object_id].AddChild(audio_stream_player);
        }
        audio_stream_player.Stream   = SoundApi.audio_list.all_audio[asset_id].stream;
        audio_stream_player.Bus      = "Master";
        audio_stream_player.Play();
    }

    // Plays audio by name
    public void PlayAudioByName(string assetFileName, int object_id)
    {
        
        AudioStreamPlayer2D audio_stream_player = ReturnAvailableChannel(object_id);
                        int asset_id            = SoundApi.audio_list_manager.SoundNameToId(assetFileName);
        GD.Print("audio_playback_manager : Playing audio | ID : " + asset_id + " | " + assetFileName);

        if(audio_stream_player.Playing)
        {
            audio_stream_player      = new AudioStreamPlayer2D();
            audio_stream_player.Name = "AudioTmp";
            SoundApi.audio_emitter_manager.audio_emitters[object_id].AddChild(audio_stream_player);
        }
        
        audio_stream_player.Stream   = SoundApi.audio_list.all_audio[asset_id].stream;
        audio_stream_player.Bus      = "Master";
        audio_stream_player.Play();
    }

    // Plays audio sequencially from a list
    public void PlayAudioInSequence(string list_id, int object_id)
    {
        //GD.Print("Object ID : " + object_id + " | List ID : " + list_id);
        int list_length   = SoundApi.audio_list_manager.GetListLength(list_id);
        int current_index = CheckContinuity(list_id, object_id); // Current sound index to play
        if(current_index  >= list_length) // If it's the last index of the audio_list reset to 0
        { current_index = 0; }
        int asset_id      = SoundApi.audio_list.all_audio_lists[list_id][current_index].sound_id; // Integer ID of list  -> Integer ID of sound
        int new_index     = current_index+1; // Next sound index to play

        //GD.Print("audio_playback_manager : Playing audio in sequence | ID : ", asset_id, " | " + SoundApi.audio_list.all_audio[asset_id].file_name);
        PlayAudio(asset_id, object_id);
        UpdateContinuity(list_id, object_id, new_index);
    }

    // Plays audio randomly from a list, avoiding the last sound played from it
    public void PlayAudioPseudoRandomly(string list_id, int object_id)
    {
        int list_length   = SoundApi.audio_list_manager.GetListLength(list_id);
        int current_index = CheckContinuity(list_id, object_id);
        int chosen        = rng.RandiRange(0, list_length);
        while(chosen      == current_index)
        {chosen           = rng.RandiRange(0, list_length);}
        int asset_id      = SoundApi.audio_list.all_audio_lists[list_id][chosen].sound_id; // Integer ID of list  -> Integer ID of sound
        
        GD.Print("audio_playback_manager : Playing audio in pseudo-sequence | ID : " + chosen + " | " + SoundApi.audio_list.all_audio[chosen].file_name);
        PlayAudio(object_id, asset_id);
        UpdateContinuity(list_id, object_id, chosen);
    }

    // Plays audio randomly from audio list
    public void PlayAudioRandomly(string list_id, int object_id)
    {
        int list_length = SoundApi.audio_list_manager.GetListLength(list_id);
        int chosen      = rng.RandiRange(0,list_length);
        int asset_id    = SoundApi.audio_list.all_audio_lists[list_id][chosen].sound_id; // Integer ID of list  -> Integer ID of sound

       //GD.Print("audio_playback_manager : Playing audio randomly | ID : " + chosen + " | " + SoundApi.audio_list.all_audio[chosen].file_name);
        SoundApi.audio_playback_manager.PlayAudio(object_id, asset_id);
    }

    // Plays sound with pitch alteration
    public void PlayInPitch()
    {
    }

    // Atenuattes the sound if is ocluded
    public void IsOccluded()
    {
    }

    // Check the continuity of audio list to have the right play index
    public int CheckContinuity(string list_id, int agent_id)
    {
        var sound_continuity = SoundApi.audio_emitter_manager.audio_data[agent_id].sound_specifics.continuity_manager;
        int play_index;
        if(sound_continuity.ContainsKey(list_id))
        {
            play_index = sound_continuity[list_id];
        }
        else
        {
            // If there's no continuity recorded, create a list to keep track of it
            sound_continuity.Add(list_id, 0);
            play_index = 0;
            //GD.Print("Continuity audio_list created for " + list_id);
        }
        return play_index;
    }

    // Updates the continuity of audio list
    public void UpdateContinuity(string list_id, int agent_id, int new_index)
    {
        var sound_continuity = SoundApi.audio_emitter_manager.audio_data[agent_id].sound_specifics.continuity_manager;
        sound_continuity[list_id] = new_index;
    }

    // Creates a timer in the game object and calls function_to_call when timeouts, returns timer ID
    public int CreateTimer(Action function_to_call, int agent_id, bool one_shoot = true)
    {
        Timer new_timer;
        new_timer = new Timer();     
        new_timer.OneShot = one_shoot;                                                           // Create timer
        SoundApi.audio_emitter_manager.audio_emitters[agent_id].AddChild(new_timer); // Add it to object
        new_timer.Timeout += () => function_to_call();                                // Connect it to custom trigger
        timers.Add(new_timer);                                                                  // Add it to timer's list
        return timers.Count;
    }

    public int CreateTimerWithSoundSpecifics(Action<SoundSpecifics> function_to_call, SoundSpecifics sound_specifics)
    {
        Timer new_timer;
        new_timer = new Timer();                                                                // Create timer
        SoundApi.audio_emitter_manager.audio_emitters[sound_specifics.agent_id].AddChild(new_timer); // Add it to object
        new_timer.Timeout += () => function_to_call(sound_specifics);                                // Connect it to custom trigger
        timers.Add(new_timer);                                                                  // Add it to timer's list
        return timers.Count;
    }
    

    // Starts the timer with the given time
    public void StartTimer(int timer_id, float time)
    {
        timer_id -= 1;
        GD.Print("Start timer ID : ", timer_id);
        if(timers[timer_id].IsStopped())
        {
            timers[timer_id].Start(time);
        }
    }

    // Stops all timers
    public void StopAllTimers()
    {
        foreach(Timer timer in timers)
        {
            timer.Stop();
        }
    }

}
