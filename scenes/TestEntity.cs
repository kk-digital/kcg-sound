//using System;
using Godot;

namespace AudioSystem;
public partial class TestEntity : Node2D
{
	public float timePass = 0f;
	public int timeTicks = 0;
	public int sound_number = 0;
    public int entityId;
    public SoundEventContinuity continuityManager = new SoundEventContinuity();
    

    public override void _Ready()
    {
		SetPhysicsProcess(false);
        AudioEmitterManager.emitters.Add(this);
        entityId = EntityList.AddEntity(this);
    }

    public void Init()
    {
		SetPhysicsProcess(true);
    }

    public override void _Process(double delta)
    {
        TimerSound();
    }

	public void TimerSound()
	{
		timePass++;
		if(timePass >= 100)
		{
			timeTicks++;
			timePass = 0;
            NewPlay();
		}
	}

    private void NewPlay()
    {
        SoundEventAgentAction.Walk(entityId);
    }
}
