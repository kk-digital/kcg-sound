//using System;
using Godot;

namespace AudioSystem;
public partial class SoundTrigger : Node2D
{
	public float timePass = 0f;
	public int timeTicks = 0;
	public int sound_number = 0;
    public int entityId;

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
            Play();
		}
	}

    private void Play()
    {
        // Play audio:
        //SoundApi.playbackManager.PlayAudio(sound_number, entityId);

        // Play sequences:
        sound_number = SoundApi.playbackManager.PlayAudioInSequence("TEST_SEQUENCE", entityId, sound_number);
        //sound_number = SoundApi.playbackManager.PlayAudioPseudoRandomly("TEST_SEQUENCE", entityId, sound_number);
        //SoundApi.playbackManager.PlayAudioRandomly("TEST_SEQUENCE", entityId);
    }
}
