using Godot;
using AudioSystem;
using System.Collections.Generic;
using System;

namespace AudioEmitter;
public static class AudioEmitterMovement 
{
    const float footsteps_frecuency = 0.5f;

    public static void Initialize(SoundSpecifics sound_specifics)
    {
        SoundApi.audio_playback_manager.CreateTimer(Walk, sound_specifics);
    }

    public static void  Walk(SoundSpecifics sound_specifics)
    {
        GD.Print("audio_emitter : -> 'Walk'");

        // Overall sound ID
        int sound_id = 4;                               // WALK ACTION ID = 3
        sound_id += sound_specifics.agent_type;         // PLUS AGENT TYPE, HUMAN ID = 0
        sound_id += sound_specifics.colliding_material; // PLUS MATERIAL TYPE, CONCRETE ID = 0
        // Example ID = 400

        SoundApi.audio_playback_manager.PlayAudioInSequence(sound_id, sound_specifics.agent_id); // Play the sequence
    }

    public static void  Idle(SoundSpecifics sound_specifics)
    {
        GD.Print("audio_emitter : -> 'Idle'");
        SoundApi.audio_playback_manager.PlayAudioInSequence(1, sound_specifics.agent_id);
    }

    public static void  Jump(SoundSpecifics sound_specifics)
    {
        GD.Print("audio_emitter : -> 'Jump'");
        SoundApi.audio_playback_manager.PlayAudioInSequence(2, sound_specifics.agent_id);
    }

    public static void  DoubleJump(SoundSpecifics sound_specifics)
    {
        GD.Print("audio_emitter : -> 'DoubleJump'");
        SoundApi.audio_playback_manager.PlayAudioInSequence(0, sound_specifics.agent_id);
    }

    public static void  Fall(SoundSpecifics sound_specifics)
    {
        GD.Print("audio_emitter : -> 'Fall'");
        //SoundApi.audio_playback_manager.PlayAudioInSequence(4, sound_specifics.agent_id);
    }
}