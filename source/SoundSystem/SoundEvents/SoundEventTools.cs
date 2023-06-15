using Godot;

namespace AudioEvent;
public class AudioEventTools
{
    // Audio tools for future sound events

    public static RandomNumberGenerator rng = new RandomNumberGenerator();

    public void LowPass(float time, float frequency)
    {
    }

    public void Crossfade(int assetId1, int assetId2, float time)
    {
    }

    public void ApplyReverb(int bus, float size, float length)
    {
    }

    public void Attenuation(int bus, float db, float time)
    {
    }
}

// For future sound systems
//public AudioEffect aB = AudioServer.GetBusEffect(AudioServer.GetBusIndex("Name"), 0); 