using Godot;
using System;

namespace AudioSystem;
public class AudioPlaybackManager
{
    RandomNumberGenerator rng = new RandomNumberGenerator();

    // Return an available and play-ready audio channel
    public AudioStreamPlayer2D ReturnAvailableChannel(int object_id)
    {
        Godot.Collections.Array<Godot.Node> channels = EnumGameObject.game_objects[object_id].GetNode<Godot.Node2D>("Audio").GetChildren(); // Get all the audio players
        int                                 channels_size = channels.Count-1;
        AudioStreamPlayer2D                 audio_stream_player;

        // Check all present audio channels for available ones
        for(int each = 0; each <= channels_size; each++)
        {
            AudioStreamPlayer2D channel = (AudioStreamPlayer2D) channels[each];
            if(!channel.Playing)
            {
                audio_stream_player = channel;
                GD.Print("audio_playback_manager : Returning available audio player");
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
        AudioStreamPlayer2D audio_stream_player = new AudioStreamPlayer2D();
        audio_stream_player.Name                = "AudioTmp";
        EnumGameObject.game_objects[object_id].AddChild(audio_stream_player);
        GD.Print("audio_playback_manager : All audio channels on use, creating a new one");
        return audio_stream_player;
    }

    // Plays audio
    public void PlayAudio(int object_id, int asset_id)
    {
        //GD.Print("audio_playback_manager : Playing audio | ID : " + asset_id + " | " + SoundApi.audio_list.all_audio[asset_id].file_name);
        AudioStreamPlayer2D audio_stream_player = ReturnAvailableChannel(object_id);
        
        if(audio_stream_player.Playing)
        {
            audio_stream_player      = new AudioStreamPlayer2D();
            audio_stream_player.Name = "AudioTmp";
            EnumGameObject.game_objects[object_id].AddChild(audio_stream_player);
        }
        audio_stream_player.Stream   = SoundApi.audio_list.all_audio[asset_id].stream;
        audio_stream_player.Bus      = "Master";
        audio_stream_player.Play();
    }

    // Plays audio by name
    public void PlayAudioByName(int object_id, string assetFileName)
    {
        
        AudioStreamPlayer2D audio_stream_player = ReturnAvailableChannel(object_id);
                        int asset_id           = SoundApi.audio_list_manager.SoundNameToId(assetFileName);
        GD.Print("audio_playback_manager : Playing audio | ID : " + asset_id + " | " + assetFileName);

        if(audio_stream_player.Playing)
        {
            audio_stream_player      = new AudioStreamPlayer2D();
            audio_stream_player.Name = "AudioTmp";
            EnumGameObject.game_objects[object_id].AddChild(audio_stream_player);
        }
        
        audio_stream_player.Stream   = SoundApi.audio_list.all_audio[asset_id].stream;
        audio_stream_player.Bus      = "Master";
        audio_stream_player.Play();
    }

    // Plays audio randomly from audio list
    public void PlayAudioRandomly(int object_id, string asset_list)
    {
        int list_length = SoundApi.audio_list_manager.GetListLength(asset_list);
        int chosen      = rng.RandiRange(0,list_length);
        int asset_id    = SoundApi.audio_list.all_audio_lists[asset_list][chosen].sound_id;

        GD.Print("audio_playback_manager : Playing audio randomly | ID : " + chosen + " | " + SoundApi.audio_list.all_audio[chosen].file_name);
        SoundApi.audio_playback_manager.PlayAudio(object_id, asset_id);
        
    }

    // Plays audio sequencially from a list
    public void PlayAudioInSequence(int object_id, string asset_list)
    {
        int list_length   = SoundApi.audio_list_manager.GetListLength(asset_list); 
        int current_index = CheckContinuity(object_id, asset_list); // Current sound index to play
    
        if(current_index  > list_length) // If it's the last one of the audio_list reset to 0
        {  
            current_index = 0;
        }

        int asset_id      = SoundApi.audio_list.all_audio_lists[asset_list][current_index].sound_id;
        int new_index     = current_index+1; // Next sound index to play

        GD.Print("audio_playback_manager : Playing audio in sequence | ID : ", asset_id, " | " + SoundApi.audio_list.all_audio[asset_id].file_name);
        PlayAudio(object_id, asset_id);
        UpdateContinuity(object_id, asset_list, new_index);
    }

    // Plays audio randomly from a list, avoiding the last sound played from it
    public void PlayAudioPseudoRandomly(int object_id, string asset_list)
    {
        int list_length   = SoundApi.audio_list_manager.GetListLength(asset_list);
        int current_index = CheckContinuity(object_id, asset_list);
        int chosen        = rng.RandiRange(0, list_length);
        while(chosen      == current_index)
        {chosen           = rng.RandiRange(0, list_length);}
        int asset_id      = SoundApi.audio_list.all_audio_lists[asset_list][chosen].sound_id;

        GD.Print("audio_playback_manager : Playing audio in pseudo-sequence | ID : " + chosen + " | " + SoundApi.audio_list.all_audio[chosen].file_name);
        PlayAudio(object_id, asset_id);
        UpdateContinuity(object_id, asset_list, chosen);
    }

    // Acknowledges sound difference for different materials
    public string SpecifyMaterial(string sound_file_name, int asset_material_id)
    {
        // Difference between materials must be in filename at the end for this to work
        switch(asset_material_id) 
        {
            case 0:
                sound_file_name += "Rock";
                return sound_file_name;
            case 1:
                sound_file_name += "Wall";
                return sound_file_name;
            case 2:
                sound_file_name += "Metal";
                return sound_file_name;
            default:
                return sound_file_name;
        }
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
    public int CheckContinuity(int object_id, string action)
    {
        var soundContinuity = EnumGameObject.game_objects[object_id].continuity_manager;
        int playIndex;
        if(soundContinuity.storedIndex.ContainsKey(action))
        {
            playIndex = soundContinuity.storedIndex[action];
        }
        else
        {
            // If there's no continuity recorded, create a list to keep track of it
            soundContinuity.storedIndex.Add(action, 0);
            playIndex = 0;
            GD.Print("Continuity audio_list created for " + action);
        }
        return playIndex;
    }

    // Updates the continuity of audio list
    public void UpdateContinuity(int object_id, string action, int newIndex)
    {
        var soundContinuity = EnumGameObject.game_objects[object_id].continuity_manager;
        soundContinuity.storedIndex[action] = newIndex;
    }
}
