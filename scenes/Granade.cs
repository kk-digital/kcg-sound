using Godot;
using SoundApi;

public partial class Granade : RigidBody2D, IGameObject
{
    int agent_id;
    GameObject game_object = new GameObject();

    public override void _Ready()
    {
        // Register game object for proper initialization when sound system is ready.
        agent_id = Audio.audio_emitter_manager.AddGameObject(game_object, this); 

        // Connect trigger signals
        this.BodyEntered   += (body) => Bounce(body);
    }

    public void Init()
    {
        GD.Print("granade : Ready");
        ThrowGranade();
    }

    // Emit the throw trigger
    public void ThrowGranade()
    {
        Thrown();
        StartGrenade();
    }

    // Create a timer for explote time
    public void StartGrenade()
    {
        var manager = Audio.audio_playback_manager;
        manager.StartTimer(manager.CreateTimer(Explote, agent_id), 2.5f);
    }

    // Play throw sound
    public void Thrown()
    {
        GD.Print("granade : Thrown");
        Audio.weapons.GranadeThrown(agent_id);
    }

    // Bounces with each collision
    public void Bounce(Node body = null)
    {
        GD.Print("granade : Bounced");
        Audio.weapons.GranadeBounce(agent_id);
    }

    // Play explotion sound
    public void Explote()
    {
        GD.Print("granade : Explodes");
        Audio.weapons.GranadeExplosion(agent_id);
    }
}
