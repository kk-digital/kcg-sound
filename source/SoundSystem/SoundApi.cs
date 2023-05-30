using System.Collections.Generic;
using AudioEvent;

namespace AudioSystem;
public static class SoundApi // SEE README
{
    public static AudioList             audio_list             = new AudioList();
    public static AudioListManager      audio_list_manager     = new AudioListManager();
    public static AudioAssetManager     audio_asset_manager    = new AudioAssetManager();
    public static AudioPlaybackManager  audio_playback_manager = new AudioPlaybackManager();
    public static AudioEventTools       audio_event_tools      = new AudioEventTools();

    public static void Init()
    {
    }

    public static void Update()
    {
    }
}