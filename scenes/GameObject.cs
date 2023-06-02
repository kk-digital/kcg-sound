using Godot;
using AudioSystem;

public partial class GameObject : Node2D
{
	public AudioEmitter.AudioEmitterAgentMovement audio_movement = new AudioEmitter.AudioEmitterAgentMovement();
    public SoundEventContinuity continuity_manager = new SoundEventContinuity();
	int agent_id;


	// To reference time passing
	public float time_pass     = 0f;
	public int   time_ticks    = 0;

    // Initialize this when engine is ready
    public override void _Ready()
    {
		SetPhysicsProcess(false); 											// Stop from updating instantly
        agent_id = SoundApi.audio_emitter_manager.AddGameObject(this);  	// Adds itself to audio emitter list for initialization & referencing for its audio player
		audio_movement.movement_data.agent_id = agent_id; 					// Asigns its ID to sound emitter
    }

    // Initialize this when all sounds are ready
    public void Init()
    {
		SetPhysicsProcess(true);
		audio_movement.Initialize(agent_id);
    }

	// Change to "Update" when transposing to main
	public override void _Process(double delta) 
	{
		double 	speed 		= 300;
		double 	move_amount = speed * delta;
		Vector2 move_vector = new Vector2(0, 0);
	
		// current_animation should be in animation system
		if (Input.IsKeyPressed(Key.S))
		{
			audio_movement.movement_data.next_animation = AudioEmitter.animation.WALK;
			move_vector.Y = (float)move_amount;
		}
		if (Input.IsKeyPressed(Key.W))
		{
			audio_movement.movement_data.next_animation = AudioEmitter.animation.IDLE;
			move_vector.Y = (float)-move_amount;
		}
		if (Input.IsKeyPressed(Key.D))
		{
			audio_movement.movement_data.next_animation = AudioEmitter.animation.JUMP;
			move_vector.X = (float)move_amount;
		}
		if (Input.IsKeyPressed(Key.A))
		{
			audio_movement.movement_data.next_animation = AudioEmitter.animation.DOUBLE_JUMP;
			move_vector.X = (float)-move_amount;
		}

		Position += move_vector;

		// This function updates the audio
		audio_movement.Update();
	}
}
