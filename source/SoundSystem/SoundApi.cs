using System.Collections.Generic;
using SoundEvent;

namespace AudioSystem;
public static class SoundApi // SEE README
{
    public static AudioList             list            = new AudioList();
    public static AudioListManager      listManager     = new AudioListManager();
    public static AudioAssetManager     assetManager    = new AudioAssetManager();
    public static AudioPlaybackManager  playbackManager = new AudioPlaybackManager();
    public static SoundEventTools       soundEventTools = new SoundEventTools();

    public static void Init()
    {
    }

    public static void Update()
    {
    }
}