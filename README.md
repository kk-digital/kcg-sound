# kcg-sound

Sound assets directory                                @ root/assets/test_audio_assets
Sound list (when loaded by system)                    @ AudioList.assets

# SoundApi > Master Manager

// listManager              # Gathers data of sound assets     @ AudioAssetList

    / GetListLength          ()                                           -> int
    / GetAssetFromId         (int Id)                                     -> AudioAsset
    / GetAssetFilePathFromId (int Id)                                     -> string
    / SoundNameToId          (string name)                                -> int

// assetManager             # Register sounds & loads them       @ AudioAssetManager

    / RegisterSoundAssetList    (string path, string name, string iconPath)   -> AudioStreamPlayer2D
        Creates an audio list, accessed at SoundApi.list.allAudioLists[] by an unique string Id. All audio assets in list are also accesible in SoundApi.list.allAudio by its global Id.
        This becomes useful when becomes needed to reproduce a sound sequence.

    / RegisterSoundAsset
        Register the audio asset as class AudioAsset. This requiers a FilePath, FileName, Icon and a Stream, and will generate a SoundID (from which will be identified in SoundApi.list.allAudio)

    / AddAsset                  (AudioAsset asset)                            -> void
    / RemoveAsset               (AudioAsset asset)                            -> void
    / LoadAllSoundAssets        ()                                            -> void
    / LoadSoundStream           (int assetId)                                 -> void

// playbackManager          # Plays sounds                     @ AudioPlaybackManager

    
    / PlayAudioRandomly
        Reproduces a random *AudioAsset* from *List<AudioAsset> list*

    / PlayAudioInSequence
        Reproduces *int last++* from *List<AudioAsset>*
        Returns *int last++* so class that called keeps the reference of last sound played

    / PlayAudioPseudoRandomly
        Reproduces a random *AudioAsset* that is not the last one played from *List<AudioAsset>*
        Returns *int last++* so class that called keeps the reference of last sound played

    / PlayInPitch               // TBD
        Reproduces the sound with +1st pitch variation (on a scale if given) until reaching a treshhold and setting back to 0st

    / IsOccluded                // TBD
        Attenuates and applies a low cut to sound bus if raycast between (Node2D asset_1) and (Node2D asset_2) returns a body

    / SpecifyMaterial
        Switches the assigned list to match assgined material Id

    / CheckContinuity           (int entityId, string action)               -> int
    / UpdateContinuity          (int entityId, string action, int newIndex) -> void
    / PlayAudio                 (int assetId, Node2D who)                   -> void
    / ReturnAvailableChannel
    / CreateNewChannel

// soundEventTools          # Affects bus playback with various effects // TBD

    / LowPass
        Applies a low cut from 12k to (float frequency) in a given (float time)

    / Crossfade
        Cross fades (AudioAsset asset_1) and (AudioAsset asset_1) in a given (float time)

    / ApplyReverb
        Updates the reverb of (int bus) with a given *float size* and *float length*
        If (float size == 0f) and (float length == 0f), deactive reverb

    / Attenuation
        Attenuates the given *int bus* with *float db* for *float time* seconds

# SoundEvents

// SoundEventAgentAction    # Plays the behaviour expected from certain action


# Local Api > Contained by entity

// continuityManager        # Keeps track of sound played for sequences

// Can't make Godot inherited functions as non-partial, an error stops from compiling
// Can't make a function update every frame if is not _Process (Godot inherited)
// If it's _Process, it needs to be a node inherited for it to work
// A SoundState was asked for, but didn't understood how it fit in the pieces, so I merged it with the api.