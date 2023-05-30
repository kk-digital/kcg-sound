using System.Collections.Generic;
using Godot;
//using Player;
using System;

namespace AudioSystem;
public class AudioAssetManager
{
    // First initialization to register all audio & lists and load them
    public void Init()
    {
        GD.Print("audio_asset_manager : Registering sounds");
        InitializeAudioDirectory("res://assets/test_audio_assets");

        List<int> test_list = new List<int>();
        test_list.Add(RegisterSoundAsset("res://assets/test_audio_assets/CoreMechStart.ogg", "CoreMech", "core_mech.png"));
        test_list.Add(RegisterSoundAsset("res://assets/test_audio_assets/CoreShotStart.ogg", "CoreShock", "core_shock.png"));
        test_list.Add(RegisterSoundAsset("res://assets/test_audio_assets/CoreBass.ogg", "CoreBass", "core_bass.png"));
        RegisterSoundAssetList("Test", test_list);

        GD.Print("audio_asset_manager : Registered sounds succesfully");

        LoadAllSoundAssets();
    }

    // Makes lists from subfolders and registers all audio in res://assets/test_audio_assets
    public void InitializeAudioDirectory(string path) 
    {
        GD.Print("audio_asset_manager : Initializing directory");
        using var dir           = DirAccess.Open(path);
        dir.IncludeHidden       = false;
        dir.IncludeNavigational = false;

        if (dir == null)
        { return; }

        dir.ListDirBegin();
        string file_name = dir.GetNext();
        while (file_name != "")
        {
            if (dir.CurrentIsDir())
            {
                string directory_path = path + "/" + file_name;
                List<int> sound_ids = new List<int>();
                foreach(var file in DirAccess.GetFilesAt(directory_path))
                {
                    if(file.GetExtension() == "ogg")
                    {
                    string asset_path = path + "/" + file_name + "/" + file;
                    string asset_name = file.GetBaseName();
                    sound_ids.Add(RegisterSoundAsset(asset_path, asset_name, "X"));
                    }
                }
                RegisterSoundAssetList(file_name, sound_ids);
            }
            file_name = dir.GetNext();
        }
    }

    // Add audio asset to all_audio list
    public void AddAudioAsset(AudioAsset asset)
    { 
        SoundApi.audio_list.all_audio.Add(asset); 
    }

    // Remove audio asset from all_audio list
    public void RemoveAudioAsset(AudioAsset asset)
    { 
        SoundApi.audio_list.all_audio.Remove(asset); 
    }

    // Regsiters the sound and assigns an ID to later access it in all_audio list
    public int RegisterSoundAsset(string path, string name, string icon_path)
    {
        AudioAsset asset    = new AudioAsset();
        asset.file_path      = path;
        asset.file_name      = name;
        //asset.icon        = ResourceLoader.Load(icon_path) as Texture; // Works, enable when textures are ready
        asset.is_loaded      = false;
        AddAudioAsset(asset);
        asset.sound_id       = SoundApi.audio_list.all_audio.Count - 1; // For the index to start at 0
        //GD.Print("audio_asset_manager : Registered : " + name + " | ID : " + asset.sound_id + " | Filepath : " + path);
        return asset.sound_id;
    }

    // Packs sound IDs into a list to later access it in all_audio_lists with a string
    public void RegisterSoundAssetList(string list_name, List<int> asset_ids)
    {
        List<AudioAsset> new_sound_list = new List<AudioAsset>();
        for (int asset_id = 0; asset_id <= (asset_ids.Count - 1); asset_id++)
        {
            new_sound_list.Add(SoundApi.audio_list.all_audio[asset_id]);
        }

        SoundApi.audio_list.all_audio_lists.Add(list_name, new_sound_list);
        GD.Print("audio_asset_manager : New audio_list created (" + list_name + ") of " + new_sound_list.Count + " size");
    }

    // Loads all sounds asset for them to be playable
    public void LoadAllSoundAssets()
    {
        GD.Print("audio_asset_manager : Loading all sound assets");
        for(int x = 0; x < SoundApi.audio_list.all_audio.Count; x++)
        {
            if (SoundApi.audio_list.all_audio[x].is_loaded == false)
            {
                LoadSoundStream(x);
            }
        }
    }

    // Assigns a stream of audio to the sound asset
    public void LoadSoundStream(int asset_id)
    {
        var audio_stream_test = new AudioStreamOggVorbis();
        audio_stream_test     = GD.Load(SoundApi.audio_list_manager.GetAssetFilePathFromId(asset_id)) as AudioStreamOggVorbis;

        SoundApi.audio_list.all_audio[asset_id].stream   = audio_stream_test;
        SoundApi.audio_list.all_audio[asset_id].is_loaded = true;
    }

    /*
    public void CheckAudioList()
    {
        GD.Print("All Audio : ");
        foreach(AudioAsset i in SoundApi.audio_list.all_audio)
        {
            GD.Print(i + " / Total capactiy" + SoundApi.audio_list.all_audio.Count);
        }
    }
    */
}

