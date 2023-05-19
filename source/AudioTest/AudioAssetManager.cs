using System.Collections.Generic;
//using Player;
using Godot;

//namespace kcgsound.source.AudioTest
namespace AudioSystem
{
    public class AudioAssetManager
    {   
        public List<AudioAsset>assets = new List<AudioAsset>();

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
            switch(name)
            {
                case "explosion":
                    return 1;
            }
            return 0;
        }

        /*
            To see:
                System.Collections.Generic.Dictionary<int, int> testing = new()
                List<int> test = new();
                Struct C#
                Ref C#
                Agents
        */

        public void InitStage1()
        {
            RegisterSoundAsset("res://assets/test_audio_assets/CoreMechStart.ogg", "Core_Mech", "core_mech.png");
            RegisterSoundAsset("res://assets/test_audio_assets/CoreShotStart.ogg", "Core_Shock", "core_shock.png");
            RegisterSoundAsset("res://assets/test_audio_assets/CoreBass.ogg", "Core_Bass", "core_bass.png");
            RegisterSoundAsset("res://assets/test_audio_assets/Atmo.ogg", "Atmo", "atmo.png");
            RegisterSoundAsset("res://assets/test_audio_assets/Reflection.ogg", "Reflection", "reflection.png");

            LoadSoundAssets();
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

            for(int x = 0; x < assets.Capacity; x++)
            {
                if (assets[x].IsLoaded == false)
                {
                    LoadAudioAsset(x);
                    //PlaySoundAssets();
                }
            }
        }

        /*
        public void PlaySoundAssets()
        {
            for(int x = 0; x < assets.Capacity; x++)
            {
                PlayAudio(x, new Node());
            }
        }
        */

        public AudioStreamOggVorbis LoadAudioAsset(int assetId)
        {
            var audioStreamTest = new AudioStreamOggVorbis();
            audioStreamTest = GD.Load(GetAssetFilePathFromId(assetId)) as AudioStreamOggVorbis;
            
            var audioStreamTest2 = new AudioStreamOggVorbis();
            audioStreamTest2.PacketSequence = audioStreamTest.PacketSequence;
            assets[assetId].IsLoaded = true;

            GD.Print("Sucesfully loaded ID " + assetId);
            return audioStreamTest2;
        }

        /*
        public Node2D CreateNode2D()
        {
            Node2D newNode = new Node2D();
            AddChild(newNode) // Doesn't work
        }
        */

        public AudioStreamPlayer2D PlayAudio(int assetId, Node parent)
        {
            var audioStreamPlayer = new AudioStreamPlayer2D();
            audioStreamPlayer.Stream = LoadAudioAsset(assetId);

            audioStreamPlayer.Bus = "Master";
            parent.AddChild(audioStreamPlayer);
            audioStreamPlayer.Play();

            GD.Print("Sucesfully played ID " + assetId);
            return audioStreamPlayer;
        }
    }
}
