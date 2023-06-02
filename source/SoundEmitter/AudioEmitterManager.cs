using System.Collections.Generic;
using Godot;

namespace AudioSystem;
public class AudioEmitterManager
{
    public List<GameObject> audio_emitters = new List<GameObject>(); 

    // Register a sound emitter
    public int AddGameObject(GameObject which)
    {
        audio_emitters.Add(which);
        int id = audio_emitters.Count - 1;
        return id;
    }

    // Initialize all sound emitters once sounds are loaded
    public void Init()
    {
        foreach (GameObject emitter in audio_emitters)
        {
            emitter.Init();
        }
    }
}