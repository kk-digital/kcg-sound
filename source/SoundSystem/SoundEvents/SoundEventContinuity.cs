using System.Collections.Generic;

public class SoundEventContinuity
{
    // Store local reference of the last audio played on a certain audio sequence. Store reference as a list.
    public Dictionary<string, int> storedIndex = new Dictionary<string, int>();
}