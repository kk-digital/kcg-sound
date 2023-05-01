using System.Collections.Generic;

namespace kcgsound.source.AudioTest
{
    public class AudioAssetList
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

        public AudioAsset GetPathFromId(int Id)
        {
            return assets[Id];
        }
        
    }
}
