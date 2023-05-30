using System.Collections.Generic;

namespace AudioSystem;
public static class AudioEmitterManager
{
    public static List<TestEntity> emitters = new List<TestEntity>(); 
    
    public static void Init()
    {
        foreach (TestEntity a in emitters)
        {
            a.Init();
        }
    }
}