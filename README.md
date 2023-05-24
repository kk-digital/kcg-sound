# kcg-sound

Sound assets directory                                @ root/assets/test_audio_assets
Sound list (when loaded by system)                    @ AudioList.assets

# SoundApi > Master Manager

// listManager         # Gathers data of sound assets     @ AudioAssetList

    / GetListLength          ()                                           -> int
    / GetAssetFromId         (int Id)                                     -> AudioAsset
    / GetAssetFilePathFromId (int Id)                                     -> string
    / SoundNameToId          (string name)                                -> int

// assetManager        # Register sounds & loads them     @ AudioAssetManager

    / RegisterSoundAsset    (string path, string name, string iconPath)   -> AudioStreamPlayer2D
    / AddAsset              (AudioAsset asset)                            -> void
    / RemoveAsset           (AudioAsset asset)                            -> void
    / LoadSoundAssets       ()                                            -> void
    / LoadAudioAsset        (int assetId)                                 -> void

// playbackManager     # Plays sounds                     @ AudioPlaybackManager

    / PlayAudio              (int assetId, Node2D who)                    -> void

// Can't make Godot inherited functions as non-partial, an error stops from compiling
// Can't make a function update every frame if is not _Process (Godot inherited)
// If it's _Process, it needs to be a node inherited for it to work
// A SoundState was asked for, but didn't understood how it fit in the pieces, so I merged it with the api.