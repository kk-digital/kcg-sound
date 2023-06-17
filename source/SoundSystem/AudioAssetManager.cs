using Godot;
using System;
using System.Collections.Generic;
using SoundApi;

/*
This script manages all loading & registering
*/

namespace AudioSystem;
public class AudioAssetManager
{
    // First initialization to register all audio & lists and load them
    public void Init()
    {
        GD.Print("audio_asset_manager : Registering sounds");
        GD.Print("audio_asset_manager : List Count : " + Audio.audio_list.all_audio.Count);
        InitializeAudioDirectory("res://assets/test_audio_assets");

        // Manually creating a list
        /* 
        List<int> test_list = new List<int>();
        test_list.Add(RegisterSoundAsset("res://assets/test_audio_assets/CoreMechStart.ogg", "CoreMech", "core_mech.png"));
        test_list.Add(RegisterSoundAsset("res://assets/test_audio_assets/CoreShotStart.ogg", "CoreShock", "core_shock.png"));
        test_list.Add(RegisterSoundAsset("res://assets/test_audio_assets/CoreBass.ogg", "CoreBass", "core_bass.png"));
        RegisterSoundAssetList(0, test_list, "Test");
        */

        GD.Print("audio_asset_manager : Registered sounds succesfully");
        LoadAllSoundAssets();
        PrintAllLists();
    }

