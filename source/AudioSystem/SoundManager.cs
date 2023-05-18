using Godot;
using System;

public partial class SoundManager : Node
{
	[Export]
	public int soundAssetId = 0;
	public override void _Ready()
	{
		GD.Print("asdasd");
	}

	public void PlaySound(int SoundAssetId = 2) 
	{
		GD.Print("Plays sound of ID " + GD.VarToStr(SoundAssetId));
	}

	public override void _Process(double delta)
	{
	}
}