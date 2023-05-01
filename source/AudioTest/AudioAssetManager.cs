using System.Collections.Generic;
using Player;
using Godot;

namespace kcgsound.source.AudioTest
{
    public class AudioAssetManager

    {
        
        public List<AudioAsset>assets = new List<AudioAsset>();

        public void AddAsset(AudioAsset asset)
        {
            assets.Add(asset);
        }
        
        public void AddAssetByPath(string path)
        {
            AudioAsset asset = new AudioAsset();
            asset.FilePath = path;
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

        public void InitStage1()
        {
            AddAssetByPath("res://testaudioassets/CoreMechStart.ogg");
            AddAssetByPath("res://testaudioassets/CoreShotStart.ogg");
            AddAssetByPath("res://testaudioassets/CoreBass.ogg");
            AddAssetByPath("res://testaudioassets/Atmo.ogg");
            AddAssetByPath("res://testaudioassets/Reflection.ogg");
        }

        public AudioStreamPlayer2D PlayAudio(int assetId, Node parent)
        {
            
            // test function TODO: remove
            
            var audioStreamPlayer = new AudioStreamPlayer2D();

            var audioStreamTest = new AudioStreamOggVorbis();
            audioStreamTest = GD.Load(GetAssetFilePathFromId(assetId)) as AudioStreamOggVorbis;
            
            var audioStreamTest2 = new AudioStreamOggVorbis();
            audioStreamTest2.PacketSequence = audioStreamTest.PacketSequence;

            audioStreamPlayer.Stream = audioStreamTest2;
            
            audioStreamPlayer.Bus = "Master";
            parent.AddChild(audioStreamPlayer);
            audioStreamPlayer.Play();
            return audioStreamPlayer;           
        }
    }
}
