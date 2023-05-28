using Godot;
using System;

namespace AudioSystem;
public class AudioPlaybackManager
{
    RandomNumberGenerator rng = new RandomNumberGenerator();

    public AudioStreamPlayer2D ReturnAvailableChannel(int assetId, int entityId)
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
        return CreateNewChannel(entityId);
    }

    public AudioStreamPlayer2D CreateNewChannel(int entityId)
    {
        AudioStreamPlayer2D audioStreamPlayer       = new AudioStreamPlayer2D();
        audioStreamPlayer.Name  = "AudioTmp";
        EntityList.entities[entityId].AddChild(audioStreamPlayer);
        GD.Print("All audio channels on use, creating a new one");
        return audioStreamPlayer;
    }

    public void PlayAudio(int assetId, int entityId)
    {
        //GD.Print("Playing audio ID : " + assetId + " / " + SoundApi.list.allAudio[assetId].FileName);
        AudioStreamPlayer2D audioStreamPlayer = ReturnAvailableChannel(assetId, entityId);
    
        if(audioStreamPlayer.Playing)
        {
            audioStreamPlayer = new AudioStreamPlayer2D();
            audioStreamPlayer.Name = "AudioTmp";
            EntityList.entities[entityId].AddChild(audioStreamPlayer);
        }
        audioStreamPlayer.Stream = SoundApi.list.allAudio[assetId].Stream;
        audioStreamPlayer.Bus = "Master";
        audioStreamPlayer.Play();
    }

    public void PlayAudioRandomly(string assetList, int entityId)
    {
        int listLength  = SoundApi.listManager.GetListLength(assetList);
        int chosen      = rng.RandiRange(0,listLength);
        int chosenId    = SoundApi.list.allAudioLists[assetList][chosen].SoundID;
        SoundApi.playbackManager.PlayAudio(chosenId, entityId);
        GD.Print("Playing audio randomly ID : " + chosen + " / " + SoundApi.list.allAudio[chosen].FileName);
    }
    
    public int PlayAudioInSequence(string assetList, int entityId, int playIndex = 0)
    {
        int listLength  = SoundApi.listManager.GetListLength(assetList);
        if(playIndex    > listLength)
        {  
            playIndex   = 0; 
            GD.Print("Reseting value, list length " + listLength);
        }

        int assetId     = SoundApi.list.allAudioLists[assetList][playIndex].SoundID;

        GD.Print("Playing audio in sequence / ID : ", assetId, " / Name : " + SoundApi.list.allAudio[assetId].FileName);
        GD.Print("New PlayIndex : " + playIndex + " / List length : " + listLength);
        PlayAudio(assetId, entityId);
        return playIndex+1;
    }

    public int PlayAudioPseudoRandomly(string assetList, int entityId, int playIndex = 0)
    {
        int listLength  = SoundApi.listManager.GetListLength(assetList);
        int chosen      = rng.RandiRange(0,listLength);
        while(chosen    == playIndex)
        {chosen         = rng.RandiRange(0,listLength);}
        int chosenId    = SoundApi.list.allAudioLists[assetList][chosen].SoundID;

        GD.Print("Playing audio in pseudo-sequence ID : " + chosen + " / " + SoundApi.list.allAudio[chosen].FileName);
        return chosen;
    }

    public void SpecifyMaterial(string soundFileName, int assetMaterialId)
    {
        switch(assetMaterialId) // Difference between materials must be in filename at the end for this to work
        {
            case 0:
                soundFileName += "Rock";
                break;
            case 1:
                soundFileName += "Wall";
                break;
            case 2:
                soundFileName += "Metal";
                break;
            default:
                break;
        }
    }

    public void PlayInPitch()
    {
    }

    public void IsOccluded(Node2D asset_1, Node2D asset_2)
    {
    }
}
