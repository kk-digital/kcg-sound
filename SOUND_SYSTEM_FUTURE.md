   
    public void ReproduceRandomly(AudioAsset list){}
    //  Reproduces a random (AudioAsset) from (List<AudioAsset> list)

    public int ReproduceInSequence(List<AudioAsset> list, int last){}
    //  Reproduces (int last)++ from (List<AudioAsset>)
    //  Returns (int last)++ so class that called keeps the reference of last sound played

    public int PseudoRandomly(List<AudioAsset> list, int last){}
    //  Reproduces a random (AudioAsset) that is != (int last) from (List<AudioAsset>)
    //  Returns (int last)++ so class that called keeps the reference of last sound played

    public void LowPass(float time, float frequency){}
    //  Applies a low cut from 12k to (float frequency) in a given (float time)

    public void Crossfade(AudioAsset asset_1, AudioAsset asset_2, float time){}
    //  Cross fades (AudioAsset asset_1) and (AudioAsset asset_1) in a given (float time)

    public void ApplyReverb(int bus, float size, float length)
    //  Updates the reverb of (int bus) with a given (float size) and (float length)
    //  If (float size == 0f) and (float length == 0f), deactive reverb

    public void Attenuation(int bus, float db, float time)
    //  Attenuates the given (int bus) with (float db) for (float time) seconds

    public void IsOccluded(Node2D asset_1, Node2D asset_2)
    //  Attenuates and applies a low cut to sound bus if raycast between (Node2D asset_1) and (Node2D asset_2) returns a body

    // STEP SOUND
    //  Would work with a raycast pointing to Vector2.DOWN
    //  Tile ID represent different materials, therefore different soundstep

    // ARRAY OF SOUNDS
    // Does sound x belong to an Array?