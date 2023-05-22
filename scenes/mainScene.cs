using Godot;
using System;

public partial class mainScene : Node2D
{
	public override void _Ready()
	{
		Node tmp = GetNode("/root/MainScene/Player");
		Node2D player = (Node2D) tmp;

		SoundAssetManager.Init();
		SoundManager.Init();
		AudioSystem.GameState.InitGameState(player);
	}

	public override void _Process(double delta)
	{
	}
}
