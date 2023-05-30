//using System;
using Godot;

namespace AudioSystem;
public partial class GameObjectRef : Node2D
{
	public float time_pass = 0f;
	public int time_ticks = 0;
	public int sound_number = 0;
    public int entity_id;
    public SoundEventContinuity continuity_manager = new SoundEventContinuity();
    
    // Initialize when engine is ready
    public override void _Ready()
    {
		SetPhysicsProcess(false);
        AudioEmitterManager.audio_emitters.Add(this);
        entity_id = EnumGameObject.AddGameObject(this);
    }

    // Initialize when all sounds are ready
    public void Init()
    {
		SetPhysicsProcess(true);
    }

    // Update every frame to keep track of time
    public override void _Process(double delta)
    {
        TimerSound();
    }

    // Play a sound after X miliseconds
	public void TimerSound()
	{
		time_pass++;
		if(time_pass >= 100)
		{
			time_ticks++;
			time_pass = 0;
            Play();
		}
	}

    // Play the sound
    private void Play()
    {
        SoundEventAgentAction.Walk(entity_id);
    }
}
