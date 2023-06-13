using Godot;
using AudioSystem;
using System;
using System.Collections.Generic;

namespace AudioEmitter;
public class SoundManager
{
    //public AudioEmitterMovement audio_movement      = new AudioEmitterMovement();
    public AudioEmitterWeapons  audio_weapons       = new AudioEmitterWeapons();

    /*
    // Sound Specifics
    Is calculated within the agent emitter, and when it calls the sound it is passed with it. It includes:
    - public int      agent_id;             [0] Its used to play the sound in the specific agent, as sound is spatial.
    - public int      colliding_material;   [1] If its relevant for the sound called, it will be used to modify the sound played.
    - public Vector2  position;             [2] Current position
    - public Vector2  speed;                [3] Current speed
    - public float    distance_to_ground;   [4]
    */

    public static void Walk(SoundSpecifics sound_specifics)
    {
        AudioEmitterMovement.Walk(sound_specifics);
    }

    public static void Shoot_SMG()
    {

    }

    public static void Explosion(SoundSpecifics sound_specifics)
    {
        
    }

}

