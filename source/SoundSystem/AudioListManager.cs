using System.Collections.Generic;
using Godot;
using System;

namespace AudioSystem;
public class AudioListManager
{
    public int GetListLength()
    {
        return SoundApi.list.Count;
    }

    public AudioAsset GetAssetFromId(int Id)
    {
        return SoundApi.list[Id];
    }

    public string GetAssetFilePathFromId(int Id)
    {
        return SoundApi.list[Id].FilePath;
    }

    public int SoundNameToId(string name)
    {            
        foreach (AudioAsset i in SoundApi.list)
        {
            if (i.FileName == name)
            {
                return i.SoundID;
            }
        }
        return 0;
    }
}
