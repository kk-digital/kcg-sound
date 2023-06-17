using Godot;
using SoundApi;

namespace AudioEmitter;
public static class AudioEmitterMovement 
{
    const float footsteps_frecuency = 0.5f;

    public static void Initialize(SoundSpecifics sound_specifics)
    {
        Audio.audio_playback_manager.CreateTimerWithSoundSpecifics(Walk, sound_specifics);
    }

    public static void  Walk(SoundSpecifics sound_specifics)
    {
        // Overall sound ID
        string sound_id = "03";                         // Walk Action ID = 03
        sound_id += sound_specifics.agent_type;         // TODO: Plus AGENT TYPE, HUMAN ID = 00
        sound_id += sound_specifics.colliding_material; // PLUS MATERIAL TYPE, CONCRETE ID = 00
        // Example ID = 400

        Audio.audio_playback_manager.PlayAudioInSequence(sound_id, sound_specifics.agent_id); // Play the sequence
    }

    public static void  Idle(SoundSpecifics sound_specifics)
    {
        Audio.audio_playback_manager.PlayAudioInSequence("010000", sound_specifics.agent_id);
    }

    public static void  Jump(SoundSpecifics sound_specifics)
    {
        Audio.audio_playback_manager.PlayAudioInSequence("020000", sound_specifics.agent_id);
    }

    public static void  DoubleJump(SoundSpecifics sound_specifics)
    {
        Audio.audio_playback_manager.PlayAudioInSequence("030000", sound_specifics.agent_id);
    }

    public static void  Fall(SoundSpecifics sound_specifics)
    {
        GD.Print("audio_emitter : -> 'Fall'");
        //Audio.audio_playback_manager.PlayAudioInSequence(4, sound_specifics.agent_id);
    }
}