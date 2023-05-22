namespace AudioSystem;
using Godot;

public static class GameState
{
    
    public static bool IsInitialized { get; private set; }
    
    public static AudioAssetManager AudioAssetManager = new AudioAssetManager();
    
    public static void InitGameState(Node2D player)
    {
        AudioAssetManager.InitStage1(player);
    }
    
    public static void PlayAudio(int name)
    {
        AudioAssetManager.PlayAudio(name);
    }

    public static int getSoundListCount()
    {
        return AudioAssetManager.assets.Count;
    }

}