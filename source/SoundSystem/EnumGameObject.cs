using System.Collections.Generic;

// This scripts simulates whatever record of game_objects it is saved on the engine
// Replace it as it should

namespace AudioSystem;
public static class EnumGameObject
{
    public static List<GameObjectRef> game_objects = new List<GameObjectRef>();

    // Adds game object to list to later be referenced for its AudioStreamPlayer2D
    public static int AddGameObject(GameObjectRef which)
    {
        game_objects.Add(which);
        int id = game_objects.Count - 1;
        return id;
    }
}
    /*
    struct GameObjectRef 
    {
        enum GameObjectType{}
        enum GameObjectId{}
    }
    */