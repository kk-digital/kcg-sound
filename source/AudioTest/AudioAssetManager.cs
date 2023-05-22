using System.Collections.Generic;
using Godot;
using Player;
using System;

namespace AudioSystem
{
    public partial class AudioAssetManager : Node
    {   
        public List<AudioAsset>assets = new List<AudioAsset>();
        public Node2D player;

        public void AddAsset(AudioAsset asset)
        {
            assets.Add(asset);
        }

        public void RemoveAsset(AudioAsset asset)
        {
            assets.Remove(asset);
        }

        public AudioAsset GetAssetFromId(int Id)
        {
            return assets[Id];
        }
        
        public string GetAssetFilePathFromId(int Id)
        {
            return assets[Id].FilePath;
        }

        public int SoundNameToId(string name)
        {            
            foreach (AudioAsset i in assets)
            {
                if (i.FileName == name)
                {
                    GD.Print("Found ID of " + name);
                    return i.SoundID;
                }
            }
            GD.Print("ERROR: " + name + " was not found");
            return 0;
        }

        public void InitStage1(Node2D player_pass)
        {
            player = player_pass; // Reference to get AudioPlayer for function PlayAudio()

            RegisterSoundAsset("res://assets/test_audio_assets/CoreMechStart.ogg", "Core_Mech", "core_mech.png");
            RegisterSoundAsset("res://assets/test_audio_assets/CoreShotStart.ogg", "Core_Shock", "core_shock.png");
            RegisterSoundAsset("res://assets/test_audio_assets/CoreBass.ogg", "Core_Bass", "core_bass.png");
            RegisterSoundAsset("res://assets/test_audio_assets/Atmo.ogg", "Atmo", "atmo.png");
            RegisterSoundAsset("res://assets/test_audio_assets/Reflection.ogg", "Reflection", "reflection.png");
            RegisterSoundAsset("res://assets/test_audio_assets/CoreShot.ogg", "Core_Shot", "reflection.png");

            LoadSoundAssets();
            PlayAudio(0);
        }

        public void RegisterSoundAsset(string path, string name, string iconPath)
        {
            AudioAsset asset = new AudioAsset();
            asset.FilePath = path;
            asset.FileName = name;
            //asset.Icon =  ResourceLoader.Load(iconPath) as Texture;
            asset.IsLoaded = false;
            assets.Add(asset);
            asset.SoundID = assets.Capacity;
        }

        public void LoadSoundAssets()
        {
            GD.Print("Loading all sound assets");

            for(int x = 0; x < assets.Count; x++)
            {
                if (assets[x].IsLoaded == false)
                {
                    LoadAudioAsset(x);
                }
            }
        }

        public void PlaySoundAssets()
        {
            GD.Print("Playing all sound assets");

            for(int x = 0; x < assets.Count; x++)
            {
                if (assets[x].IsLoaded == true)
                {
                    PlayAudio(x);
                }
            }
        }

        public void LoadAudioAsset(int assetId)
        {
            var audioStreamTest = new AudioStreamOggVorbis();
            audioStreamTest = GD.Load(GetAssetFilePathFromId(assetId)) as AudioStreamOggVorbis;

            assets[assetId].Stream = audioStreamTest;
            assets[assetId].IsLoaded = true;

            GD.Print("Sucesfully loaded ID " + assetId + " " + assets[assetId].IsLoaded);
        }

        public AudioStreamPlayer2D PlayAudio(int assetId)
        {
            GD.Print("Playing audio : " + assets[assetId].FileName);

            var audioStreamPlayer = new AudioStreamPlayer2D();
            audioStreamPlayer.Stream = assets[assetId].Stream;
            audioStreamPlayer.Bus = "Master";

            player.AddChild(audioStreamPlayer);
            audioStreamPlayer.Play();
            GD.Print("Sucesfully played " + assets[assetId].FileName);

            return audioStreamPlayer;
        }
    }
}

/*
    To see:
        System.Collections.Generic.Dictionary<int, int> testing = new()
        List<int> test = new();
        Struct C#
        Ref C#
        Agents
*/