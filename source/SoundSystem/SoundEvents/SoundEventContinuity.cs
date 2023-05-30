using System.Collections.Generic;

public class SoundEventContinuity
{
    // Store local reference of which was the last audio played on audio sequence. Store reference as a list
    public Dictionary<string, int> storedIndex = new Dictionary<string, int>();
}