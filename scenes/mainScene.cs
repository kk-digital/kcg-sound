using Godot;
using System;

namespace MainScene;
public partial class mainScene : Node2D // Needs to be partial or godot doesn't compile
{
	public Node tmp;
	public Node2D player;

	public override void _Ready()
	{

		tmp =  GetNode("/root/MainScene/Player");
		player = (Node2D) tmp;

		SoundAssetManager.Init();
		SoundManager.Init();
		AudioSystem.GameState.InitGameState();
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
