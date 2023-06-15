using SoundApi;

//namespace AudioSystem;
public static class GameState
{
    public static bool IsInitialized { get; private set; }
    
    public static void InitGameState()
    {
        // (Fela)
        // Initialize sound | Create lists from subfolders and register & load all the sounds
        Audio.audio_asset_manager.Init();

        // Initialize sound emitters once sounds are loaded
        Audio.audio_emitter_manager.Init();
    }
}