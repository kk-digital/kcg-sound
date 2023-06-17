using Godot;

namespace AudioEvent;
public class AudioEventTools
{
    // Audio tools for future sound events

    public static RandomNumberGenerator rng = new RandomNumberGenerator();

    public void StartLowPass(string channel, float time, float frequency)
    {
        AudioEffectEQ audio_effects = (AudioEffectEQ) AudioServer.GetBusEffect(AudioServer.GetBusIndex(channel), 0);
        AudioServer.SetBusEffectEnabled(0, 0, true);

        // Needs future tween for smooth transition
        audio_effects.SetBandGainDb(3, -60);
        audio_effects.SetBandGainDb(4, -60);
        audio_effects.SetBandGainDb(5, -60);
        
    }

    public void StopLowPass(string channel, float time, float frequency)
    {
        AudioEffectEQ audio_effects = (AudioEffectEQ) AudioServer.GetBusEffect(AudioServer.GetBusIndex(channel), 0);
        AudioServer.SetBusEffectEnabled(0, 0, false);

        // Needs future tween for smooth transition
        audio_effects.SetBandGainDb(3, 0);
        audio_effects.SetBandGainDb(4, 0);
        audio_effects.SetBandGainDb(5, 0);
    }

    public void ApplyReverb(string channel = "Master", float size = 0.8f, float length = 1.0f)
    {
        if(!AudioServer.IsBusEffectEnabled(0, 1))
        {
            GD.Print("sound_event_tools : Reverb Activated");
            AudioEffectReverb audio_effects = (AudioEffectReverb) AudioServer.GetBusEffect(AudioServer.GetBusIndex(channel), 1);
            AudioServer.SetBusEffectEnabled(0, 1, true); // Enable effect

            // Future tween for smooth transition
            /*
            Tween tween = Godot.SceneTree.CreateTween;
            tween.TweenProperty(audio_effects, "Wet", 0.5, 1.0);
            tween.TweenProperty(audio_effects, "RoomSize", size, 1.0);
            tween.TweenProperty(audio_effects, "Spread", length, 1.0);
            */
        }
    }

    public void CancelReverb(string channel = "Master")
    {
        if(AudioServer.IsBusEffectEnabled(0, 1))
        {
            GD.Print("sound_event_tools : Reverb De-activated");
            AudioEffectReverb audio_effects = (AudioEffectReverb) AudioServer.GetBusEffect(AudioServer.GetBusIndex(channel), 1);

            // Future tween for smooth transition
            /*
            Tween tween = Godot.SceneTree.CreateTween;
            tween.TweenProperty(audio_effects, "Wet", 0.5, 1.0);
            tween.TweenProperty(audio_effects, "RoomSize", size, 1.0);
            tween.TweenProperty(audio_effects, "Spread", length, 1.0);
            */

            AudioServer.SetBusEffectEnabled(0, 1, false); // Disable effect
        }
    }

    public void Crossfade(int assetId1, int assetId2, float time)
    {
    }

    public void Attenuation(int bus, float db, float time)
    {
    }
}