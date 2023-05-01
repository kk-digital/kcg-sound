using Godot;
using kcgsound.source.AudioTest;
using System;

namespace Player;
public partial class Player : Node2D
{
	private AudioStreamPlayer2D soundNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameState.InitGameState();
		// add and play our test audio
		soundNode = GameState.AudioAssetManager.PlayAudio(0, GetNode("/root/Player"));
		soundNode = GameState.AudioAssetManager.PlayAudio(1, GetNode("/root/Player"));
		soundNode = GameState.AudioAssetManager.PlayAudio(2, GetNode("/root/Player"));
		soundNode = GameState.AudioAssetManager.PlayAudio(3, GetNode("/root/Player"));
		soundNode = GameState.AudioAssetManager.PlayAudio(4, GetNode("/root/Player"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		double speed = 300;
		double moveAmount = speed * delta;
		Vector2 moveVector = new Vector2(0, 0);
		if (Input.IsKeyPressed(Key.S))
		{
			moveVector.Y = (float)moveAmount;
		}
		if (Input.IsKeyPressed(Key.W))
		{
			moveVector.Y = (float)-moveAmount;
		}
		if (Input.IsKeyPressed(Key.D))
		{
			moveVector.X = (float)moveAmount;
		}
		if (Input.IsKeyPressed(Key.A))
		{
			moveVector.X = (float)-moveAmount;
		}

		Position += moveVector;
	}
}
