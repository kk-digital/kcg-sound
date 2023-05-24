using System.Collections.Generic;

namespace AudioSystem;
public static class SoundApi // SEE README
{
    public static List<AudioAsset>      list            = new AudioList().assets;
    public static AudioListManager      listManager     = new AudioListManager();
    public static AudioAssetManager     assetManager    = new AudioAssetManager();
    public static AudioAssetManager     gameState       = new AudioAssetManager();
    public static AudioPlaybackManager  playbackManager = new AudioPlaybackManager();

    public static void Init()
    {
    }

    public static void Update()
    {
    }
}