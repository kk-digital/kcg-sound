using Godot;
using System;

namespace AudioSystem;
public class AudioPlaybackManager
{
    public void PlayAudio(int assetId, Node2D who)
    {
        assetId = Math.Clamp(assetId, 0, SoundApi.listManager.GetListLength() - 1);
        GD.Print("Playing audio ID : " + assetId + " / " + SoundApi.list[assetId].FileName);

        //var audioStreamPlayer = new AudioStreamPlayer2D();   // Assuming entity doesn't have an AudioPlayer2D
    
        AudioStreamPlayer2D audioStreamPlayer = (AudioStreamPlayer2D) who.GetChild(0);
        audioStreamPlayer.Stream = SoundApi.list[assetId].Stream;
        audioStreamPlayer.Bus = "Master";

        //who.AddChild(audioStreamPlayer);                      // Assuming entity doesn't have an AudioPlayer2D
        //who.GetChild(0);                                      // Supposing AudioPlayer always be on index 0

        audioStreamPlayer.Play();
        //return audioStreamPlayer;
    }
}
