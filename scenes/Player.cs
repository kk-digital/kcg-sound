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
			Audio.weapons.EmptyClip(agent_id);
			//move_vector.Y = (float)move_amount;
		}
		if (Input.IsActionJustPressed("ui_up"))
		{
			Audio.weapons.ReloadPistol(agent_id);
			//move_vector.Y = (float)-move_amount;
		}
		if (Input.IsActionJustPressed("ui_right"))
		{
			Audio.weapons.ShootPistol(agent_id);
			//move_vector.X = (float)move_amount;
		}
		if (Input.IsActionJustPressed("ui_left"))
		{
			Audio.weapons.HitTerrain(agent_id);
			//move_vector.X = (float)-move_amount;
		}
		Position += move_vector;
	}
}