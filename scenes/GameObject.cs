using Godot;
using AudioSystem;
using AudioEmitter;
using System;
//using AudioEmitter;

public partial class GameObject : Node2D
{
	public SoundSpecifics sound_specifics = new SoundSpecifics();
	int agent_id;

    // Initialize this when engine is ready
    public override void _Ready()
    {
		SetPhysicsProcess(false); 											// Stop from updating instantly
        agent_id = SoundApi.audio_emitter_manager.AddGameObject(this);  	// Adds itself to audio emitter list for initialization & referencing for its audio player
    }

    // Initialize this when all sounds are ready
    public void Init()
    {
		SetPhysicsProcess(true);
    }

	// Change to "Update" when transposing to main
	public override void _Process(double delta)
	{
		double 	speed 		= 300;
		double 	move_amount = speed * delta;
		Vector2 move_vector = new Vector2(0, 0);
	
		if (Input.IsKeyPressed(Key.S))
		{
			SoundManager.Walk(sound_specifics);

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
