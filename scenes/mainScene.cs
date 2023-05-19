using Godot;
using System;

public partial class mainScene : Node2D
{
	public override void _Ready()
	{
		SoundAssetManager.Init();
		SoundManager.Init();
		AudioSystem.GameState.InitGameState();
	}

	public override void _Process(double delta)
	{
	}
}
