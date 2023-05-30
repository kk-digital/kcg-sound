using System;
using AudioSystem;
using Godot;
using System.Collections.Generic;

namespace SoundEvent;
public class SoundEventTools
{
    public static RandomNumberGenerator rng = new RandomNumberGenerator();

	//public AudioEffect aB = AudioServer.GetBusEffect(AudioServer.GetBusIndex("Name"), 0); // For future Sound Systems

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