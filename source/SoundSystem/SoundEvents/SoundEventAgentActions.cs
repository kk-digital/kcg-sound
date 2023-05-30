using System;
using Godot;
using System.Collections.Generic;

namespace AudioSystem;
public static class SoundEventAgentAction
{
    // A List<AudioAsset> has to be done for each sequence as it's what represents

    public static void  Test(int entityId)
    {
        string action = "Test";
        //SoundApi.playbackManager.PlayAudioInSequence(entityId, action);
        //SoundApi.playbackManager.PlayAudioPseudoRandomly(entityId, action);
        SoundApi.playbackManager.PlayAudioRandomly(entityId, action);
                            
    }

    public static void  Walk(int entityId)
    {
        SoundApi.playbackManager.PlayAudioInSequence(entityId, "Walk");
    }

    public static void  Run(int entityId)
    {
        SoundApi.playbackManager.PlayAudioInSequence(entityId, "Run");
    }

    public static void  Jump(int entityId)
    {
        SoundApi.playbackManager.PlayAudioInSequence(entityId, "Jump");
    }
}
/*
    public static void  Land(int entityId)
    {
        SoundApi.playbackManager.PlayAudioInSequence(entityId, "Land", playIndex);
    }

    public static void  Stab(int entityId)
    {
        SoundApi.playbackManager.PlayAudioInSequence(entityId, "Stab", playIndex);
    }

    public static void  Stash(int entityId)
    {
        SoundApi.playbackManager.PlayAudioByName(entityId, "Stash");
    }

    public static void  WeaponFire(int entityId, string weaponName)
    {
        SoundApi.playbackManager.PlayAudioInSequence(entityId, "WeaponFire"+weaponName, playIndex);
    }

    public static void  WeaponAim(int entityId)
    {
        SoundApi.playbackManager.PlayAudioByName(entityId, "Aim");
    }

    public static void  WeaponUnAim(int entityId)
    {
        SoundApi.playbackManager.PlayAudioByName(entityId, "UnAim");
    }

    public static void  JetpackStart(int entityId)
    {
        //SoundApi.playbackManager.PlayAudioInSequence("Walk_", entityId, playIndex);
        // Sound that starts in loop and mantains
    }

    public static void  JetpackEnd(int entityId)
    {
        //SoundApi.playbackManager.PlayAudioInSequence("Walk_", entityId, playIndex);
        // Loops cuts on fades out while triggering an end sound
    }

    public static void  Bounce(int entityId, int bounceMaterialIndex) //
    {
        string objectThrown = EntityList.entities[entityId].materialType;
        string assetName = SoundApi.playbackManager.SpecifyMaterial(objectThrown, bounceMaterialIndex);
        if(playIndex == 0) // Initial object impact
        {
            SoundApi.playbackManager.PlayAudioByName(assetName+"Start", entityId);
        }
        else
        {
            SoundApi.playbackManager.PlayAudioInSequence(assetName, entityId, playIndex);
        }
    }



}

*/

/*
- trigger condition
- switch group (material?)
- a string name for each audio event
- ex. AgentActions_GrenadeThrow
*/