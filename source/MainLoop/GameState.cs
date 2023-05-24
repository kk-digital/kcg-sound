using Godot;
using Entity;

namespace AudioSystem;
public static class GameState
{
    public static bool IsInitialized { get; private set; }
    
    public static void InitGameState()
    {
        SoundAssetManager.Init();           // Placeholder, following instructions
        SoundManager.Init();                // Placeholder, following instructions
        SoundApi.assetManager.Init();
        AudioEmitterManager.Init();
        
    }
}