using Godot;
using System;
using System.Collections.Generic;

using AudioSystem;

namespace AudioSystem;
public static class SoundApi
{
    public static List<AudioAsset>      list            = new AudioList().assets;
    public static AudioListManager      listManager     = new AudioListManager();
    public static AudioAssetManager     assetManager    = new AudioAssetManager();
    public static AudioPlaybackManager  playbackManager = new AudioPlaybackManager();


    public static void Init()
    {
    }

    public static void Update()
    {
    }
}
