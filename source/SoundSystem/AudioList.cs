using System.Collections.Generic;

namespace AudioSystem;
public class AudioList
{
	// Contains all audio on the game, accesible by audio asset's ID.
	public List<AudioAsset> all_audio = new List<AudioAsset>();

	// Contains lists of sounds. Is also contained in all audio.
	//public List<List<AudioAsset>> all_audio_lists = new List<List<AudioAsset>>(); // Integer ID -> Asset
	public Dictionary<string, List<AudioAsset>> all_audio_lists = new Dictionary<string, List<AudioAsset>>(); // Integer ID -> Asset

	//public AudioEffect aB = AudioServer.GetBusEffect(AudioServer.GetBusIndex("Name"), 0); // For future Sound Systems
}
