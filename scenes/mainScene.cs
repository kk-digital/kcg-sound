using Godot;
using System;
using AudioSystem;

namespace MainScene;
public partial class mainScene : Node2D // Needs to be partial or godot doesn't compile
{
	public override void _Ready()
	{
		GameState.InitGameState();
	}

	public override void _Process(double delta)
	{
		//MainLoopUpdate();
	}

	public void MainLoopUpdate()
	{ 
	}

	public void MainLoopInit()
	{ 
	}
}
