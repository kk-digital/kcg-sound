using Godot;
using System;

public partial class mainScene : Node2D
{
	public override void _Ready()
	{
		SoundAssetManager.Init();
		SoundManager.Init();
	}

	public override void _Process(double delta)
	{
	}
}
