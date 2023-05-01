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
            AddAssetByPath("res://testaudioassets/CoreMech.ogg");
            AddAssetByPath("res://testaudioassets/CoreShotStart.ogg");
            AddAssetByPath("res://testaudioassets/Atmo.ogg");
        }

        public AudioStreamPlayer2D PlayAudio(int assetId, Node parent)
        {
            var audioStreamPlayer = new AudioStreamPlayer2D();
            audioStreamPlayer.Stream = GD.Load(GetAssetFilePathFromId(assetId)) as AudioStreamOggVorbis;
            audioStreamPlayer.Bus = "Master";
            parent.AddChild(audioStreamPlayer);
            audioStreamPlayer.Play();
            return audioStreamPlayer;           
        }
    }
}