    // Makes lists of audio from folders and subfolders, then register it into the api. For all audio in res://assets/test_audio_assets.
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
            if (dir.CurrentIsDir()) // For each directory in audio assets, create me lists
            {
                string directory_path = path + "/" + file_name;
                string[] subdirectories = DirAccess.GetDirectoriesAt(directory_path);
                if(subdirectories.IsEmpty())
                {
                    CreateSoundAssetList(directory_path, file_name); // If there are no subfolders in this directory, create a list with the sounds in it
                }                                                    // Else, if there are subfolders, create a list for each subfolder
                else
                {
                    foreach (string directory in DirAccess.GetDirectoriesAt(directory_path))
                    {
                    string subfolder_path = directory_path + "/" + directory;
                    CreateSoundAssetList(subfolder_path, directory);
                    }
                }
            }
            file_name = dir.GetNext(); // Continue with next directory
        }
    }

    public void CreateSoundAssetList(String directory_path, String file_name)
    {
        List<int> sound_ids = new List<int>();                      // New list of sounds for directory
        int    list_id = Audio.audio_list.all_audio_lists.Count;    // ID of new list
        string list_string;

        if(list_id<9)                                               // If ID is a single digit, put a zero in front
        {list_string = "0" + list_id.ToString();}                   // Either way, make it a string
        else
        {list_string = list_id.ToString();}

        string agent_type    = ApplyAgentType(file_name);           // Apply ID of agent type
        string material_type = ApplyMaterial(file_name);            // Apply ID of material
        string stick_id = string.Format("{0}{1}{2}", list_string, agent_type, material_type); // Join them together into hash

        foreach(var file in DirAccess.GetFilesAt(directory_path))   // For subfolder, make a sound list with the sounds in it
        {
            if(file.GetExtension() == "ogg")
            {
            string asset_path = directory_path + "/" + file;
            string asset_name = file.GetBaseName();
            sound_ids.Add(RegisterSoundAsset(asset_path, asset_name, "X"));
            }
        }

        RegisterSoundAssetList(stick_id, sound_ids, file_name);     // Finally, register the list 
    }

    // Registers the sound and assigns an ID to later access it in all_audio list
    public int  RegisterSoundAsset(string path, string name, string icon_path)
    {
        AudioAsset asset     = new AudioAsset();
        asset.file_path      = path;
        asset.file_name      = name;
        //asset.icon         = ResourceLoader.Load(icon_path) as Texture; // Works, enable when textures are ready
        asset.is_loaded      = false;
        AddAudioAsset(asset);
        asset.sound_id       = Audio.audio_list.all_audio.Count-1; // For the index to start at 0
        GD.Print("audio_asset_manager : Registered : " + name + " | ID : " + asset.sound_id + " | Filepath : " + path);
        return asset.sound_id;
    }

    // Packs sound IDs into a list to later access it in all_audio_lists with a string
    public void RegisterSoundAssetList(string list_id, List<int> asset_ids, string list_name)
    {
        //List<AudioAsset> new_sound_list = new List<AudioAsset>();
        //for (int current_id = 0; current_id <= (asset_ids.Count-1); current_id++)
        //{ new_sound_list.Add(Audio.audio_list.all_audio[(asset_ids[current_id])-1]); }  // Add all assets on asset_ids to new_sound_list (about the "-1", idk what is it but without it everything breaks)
        //Audio.audio_list.all_audio_lists.Add(new_sound_list);                         // Store new_sound_list with an integer ID
        Audio.audio_list.all_audio_lists.Add(list_id, asset_ids);                  // Store new_sound_list with an integer ID
        GD.Print("audio_asset_manager : New audio_list created for " + list_name + " | ID " + list_id);
    }

    // Add audio asset to all_audio list
    public void AddAudioAsset(AudioAsset asset)
    { 
        Audio.audio_list.all_audio.Add(asset); 
    }

    // Remove audio asset from all_audio list
    public void RemoveAudioAsset(AudioAsset asset)
    { 
        Audio.audio_list.all_audio.Remove(asset); 
    }

    // Loads all sounds asset for them to be playable
    public void LoadAllSoundAssets()
    {
        GD.Print("audio_asset_manager : Loading all sound assets");
        for(int x = 0; x < Audio.audio_list.all_audio.Count; x++)
        {
            if (Audio.audio_list.all_audio[x].is_loaded == false)
            {
                LoadSoundStream(x);
            }
        }
    }

    // Assigns a stream of audio to the sound asset
    public void LoadSoundStream(int asset_id)
    {
        var audio_stream_test = new AudioStreamOggVorbis();
        audio_stream_test     = GD.Load(Audio.audio_list_manager.GetAssetPathFromId(asset_id)) as AudioStreamOggVorbis;

        Audio.audio_list.all_audio[asset_id].stream   = audio_stream_test;
        Audio.audio_list.all_audio[asset_id].is_loaded = true;
    }

    // For debug purpose
    public void PrintAllLists()
    {
        GD.Print("- - - - - - - - -");
        GD.Print("audio_asset_manager : DEBUG | Printing all lists");

        foreach(string list in Audio.audio_list.all_audio_lists.Keys) // Keys = List ID (example: 010000)
        {
            GD.Print("audio_asset_manager : List : " + list);
            //for(int sound = 0; sound < Audio.audio_list.all_audio_lists[list].Count; sound++)
            foreach (int sound in Audio.audio_list.all_audio_lists[list])
            {
                GD.Print("audio_asset_manager : Sound Number : " + sound);
                GD.Print("audio_asset_manager : Contains ID " + Audio.audio_list.all_audio[sound].sound_id + " : " + Audio.audio_list.all_audio[sound].file_name);
            }
        }
        GD.Print("audio_asset_manager : All Audio : ");
        for(int id = 0; id < Audio.audio_list.all_audio.Count; id++)
        {
            GD.Print("audio_asset_manager : ID" + Audio.audio_list.all_audio[id].sound_id + " : Audio : " + Audio.audio_list.all_audio[id].file_name);
        }
        GD.Print("- - - - - - - - -");
    }

    // Working on this...
    public string ApplyMaterial(string file_name)
    {
        if(file_name.Contains("Rock"))
        {
            GD.Print("File has rock material");
            return "01";
        }
        else if(file_name.Contains("Concrete"))
        {
            GD.Print("File has concrete material");
            return "02";
        }
        else if(file_name.Contains("Dirt"))
        {
            GD.Print("File has dirt material");
            return "03";
        }
        else
        {
            return "00";
        }
    }

    // Working on this...
    public string ApplyAgentType(string file_name)
    {
        if(file_name.Contains("Human"))
        {
            GD.Print("File is human type");
            return "01";
        }
        else if(file_name.Contains("Monster"))
        {
            GD.Print("File is monster type");
            return "02";
        }
        else if(file_name.Contains("Droid"))
        {
            GD.Print("File is droid type");
            return "03";
        }
        else
        {
            return "00";
        }
    }
}