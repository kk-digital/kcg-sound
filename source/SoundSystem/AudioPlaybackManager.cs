using Godot;
using System;
//using Player;

namespace AudioSystem;
public class AudioPlaybackManager
{
        public AudioStreamPlayer2D PlayAudio(int assetId, Node2D who)
        {
            GD.Print("Playing audio : " + SoundApi.list[assetId].FileName);

            var audioStreamPlayer = new AudioStreamPlayer2D();
            audioStreamPlayer.Stream = SoundApi.list[assetId].Stream;
            audioStreamPlayer.Bus = "Master";

            //who.AddChild(audioStreamPlayer); // Supposing there's no AudioPlayer
            who.GetChild(0); // Supposing AudioPlayer always be on index 0
            audioStreamPlayer.Play();
            GD.Print("Sucesfully played " + SoundApi.list[assetId].FileName);

            return audioStreamPlayer;
        }
}
