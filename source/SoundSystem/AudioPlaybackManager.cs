using Godot;
using System;
using System.Collections.Generic;
using SoundApi;

namespace AudioSystem;
public class AudioPlaybackManager
{
    RandomNumberGenerator rng = new RandomNumberGenerator();    // Randomness purpouse
    List<Timer> timers = new List<Timer>();                     // Stores timers for sequence of sounds

    // Return an available and play-ready audio channel
    public AudioStreamPlayer2D ReturnAvailableChannel(int agent_id)
    {
        Godot.Collections.Array<Godot.Node> channels = Audio.audio_emitter_manager.audio_emitters[agent_id].GetNode<Godot.Node2D>("Audio").GetChildren(); // Get all the audio players
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
        return CreateNewChannel(agent_id);
    }

    // Create a new audio channel
    public AudioStreamPlayer2D CreateNewChannel(int agent_id)
    {
        //GD.Print("audio_playback_manager : All audio channels on use, creating a new one");
        AudioStreamPlayer2D audio_stream_player = new AudioStreamPlayer2D(); // TODO: Make Player3D
        audio_stream_player.Name                = "AudioTmp";
        Audio.audio_emitter_manager.audio_emitters[agent_id].GetChild(1).AddChild(audio_stream_player); // Audio Folder node must be of index 1 for the timer to be placed on the right spot
        audio_stream_player.Finished += () => DeleteChannel(audio_stream_player);
        return audio_stream_player;
    }

    // Automatically delete the channel after being used
    private void DeleteChannel(AudioStreamPlayer2D channel)
    {   
        channel.QueueFree();
    }

    // Plays audio
    public void PlayAudio(int asset_id, int agent_id)
    {
        AudioStreamPlayer2D audio_stream_player = ReturnAvailableChannel(agent_id);
        audio_stream_player.Stream     = Audio.audio_list.all_audio[asset_id].stream;
        audio_stream_player.Bus        = "Master";
        audio_stream_player.PitchScale = 1.0f; // Reset pitch of audio player in case it was altered before
        audio_stream_player.Play();
        //GD.Print("audio_playback_manager : Playing audio | ID : " + asset_id + " | " + Audio.audio_list.all_audio[asset_id].file_name);
    }

    // Plays audio by name
    public void PlayAudioByName(string assetFileName, int agent_id)
    { 
        AudioStreamPlayer2D audio_stream_player = ReturnAvailableChannel(agent_id);
        int asset_id                            = Audio.audio_list_manager.SoundNameToId(assetFileName);
        audio_stream_player.Stream   = Audio.audio_list.all_audio[asset_id].stream;
        audio_stream_player.Bus      = "Master";
        audio_stream_player.Play();
        //GD.Print("audio_playback_manager : Playing audio | ID : " + asset_id + " | " + assetFileName);
    }

    // Plays audio sequencially from a list
    public void PlayAudioInSequence(string list_id, int agent_id)
    {
        int list_length   = Audio.audio_list_manager.GetListLength(list_id);
        int current_index = CheckContinuity(list_id, agent_id);                       // Current sound index to play
        if(current_index  >= list_length)                                             // If it's the last index of the audio_list reset to 0
        { current_index = 0; }
        int sound_id   = Audio.audio_list.all_audio_lists[list_id][current_index];    // ID of list  -> Index of sound to play
        int new_index     = current_index+1;                                          // Next sound index to play
        PlayAudio(sound_id, agent_id);
        UpdateContinuity(list_id, agent_id, new_index);
        GD.Print("audio_playback_manager : Playing audio in sequence | ID : ", sound_id, " | " + Audio.audio_list.all_audio[sound_id].file_name);
    }

    // Plays audio randomly from a list, avoiding the last sound played from it
    // All "Play" methods are quite the same with differences
    public void PlayAudioPseudoRandomly(string list_id, int agent_id)
    {
        int list_length   = Audio.audio_list_manager.GetListLength(list_id);
        int current_index = CheckContinuity(list_id, agent_id);
        int chosen        = rng.RandiRange(0, list_length);
        while(chosen      == current_index)
        {chosen           = rng.RandiRange(0, list_length);}
        int sound_id   = Audio.audio_list.all_audio_lists[list_id][chosen];
        GD.Print("audio_playback_manager : Playing audio in pseudo-sequence | ID : " + chosen + " | " + Audio.audio_list.all_audio[chosen].file_name);
        PlayAudio(agent_id, sound_id);
        UpdateContinuity(list_id, agent_id, chosen);
    }

    // Plays audio randomly from audio list
    public void PlayAudioRandomly(string list_id, int agent_id)
    {
        int list_length   = Audio.audio_list_manager.GetListLength(list_id);
        int current_index = rng.RandiRange(0,list_length);
        int sound_id      = Audio.audio_list.all_audio_lists[list_id][current_index]; // Integer ID of list  -> Integer ID of sound on index
       GD.Print("audio_playback_manager : Playing audio randomly | ID : " + current_index + " | " + Audio.audio_list.all_audio[current_index].file_name);
        Audio.audio_playback_manager.PlayAudio(agent_id, sound_id);
    }

