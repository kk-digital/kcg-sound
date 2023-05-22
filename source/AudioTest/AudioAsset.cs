using Godot;

//namespace kcgsound.source.AudioTest;
namespace AudioSystem;

public class AudioAsset
{
    public string FilePath;
	public string FileName;
	public AudioStreamOggVorbis Stream;
	public int SoundID;
	public float SoundLength;
	public bool IsLoaded;
    public Texture Icon;
	public int RawSoundData;
}