using System.Collections.Generic;
using Godot;
using System;

namespace AudioSystem;
public class AudioListManager
{
    public int GetListLength(string listName)
    {
        return SoundApi.list.allAudioLists[listName].Count-1;
    }

    public AudioAsset GetAssetFromId(int Id)
    {
        return SoundApi.list.allAudio[Id];
    }

    public string GetAssetFilePathFromId(int Id)
    {
        return SoundApi.list.allAudio[Id].FilePath;
    }

    public int SoundNameToId(string name) // Computationally expensive
    {            
        foreach (AudioAsset audioAsset in SoundApi.list.allAudio)
        {
            if (audioAsset.FileName == name)
            {
                return audioAsset.SoundID;
            }
        }
        return 0;
    }
}
