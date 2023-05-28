using Godot;
using System;
using System.Collections.Generic;

namespace AudioSystem;
public class AudioList
{
	public Dictionary<string, List<AudioAsset>> allAudioLists = new Dictionary<string, List<AudioAsset>>();
	public List<AudioAsset> allAudio = new List<AudioAsset>();

	public List<AudioAsset> walkingRock = new List<AudioAsset>();
	public List<AudioAsset> walkingMetal = new List<AudioAsset>();

	public List<AudioAsset> weaponMachineGun = new List<AudioAsset>();

	public List<AudioAsset> shootRock = new List<AudioAsset>();
	public List<AudioAsset> shootMetal = new List<AudioAsset>();

	public void Init()
	{
	}
	
	//public AudioEffect aB = AudioServer.GetBusEffect(AudioServer.GetBusIndex("Name"), 0); // For future Sound Systems
}
