using System.Collections.Generic;

namespace AudioSystem;
public static class AudioEmitterManager
{
    public static List<SoundTrigger> emitters = new List<SoundTrigger>(); 
    
    public static void Init()
    {
        foreach (SoundTrigger a in emitters)
        {
            a.Init();
        }
    }
}