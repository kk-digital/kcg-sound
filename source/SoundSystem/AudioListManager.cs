using System.Collections.Generic;
using Godot;
using System;

namespace AudioSystem;
public class AudioListManager
{
    // Returns the given list length minus one to ensure no bad index error
    public int GetListLength(string list_name)
    {
        return SoundApi.audio_list.all_audio_lists[list_name].Count-1;
    }

    // Returns audio asset from ID
    public AudioAsset GetAssetFromId(int id)
    {
        return SoundApi.audio_list.all_audio[id];
    }

    // Returns audio asset file path from ID
    public string GetAssetFilePathFromId(int Id)
    {
        return SoundApi.audio_list.all_audio[Id].file_path;
    }

    // Returns audio name from ID
    public int SoundNameToId(string name)
    {            
        foreach (AudioAsset audio_asset in SoundApi.audio_list.all_audio)
        {
            if (audio_asset.file_name == name)
            {
                return audio_asset.sound_id;
            }
        }
        GD.Print("list_manager : ERROR -> 'SoundNameToId' | Sound not found");
        return 0;
    }
}
