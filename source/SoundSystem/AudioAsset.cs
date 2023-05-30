using Godot;

namespace AudioSystem;
public class AudioAsset
{
	// Propieties of each sound asset
    public string 				file_path;
	public string 				file_name;
	public AudioStreamOggVorbis stream;
	public int					sound_id;
	public float 				soundL_length;
	public bool 				is_loaded;
    public Texture 				icon;
	public int 					raw_sound_data;
}