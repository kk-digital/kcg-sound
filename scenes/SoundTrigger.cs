//using System;

namespace AudioSystem;
public partial class SoundTrigger : Node2D
{
	public float timePass = 0f;
	public int timeTicks = 0;
	public int sound_number = -1;

    public override void _Ready()
    {
		SetPhysicsProcess(false);
        AudioEmitterManager.emitters.Add(this);
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
        sound_number++;
        //sound_number = Math.Clamp(sound_number, 0, SoundApi.listManager.GetListLength());
        SoundApi.playbackManager.PlayAudio(sound_number, this);
    }

}
