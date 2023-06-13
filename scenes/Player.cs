using Godot;
using AudioSystem;
using AudioEmitter;
using System;
using System.Collections.Generic;
//using AudioEmitter;

public partial class Player : Node2D, IGameObject
{

    int agent_id;
    GameObject game_object = new GameObject();

    // Initialize this when engine is ready
    public override void _Ready()
    {
		SetPhysicsProcess(false); 											            // Stop from updating instantly
        SoundSpecifics sound_specifics = new SoundSpecifics();
        sound_specifics.agent_id = SoundApi.audio_emitter_manager.AddGameObject(game_object, this);  // Adds itself to audio emitter list for initialization & referencing for its audio player
    }

    // Initialize when all sounds are ready
    public void Init()
    {
		SetPhysicsProcess(true);
        Dictionary<string, int> dicto = new Dictionary<string, int>();
        
    }

	// Change to "Update" when transposing to main
	public override void _Process(double delta)
	{
		double 	speed 		= 300;
		double 	move_amount = speed * delta;
		Vector2 move_vector = new Vector2(0, 0);
	
		if (Input.IsKeyPressed(Key.S))
		{
			//SoundManager.Walk(sound_specifics);
			SoundManager.Explosion(game_object.sound_specifics);

			move_vector.Y = (float)move_amount;
		}
		if (Input.IsKeyPressed(Key.W))
		{
			move_vector.Y = (float)-move_amount;
		}
		if (Input.IsKeyPressed(Key.D))
		{
			move_vector.X = (float)move_amount;
		}
		if (Input.IsKeyPressed(Key.A))
		{
			move_vector.X = (float)-move_amount;
		}

		Position += move_vector;
	}

	/*
	public SoundSpecifics UpdateSoundSpecifics()
	{
		
	}
	*/
}