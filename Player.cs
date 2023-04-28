using Godot;
using System;

public partial class Player : Node2D
{
	private AudioStreamPlayer2D soundNode;
	public AudioStreamPlayer2D playSound(Node parent)
	{
		var audioStreamPlayer = new AudioStreamPlayer2D();
		audioStreamPlayer.Stream = GD.Load("res://testaudioassets/Atmo.ogg") as AudioStreamOggVorbis;
		audioStreamPlayer.Bus = "Master";
		parent.AddChild(audioStreamPlayer);
		audioStreamPlayer.Play();
		return audioStreamPlayer;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundNode = playSound(GetNode("/root/Player"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if (soundNode != null)
		{
			if (!soundNode.Playing)
			{
				soundNode.QueueFree();
				soundNode = null;
			}			
		}

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
