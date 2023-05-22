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

	public int time_pass = 0;
	public int sound_number = 1;

	public override void _Process(double delta)
	{ 
		TimerSound();
	}

	public void TimerSound()
	{
		time_pass++;
		if(time_pass >= 100)
		{
			GD.Print("Tick");
			time_pass = 0;
			sound_number++;
			Math.Clamp(sound_number, 0, AudioSystem.GameState.getSoundListCount());
			AudioSystem.GameState.PlayAudio(sound_number);
		}
	}
}
