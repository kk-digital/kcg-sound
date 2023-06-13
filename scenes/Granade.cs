using Godot;
using System;
using AudioEmitter;
using AudioSystem;

public partial class Granade : RigidBody2D, IGameObject
{
    int agent_id;
    GameObject game_object = new GameObject();

    [Signal] public delegate void ThrownSignalEventHandler();
    [Signal] public delegate void ExploteSignalEventHandler();

    public override void _Ready()
    {
        // Register game object for proper initialization when sound system is ready.
        agent_id = SoundApi.audio_emitter_manager.AddGameObject(game_object, this); 

        // Connect trigger signals
        this.BodyEntered   += (body) => Bounce(body);
        this.ThrownSignal  += ()     => Thrown();
        this.ExploteSignal += ()     => Explote();
    }
    public void Init()
    {
        GD.Print("granade : Ready");
        ThrowGranade();
    }
    public void ThrowGranade()
    {
        // Emit the throw trigger
        EmitSignal(SignalName.ThrownSignal);
        // Create a timer for explote time
        var manager = SoundApi.audio_playback_manager;
        manager.StartTimer(manager.CreateTimer(Explote, agent_id), 2.5f);
    }
    // Bounces with each collision
    public void Bounce(Node body = null)
    {
        GD.Print("granade : Bounced");
        AudioEmitterWeapons.GranadeBounce(agent_id);
    }
    // Play throw sound
    public void Thrown()
    {
        GD.Print("granade : Thrown");
        AudioEmitterWeapons.GranadeThrown(agent_id);
    }
    // Play explotion sound
    public void Explote()
    {
        GD.Print("granade : Explodes");
        AudioEmitterWeapons.GranadeExplosion(agent_id);
    }
}