    // Plays sound with pitch alteration
    public void PlayInPitch(int pitch, int asset_id, int agent_id, int pitch_increment = 1, string is_list = null)
    {
        AudioStreamPlayer2D audio_stream_player = ReturnAvailableChannel(agent_id);            // Give me an available channel
        audio_stream_player.Stream              = Audio.audio_list.all_audio[asset_id].stream; // Load it with the sound
        audio_stream_player.Bus                 = "Master";                                    // Assign it to master channel
        String pitch_id;
        if(is_list == null) // If is a single sound
        {
            pitch_id = asset_id + "1"; // Create a custom ID for the single sound in continuity manager 
        }
        else                // If is a list
        {
            pitch_id = is_list + "1";  // Create a custom ID for the whole list in continuity manager
        }
        float new_pitch                         = (float) CheckContinuity(pitch_id, agent_id); // Check what was last pitch played for that sound
        new_pitch = Math.Clamp(new_pitch, 1.0f, 12.0f);
        audio_stream_player.PitchScale          = new_pitch;                                   // Set pitch based on last pitch played
        GD.Print("audio_playback_manager : Pitch : " + new_pitch);
        new_pitch += (float) pitch_increment;                                                  // Update pitch with increment
        UpdateContinuity(pitch_id, agent_id, (int) new_pitch);                                 // And store it
        audio_stream_player.Play();                                                            // Finally play
    }

    // Plays a sound sequence in pitch
    public void PlaySequenceInPitch(int pitch, string list_id, int agent_id, int pitch_increment = 1)
    {
        int list_length   = Audio.audio_list_manager.GetListLength(list_id);
        int current_index = CheckContinuity(list_id, agent_id);                       // Current sound index to play
        if(current_index  >= list_length)                                             // If it's the last index of the audio_list reset to 0
        { current_index = 0; }
        int sound_id   = Audio.audio_list.all_audio_lists[list_id][current_index];    // ID of list  -> ID of sound
        int new_index     = current_index+1;                                          // Next sound index to play
        GD.Print("audio_playback_manager : Playing audio in pitch sequence | ID : " + sound_id + " | " + Audio.audio_list.all_audio[sound_id].file_name);
        PlayInPitch(pitch, sound_id, agent_id, pitch_increment, list_id);
        UpdateContinuity(list_id, agent_id, new_index);
    }

    // Atenuattes the sound if is ocluded
    public void IsOccluded()
    {
    }

    // Check the continuity of audio list to have the right play index
    public int CheckContinuity(string list_id, int agent_id)
    {
        var sound_continuity = Audio.audio_emitter_manager.audio_data[agent_id].sound_specifics.continuity_manager;
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
            //GD.Print("audio_playback_manager : Continuity audio_list created for " + list_id);
        }
        return play_index;
    }

    // Updates the continuity of audio list
    public void UpdateContinuity(string list_id, int agent_id, int new_index)
    {
        var sound_continuity = Audio.audio_emitter_manager.audio_data[agent_id].sound_specifics.continuity_manager;
        sound_continuity[list_id] = new_index;
    }

    // Creates a timer in the game object and calls function_to_call when timeouts, returns timer ID
    public int CreateTimer(Action function_to_call, int agent_id, bool one_shoot = true)
    {
        Timer   new_timer;
        int     new_timer_id;
        new_timer = new Timer();                                // Create timer
        new_timer.OneShot = one_shoot;                          
        timers.Add(new_timer);                                  // Add it to timer's list
        new_timer_id = timers.Count;                                        
        Audio.audio_emitter_manager.audio_emitters[agent_id].AddChild(new_timer);   // Add it to object
        new_timer.Timeout += () => function_to_call();                              // Connect it to custom trigger
        if(one_shoot)                                                               // If is one shot, delete after use
        {
            new_timer.Timeout += () => DeleteTimer(new_timer_id);
        }
        return new_timer_id;
    }

    public int CreateTimerWithSoundSpecifics(Action<SoundSpecifics> function_to_call, SoundSpecifics sound_specifics)
    {
        Timer new_timer;
        new_timer = new Timer();                                                                    // Create timer
        Audio.audio_emitter_manager.audio_emitters[sound_specifics.agent_id].AddChild(new_timer);   // Add it to object
        new_timer.Timeout += () => function_to_call(sound_specifics);                               // Connect it to custom trigger
        timers.Add(new_timer);                                                                      // Add it to timer's list
        return timers.Count;
    }

    // Starts the timer with the given time
    public void StartTimer(int timer_id, float time)
    {
        timer_id -= 1; // This fixed an index error for me
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

    public void DeleteTimer(int timer_id)
    {
        timers[timer_id-1].QueueFree(); 
    }

}
