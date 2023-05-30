using System.Collections.Generic;

namespace AudioSystem;
public class AudioList
{
	// Contains lists packed with audio, accesible by unique string ID
	public Dictionary<string, List<AudioAsset>> all_audio_lists = new Dictionary<string, List<AudioAsset>>();

	// All audio on the game, accesible by audio asset ID
	public List<AudioAsset> all_audio = new List<AudioAsset>();

	//public AudioEffect aB = AudioServer.GetBusEffect(AudioServer.GetBusIndex("Name"), 0); // For future Sound Systems
}
