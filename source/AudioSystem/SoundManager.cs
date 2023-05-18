using Godot;
using System;

public partial class SoundManager
{
	[Export]
	public int soundAssetId = 0;

	public static void Init()
	{
		GD.Print("INITIALIZED SOUND MANAGER");
	}

	public void PlaySound(int SoundAssetId = 0) 
	{
		GD.Print("Plays sound of ID " + GD.VarToStr(SoundAssetId));
	}
}