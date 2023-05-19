namespace AudioSystem;

public static class GameState
{
    
    public static bool IsInitialized { get; private set; }
    
    public static AudioAssetManager AudioAssetManager { get; private set; } = new();
    
    public static void InitGameState()
    {
        AudioAssetManager.InitStage1();
    }
    
}