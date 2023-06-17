using SoundApi;

namespace AudioEmitter;
public class AudioEmitterWeapons
{
    public void GranadeBound(int agent_id)
    {
        Audio.audio_playback_manager.PlayAudio(0, agent_id);
    }

    public void GranadeThrown(int agent_id)
    {
        Audio.audio_playback_manager.PlayAudio(1, agent_id);
    }

    public void GranadeBounce(int agent_id)
    {
        Audio.audio_playback_manager.PlayAudio(2, agent_id);
    }

    public void GranadeExplosion(int agent_id)
    {
        Audio.audio_playback_manager.PlayAudio(5, agent_id);
    }

    public void EmptyClip(int agent_id)
    {
        Audio.audio_playback_manager.PlayAudio(0, agent_id);
    }

    public void ReloadPistol(int agent_id)
    {
        Audio.audio_playback_manager.PlaySequenceInPitch(1, "010000", agent_id);
    }

    public void ShootPistol(int agent_id)
    {
        Audio.audio_playback_manager.PlaySequenceInPitch(1, "020000", agent_id);
        //Audio.audio_playback_manager.PlayAudioInSequence("020000", agent_id);
    }
    public void ClipIn(int agent_id)
    {
        Audio.audio_playback_manager.PlayAudioInSequence("030000", agent_id);
    }
    public void ClipOut(int agent_id)
    {
        Audio.audio_playback_manager.PlayAudioInSequence("040000", agent_id);
    }
    public void HitTerrain(int agent_id)
    {
        
        Audio.audio_playback_manager.PlayAudio(11, agent_id);
    }
    public void HitAgent(int agent_id)
    {
        Audio.audio_playback_manager.PlayAudioInSequence("060000", agent_id);
    }
}