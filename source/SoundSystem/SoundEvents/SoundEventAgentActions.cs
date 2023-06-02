using System;
using Godot;
using System.Collections.Generic;

namespace AudioSystem;
public static class SoundEventAgentAction
{
    // Specific behaviour of each sound trigger
    // REPLACED WITH AUDIO EMITTER AGENT MOVEMENT

    public static void  Test(int entityId)
    {
        string action = "Test";
        //SoundApi.audio_playback_manager.PlayAudioInSequence(entityId, action);
        //SoundApi.audio_playback_manager.PlayAudioPseudoRandomly(entityId, action);
        SoundApi.audio_playback_manager.PlayAudioRandomly(entityId, action);
                            
    }

    public static void  Walk(int entityId)
    {
        SoundApi.audio_playback_manager.PlayAudioInSequence(entityId, "Walk");
    }

    public static void  Run(int entityId)
    {
        SoundApi.audio_playback_manager.PlayAudioInSequence(entityId, "Run");
    }

    public static void  Jump(int entityId)
    {
        SoundApi.audio_playback_manager.PlayAudioInSequence(entityId, "Jump");
    }
}

// More sounds to do
/*  

    public static void  Land(int entityId)
    {
        SoundApi.audio_playback_manager.PlayAudioInSequence(entityId, "Land", playIndex);
    }

    public static void  Stab(int entityId)
    {
        SoundApi.audio_playback_manager.PlayAudioInSequence(entityId, "Stab", playIndex);
    }

    public static void  Stash(int entityId)
    {
        SoundApi.audio_playback_manager.PlayAudioByName(entityId, "Stash");
    }

    public static void  WeaponFire(int entityId, string weaponName)
    {
        SoundApi.audio_playback_manager.PlayAudioInSequence(entityId, "WeaponFire"+weaponName, playIndex);
    }

    public static void  WeaponAim(int entityId)
    {
        SoundApi.audio_playback_manager.PlayAudioByName(entityId, "Aim");
    }

    public static void  WeaponUnAim(int entityId)
    {
        SoundApi.audio_playback_manager.PlayAudioByName(entityId, "UnAim");
    }

    public static void  JetpackStart(int entityId)
    {
        //SoundApi.audio_playback_manager.PlayAudioInSequence("Walk_", entityId, playIndex);
        // Sound that starts in loop and mantains
    }

    public static void  JetpackEnd(int entityId)
    {
        //SoundApi.audio_playback_manager.PlayAudioInSequence("Walk_", entityId, playIndex);
        // Loops cuts on fades out while triggering an end sound
    }

    public static void  Bounce(int entityId, int bounceMaterialIndex) //
    {
        string objectThrown = EntityList.entities[entityId].materialType;
        string assetName = SoundApi.audio_playback_manager.SpecifyMaterial(objectThrown, bounceMaterialIndex);
        if(playIndex == 0) // Initial object impact
        {
            SoundApi.audio_playback_manager.PlayAudioByName(assetName+"Start", entityId);
        }
        else
        {
            SoundApi.audio_playback_manager.PlayAudioInSequence(assetName, entityId, playIndex);
        }
    }
}
*/