using System.Collections.Generic;
using Godot;
//using Player;
using System;

namespace AudioSystem;
public class AudioAssetManager
{
    public void Init()
    {
        GD.Print("// Registering sounds");
        InitializeAudioDirectory("res://assets/test_audio_assets");
    
        List<int> test = new List<int>();
        test.Add(RegisterSoundAsset("res://assets/test_audio_assets/CoreMechStart.ogg", "CoreMech", "core_mech.png"));
        test.Add(RegisterSoundAsset("res://assets/test_audio_assets/CoreShotStart.ogg", "CoreShock", "core_shock.png"));
        test.Add(RegisterSoundAsset("res://assets/test_audio_assets/CoreBass.ogg", "CoreBass", "core_bass.png"));
        RegisterSoundAssetList("Test", test);

        /*
        RegisterSoundAsset("res://assets/test_audio_assets/CoreMechStart.ogg", "CoreMech", "core_mech.png");
        RegisterSoundAsset("res://assets/test_audio_assets/CoreShotStart.ogg", "CoreShock", "core_shock.png");
        RegisterSoundAsset("res://assets/test_audio_assets/CoreBass.ogg", "CoreBass", "core_bass.png");
        RegisterSoundAsset("res://assets/test_audio_assets/Atmo.ogg", "Atmo", "atmo.png");
        RegisterSoundAsset("res://assets/test_audio_assets/Reflection.ogg", "Reflection", "reflection.png");
        RegisterSoundAsset("res://assets/test_audio_assets/CoreShot.ogg", "CoreShot", "reflection.png");
        */
        GD.Print("// Registered sounds succesfully");

        LoadAllSoundAssets();
    }

    public void InitializeAudioDirectory(string path) // Makes lists and registers all audio in test_audio_assets
    {
        using var dir = DirAccess.Open(path);
        dir.IncludeHidden = false;
        dir.IncludeNavigational = false;

        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (dir.CurrentIsDir())
                {
                    string directoryPath = path + "/" + fileName;
                    //GD.Print($"Directory to list: {fileName}");
                    //GD.Print($"Currently on : " + directoryPath);
                    List<int> soundIds = new List<int>();
                    foreach(var file in DirAccess.GetFilesAt(directoryPath))
                    {
                       if(file.GetExtension() == "ogg")
                        {
                            //GD.Print("Names : " + assetNames.Length + " / Directories : " + assetDirectories.Length);
                            string assetPath = path + "/" + fileName + "/" + file;
                            string assetName      = file.GetBaseName();
                            //GD.Print("Name : " + assetName + " / Directory : " + assetPath);
                            soundIds.Add(RegisterSoundAsset(assetPath, assetName, "X"));
                        }
                    }
                    RegisterSoundAssetList(fileName, soundIds);
                }
                else
                {
                    //GD.Print($"Audio File: {fileName}");
                }
                fileName = dir.GetNext();
            }
        }
        else
        {
            GD.Print("# An error occurred when trying to access the path.");
        }
    }
    
    public void FolderToList()
    {
        
    }

    public void AddAsset(AudioAsset asset)
    { SoundApi.list.allAudio.Add(asset); }

    public void RemoveAsset(AudioAsset asset)
    { SoundApi.list.allAudio.Remove(asset); }

    public int RegisterSoundAsset(string path, string name, string iconPath)
    {
        AudioAsset asset    = new AudioAsset();
        asset.FilePath      = path;
        asset.FileName      = name;
        //asset.Icon        = ResourceLoader.Load(iconPath) as Texture; // Works, enable when textures are ready
        asset.IsLoaded      = false;
        AddAsset(asset);
        asset.SoundID       = SoundApi.list.allAudio.Count - 1; // For the index to start at 0
        GD.Print("Registered : " + name + " with ID " + asset.SoundID + ", Filepath : " + path);
        return asset.SoundID;
    }

    public void RegisterSoundAssetList(string listName, List<int> assetIds)
    {
        List<AudioAsset> newSoundList = new List<AudioAsset>();
        for (int assetId = 0; assetId <= (assetIds.Count - 1); assetId++)
        {
            newSoundList.Add(SoundApi.list.allAudio[assetId]);
        }

        SoundApi.list.allAudioLists.Add(listName, newSoundList);
        GD.Print("// New list created (" + listName + ") of " + newSoundList.Count + " size");
    }

    public void LoadAllSoundAssets()
    {
        GD.Print("//  Loading all sound assets");
        for(int x = 0; x < SoundApi.list.allAudio.Count; x++)
        {
            if (SoundApi.list.allAudio[x].IsLoaded == false)
            {
                LoadSoundStream(x);
            }
        }
    }

    public void LoadSoundStream(int assetId)
    {
        var audioStreamTest = new AudioStreamOggVorbis();
        audioStreamTest     = GD.Load(SoundApi.listManager.GetAssetFilePathFromId(assetId)) as AudioStreamOggVorbis;

        SoundApi.list.allAudio[assetId].Stream   = audioStreamTest;
        SoundApi.list.allAudio[assetId].IsLoaded = true;
        GD.Print("Sucesfully loaded sound " + SoundApi.list.allAudio[assetId].FileName);
    }

    /*
    public void CheckAudioList()
    {
        GD.Print("All Audio : ");
        foreach(AudioAsset i in SoundApi.list.allAudio)
        {
            GD.Print(i + " / Total capactiy" + SoundApi.list.allAudio.Count);
        }
    }
    */
}

