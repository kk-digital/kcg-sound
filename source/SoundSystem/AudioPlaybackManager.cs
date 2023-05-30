using Godot;
using System;

namespace AudioSystem;
public class AudioPlaybackManager
{
    RandomNumberGenerator rng = new RandomNumberGenerator();

    public AudioStreamPlayer2D ReturnAvailableChannel(int entityId)
    {
        Godot.Collections.Array<Godot.Node> channels = EntityList.entities[entityId].GetNode<Godot.Node2D>("Audio").GetChildren(); // Get all the audio players
        int                                 channelsSize = channels.Count-1;
        AudioStreamPlayer2D                 audioStreamPlayer;

        // Check all present audio channels for available ones
        for(int each = 0; each <= channelsSize; each++)
        {
            AudioStreamPlayer2D channel = (AudioStreamPlayer2D) channels[each];
            if(!channel.Playing)
            {
                audioStreamPlayer = channel;
                GD.Print("Returning available audio player");
                return audioStreamPlayer;
            }
        }

        // If no available ones create a new temporal channel
        return CreateNewChannel(entityId);
    }

    public AudioStreamPlayer2D CreateNewChannel(int entityId)
    {
        // Need to make the temporal part work yet
        AudioStreamPlayer2D audioStreamPlayer = new AudioStreamPlayer2D();
        audioStreamPlayer.Name                = "AudioTmp";
        EntityList.entities[entityId].AddChild(audioStreamPlayer);
        GD.Print("All audio channels on use, creating a new one");
        return audioStreamPlayer;
    }

    public int CheckContinuity(int entityId, string action)
    {
        // Check the local continuity of audio asset list in entity
        var soundContinuity = EntityList.entities[entityId].continuityManager;
        int playIndex;
        if(soundContinuity.storedIndex.ContainsKey(action))
        {
            playIndex = soundContinuity.storedIndex[action];
        }
        else
        {
            soundContinuity.storedIndex.Add(action, 0);
            playIndex = 0;
            GD.Print("Continuity list created for " + action);
        }
        return playIndex;
    }

    public void UpdateContinuity(int entityId, string action, int newIndex)
    {
        var soundContinuity = EntityList.entities[entityId].continuityManager;
        soundContinuity.storedIndex[action] = newIndex;
    }

    public void PlayAudio(int entityId, int assetId)
    {
        GD.Print("Playing audio ID : " + assetId + " / " + SoundApi.list.allAudio[assetId].FileName);
        AudioStreamPlayer2D audioStreamPlayer = ReturnAvailableChannel(entityId);
        
        if(audioStreamPlayer.Playing)
        {
            audioStreamPlayer      = new AudioStreamPlayer2D();
            audioStreamPlayer.Name = "AudioTmp";
            EntityList.entities[entityId].AddChild(audioStreamPlayer);
        }
        audioStreamPlayer.Stream   = SoundApi.list.allAudio[assetId].Stream;
        audioStreamPlayer.Bus      = "Master";
        audioStreamPlayer.Play();
    }

    public void PlayAudioByName(int entityId, string assetFileName)
    {
        
        AudioStreamPlayer2D audioStreamPlayer = ReturnAvailableChannel(entityId);
                        int assetId           = SoundApi.listManager.SoundNameToId(assetFileName);
        GD.Print("Playing audio ID : " + assetId + " / " + assetFileName);

        if(audioStreamPlayer.Playing)
        {
            audioStreamPlayer      = new AudioStreamPlayer2D();
            audioStreamPlayer.Name = "AudioTmp";
            EntityList.entities[entityId].AddChild(audioStreamPlayer);
        }
        
        audioStreamPlayer.Stream   = SoundApi.list.allAudio[assetId].Stream;
        audioStreamPlayer.Bus      = "Master";
        audioStreamPlayer.Play();
    }

    public void PlayAudioRandomly(int entityId, string assetList)
    {
        int listLength  = SoundApi.listManager.GetListLength(assetList);
        int chosen      = rng.RandiRange(0,listLength);
        int assetId    = SoundApi.list.allAudioLists[assetList][chosen].SoundID;

        GD.Print("Playing audio randomly ID : " + chosen + " / " + SoundApi.list.allAudio[chosen].FileName);
        SoundApi.playbackManager.PlayAudio(entityId, assetId);
        
    }

    public void PlayAudioInSequence(int entityId, string assetList)
    {
        int listLength   = SoundApi.listManager.GetListLength(assetList); 
        int currentIndex = CheckContinuity(entityId, assetList); // Current sound index to play
    
        if(currentIndex  > listLength) // If it's the last one of the list reset to 0
        {  
            currentIndex = 0;
        }

        int assetId      = SoundApi.list.allAudioLists[assetList][currentIndex].SoundID;
        int newIndex     = currentIndex+1; // Next sound index to play

        GD.Print("Playing audio in sequence / ID : ", assetId, " / Name : " + SoundApi.list.allAudio[assetId].FileName);
        PlayAudio(entityId, assetId);
        UpdateContinuity(entityId, assetList, newIndex);
    }

    public void PlayAudioPseudoRandomly(int entityId, string assetList)
    {
        int listLength   = SoundApi.listManager.GetListLength(assetList);
        int currentIndex = CheckContinuity(entityId, assetList);
        int chosen       = rng.RandiRange(0,listLength);
        while(chosen     == currentIndex)
        {chosen          = rng.RandiRange(0,listLength);}
        int assetId      = SoundApi.list.allAudioLists[assetList][chosen].SoundID;

        GD.Print("Playing audio in pseudo-sequence ID : " + chosen + " / " + SoundApi.list.allAudio[chosen].FileName);
        PlayAudio(entityId, assetId);
        UpdateContinuity(entityId, assetList, chosen);
    }

    public string SpecifyMaterial(string soundFileName, int assetMaterialId)
    {
        switch(assetMaterialId) // Difference between materials must be in filename at the end for this to work
        {
            case 0:
                soundFileName += "Rock";
                return soundFileName;
            case 1:
                soundFileName += "Wall";
                return soundFileName;
            case 2:
                soundFileName += "Metal";
                return soundFileName;
            default:
                return soundFileName;
        }
    }

    public void PlayInPitch()
    {
    }

    public void IsOccluded(Node2D asset_1, Node2D asset_2)
    {
    }
}
