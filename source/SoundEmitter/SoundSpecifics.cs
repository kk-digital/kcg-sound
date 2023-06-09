using Godot;
using System.Collections.Generic;

public class SoundSpecifics
{
    public int      agent_id;           // For referencing agent to play sound spatially
    public int      agent_type;          // If agent is monster, frequency and sound differ
    public int      colliding_material;
    public Vector2  position;
    public Vector2  speed;
    public float    distance_to_ground;

    // Store local reference of the last audio played on a certain audio sequence. Store reference as a list.
    public Dictionary<int, int> continuity_manager = new Dictionary<int, int>();
}