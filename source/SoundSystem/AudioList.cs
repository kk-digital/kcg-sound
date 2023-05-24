using Godot;
using System;
using System.Collections.Generic;

namespace AudioSystem;
public class AudioList
{
	public List<AudioAsset> assets = new List<AudioAsset>();

	//public AudioEffect aB = AudioServer.GetBusEffect(AudioServer.GetBusIndex("Name"), 0); // For future Sound Systems
}
