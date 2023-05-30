using System.Collections.Generic;

namespace AudioSystem;
public static class AudioEmitterManager
{
    public static List<GameObjectRef> audio_emitters = new List<GameObjectRef>(); 
    
    // Initialize all sound emitters once sounds are loaded
    public static void Init()
    {
        foreach (GameObjectRef emitter in audio_emitters)
        {
            emitter.Init();
        }
    }
}