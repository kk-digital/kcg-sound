using System.Collections.Generic;
using Godot;
//using Player;
using System;

namespace AudioSystem;
public class AudioAssetManager
{
    public void Init()
    {
        GD.Print("Registering sounds"); // Debug purpose

        RegisterSoundAsset("res://assets/test_audio_assets/CoreMechStart.ogg", "Core_Mech", "core_mech.png");
        RegisterSoundAsset("res://assets/test_audio_assets/CoreShotStart.ogg", "Core_Shock", "core_shock.png");
        RegisterSoundAsset("res://assets/test_audio_assets/CoreBass.ogg", "Core_Bass", "core_bass.png");
        RegisterSoundAsset("res://assets/test_audio_assets/Atmo.ogg", "Atmo", "atmo.png");
        RegisterSoundAsset("res://assets/test_audio_assets/Reflection.ogg", "Reflection", "reflection.png");
        RegisterSoundAsset("res://assets/test_audio_assets/CoreShot.ogg", "Core_Shot", "reflection.png");

        GD.Print("Registered sounds succesfully"); // Debug purpose
        LoadSoundAssets();
    }

    public int RegisterSoundAsset(string path, string name, string iconPath)
    {
        AudioAsset asset = new AudioAsset();
        asset.FilePath = path;
        asset.FileName = name;
        //asset.Icon =  ResourceLoader.Load(iconPath) as Texture; // Works, enable when textures are ready
        asset.IsLoaded = false;
        AddAsset(asset);
        asset.SoundID = SoundApi.list.Capacity;

        return asset.SoundID;
    }

    public void AddAsset(AudioAsset asset)
    {
        SoundApi.list.Add(asset);
    }

    public void RemoveAsset(AudioAsset asset)
    {
        SoundApi.list.Remove(asset);
    }

    public void LoadSoundAssets()
    {
        GD.Print("Loading all sound assets"); // Debug purpose
        for(int x = 0; x < SoundApi.list.Count; x++)
        {
            if (SoundApi.list[x].IsLoaded == false)
            {
                LoadAudioAsset(x);
            }
        }
    }

    public void LoadAudioAsset(int assetId)
    {
        var audioStreamTest = new AudioStreamOggVorbis();
        audioStreamTest = GD.Load(SoundApi.listManager.GetAssetFilePathFromId(assetId)) as AudioStreamOggVorbis;

        SoundApi.list[assetId].Stream = audioStreamTest;
        SoundApi.list[assetId].IsLoaded = true;
        GD.Print("Sucesfully loaded sound " + SoundApi.list[assetId].FileName); // Debug purpose
    }

    /*
    public void Update(GameState gameState, int timeTick, float timeReal)
    {
    }
    */
}

