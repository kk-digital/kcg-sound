using Godot;

public class SoundAssetManager
{
	Godot.Collections.Array<AudioStreamPlayer2D> soundAssets = new 
	Godot.Collections.Array<AudioStreamPlayer2D>();

	public static void Init()
	{
		GD.Print("INITIALIZED ASSET MANAGER");
	}
}