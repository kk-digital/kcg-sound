using System;
using Godot;
using System.Collections.Generic;

// This scripts simulates whatever record of entities it is saved on the engine
// Replace it as it should

namespace AudioSystem;
public static class EntityList
{
    public static List<Node2D> entities = new List<Node2D>();

    public static int AddEntity(Node2D which)
    {
        entities.Add(which);
        int Id = entities.Count - 1;
        return Id;
    }
}