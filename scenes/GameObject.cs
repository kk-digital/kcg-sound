using System.Collections.Generic;
using Godot;
using System;

public class GameObject
{
    // Initialize this when all sounds are ready
    public Dictionary<string, int> continuity_manager = new Dictionary<string, int>();
	public SoundSpecifics sound_specifics = new SoundSpecifics();
}

public interface IGameObject
{
	public void Init();
}