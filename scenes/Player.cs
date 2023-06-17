using Godot;
using SoundApi;

public partial class Player : Node2D, IGameObject
{
    int agent_id;
    GameObject game_object = new GameObject();

    // Initialize this when engine is ready
    public override void _Ready()
    {
		SetPhysicsProcess(false); 											            		  // Stop from updating instantly
        SoundSpecifics sound_specifics = new SoundSpecifics();
        sound_specifics.agent_id = Audio.audio_emitter_manager.AddGameObject(game_object, this);  // Adds itself to audio emitter list for initialization & referencing for its audio player
	}

    // Initialize when all sounds are ready
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
	
		if (Input.IsActionJustPressed("ui_down"))
		{
			GD.Print("audio_playback_manager : Playing audio | ID : 0 | CoreMechStart");
			Audio.audio_event_tools.CancelReverb();
			Audio.weapons.EmptyClip(agent_id);
			//move_vector.Y = (float)move_amount;
		}
		if (Input.IsActionJustPressed("ui_up"))
		{
			Audio.audio_event_tools.ApplyReverb();
			Audio.weapons.ReloadPistol(agent_id);
			//move_vector.Y = (float)-move_amount;
		}
		if (Input.IsActionJustPressed("ui_right"))
		{
			Audio.audio_event_tools.CancelReverb();
			Audio.weapons.ShootPistol(agent_id);
			//move_vector.X = (float)move_amount;
		}
		if (Input.IsActionJustPressed("ui_left"))
		{
			GD.Print("audio_playback_manager : Playing audio | ID : 11 | Alltogether");
			Audio.audio_event_tools.CancelReverb();
			Audio.weapons.HitTerrain(agent_id);
			//move_vector.X = (float)-move_amount;
		}
		Position += move_vector;
	}
}