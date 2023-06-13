using Godot;
using AudioSystem;
using System.Collections.Generic;
using System;

namespace AudioEmitter;
public class AudioEmitterWeapons
{
    public static void GranadeBound(int agent_id)
    {
        SoundApi.audio_playback_manager.PlayAudio(0, agent_id);
    }

    public static void GranadeThrown(int agent_id)
    {
        SoundApi.audio_playback_manager.PlayAudio(1, agent_id);
    }

    public static void GranadeBounce(int agent_id)
    {
        SoundApi.audio_playback_manager.PlayAudio(2, agent_id);
    }

    public static void GranadeExplosion(int agent_id)
    {
        SoundApi.audio_playback_manager.PlayAudio(5, agent_id);
        //SoundApi.audio_playback_manager.PlayAudioInSequence("000000", agent_id); // Play the sequence of sounds
    }
}