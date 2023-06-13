using System.Collections.Generic;
using Godot;
using AudioSystem;

public class AudioEmitterManager
{
    public List<GameObject> audio_data = new List<GameObject>(); 
    public List<Node> audio_emitters = new List<Node>(); 

    // Register a sound emitter
    public int AddGameObject(GameObject data, Node emitter)
    {
        audio_data.Add(data);           // Has information like materials, agent ID, sound continuity, etc.
        audio_emitters.Add(emitter);    // Has the direct agent reference
        int id = audio_emitters.Count-1;// If not -1, breaks index.
        GD.Print("audio_emitter_manager : New game object created :  " + emitter.Name + " | ID : " + id);
        return id;
    }

    // Initialize all sound emitters once sounds are loaded
    public void Init()
    {
        foreach (IGameObject emitter in audio_emitters)
        {
            emitter.Init();
        }
    }
}