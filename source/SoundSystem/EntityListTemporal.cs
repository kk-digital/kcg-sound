using System;
using Godot;
using System.Collections.Generic;

// This scripts simulates whatever record of entities it is saved on the engine
// Replace it as it should

namespace AudioSystem;
public static class EntityList
{
    public static List<TestEntity> entities = new List<TestEntity>();

    public static int AddEntity(TestEntity which)
    {
        entities.Add(which);
        int Id = entities.Count - 1;
        return Id;
    }
}