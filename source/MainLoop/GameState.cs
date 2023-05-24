using Godot;
using Entity;

namespace AudioSystem;
public static class GameState
{
    
    public static bool IsInitialized { get; private set; }
    
    public static void InitGameState()
    {
        SoundApi.assetManager.InitStage1();
    }

}