using Godot;
using System;

public class GameStateInitialize : Node
{
    public override void _Ready()
    {
        Init();
    }

    public void Init()
    {
        SoundAssetManager.Init();
        SoundManager.Init();
    }
}