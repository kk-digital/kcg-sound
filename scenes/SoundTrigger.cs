using Godot;
using System;
using AudioSystem;

public partial class SoundTrigger : Node2D
{
	public float timePass = 0f;
	public int timeTicks = 0;
	public int sound_number = 1;

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
            PlayAudio();
		}
	}

    private void PlayAudio()
    {
        sound_number++;
        Math.Clamp(sound_number, 0, SoundApi.listManager.GetListLength());
        SoundApi.playbackManager.PlayAudio(sound_number, this);
    }
}
