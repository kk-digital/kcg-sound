using AudioEvent;
using AudioEmitter;
using AudioSystem;

namespace SoundApi;
public static class Audio // SEE README
{
    public static AudioList             audio_list             = new AudioList();
    public static AudioListManager      audio_list_manager     = new AudioListManager();
    public static AudioAssetManager     audio_asset_manager    = new AudioAssetManager();
    public static AudioPlaybackManager  audio_playback_manager = new AudioPlaybackManager();
    public static AudioEventTools       audio_event_tools      = new AudioEventTools();
    public static AudioContainer        audio_container        = new AudioContainer();
    public static AudioEmitterManager   audio_emitter_manager  = new AudioEmitterManager();

    public static AudioEmitterWeapons   weapons  = new AudioEmitterWeapons();

    public static void Init()
    {
    }

    public static void Update()
    {
    }
}