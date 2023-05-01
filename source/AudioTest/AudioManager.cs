using System;
using Player;
using Godot;

namespace kcgsound.source.AudioTest
{
    public class AudioManager

    {

        public static AudioStreamPlayer2D PlayAudio(int assetId, Node parent)
        {
            var audioStreamPlayer = new AudioStreamPlayer2D();
            audioStreamPlayer.Stream = GD.Load("res://testaudioassets/Atmo.ogg") as AudioStreamOggVorbis;
            audioStreamPlayer.Bus = "Master";
            parent.AddChild(audioStreamPlayer);
            audioStreamPlayer.Play();
            return audioStreamPlayer;           
        }
    }
}
