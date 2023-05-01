using System.Net.Sockets;

namespace kcgsound.source.AudioTest
{
    // Don't use entitas internal lists because they don't have deterministic order.
    public class AudioAssetList
    {
        public AudioAsset[] List;

        public int Length;

        // the capacity is just the Length of the list
        public int Capacity;

        public AudioAssetList()
        {
            List = new AudioAsset[4096];
            Capacity = List.Length;
        }


        public AudioAsset Add(AudioAsset asset)
        {
            // if we don't have enough space we expand the capacity.
            ExpandArray();


            int LastIndex = Length;
            List[LastIndex] = asset;
            asset.Index = Length;
            Length++;

            return List[LastIndex];
        }


        public AudioAsset Get(int Index)
        {
            return List[Index];
        }


        public void Remove(int particleIndex)
        {
            AudioAsset asset = List[particleIndex];

            List[particleIndex] = List[Length - 1];
            List[particleIndex].Index = particleIndex;

            // Node(Joao): Destroy can't be above component access. If it's, there is going to be a bug when particleIndex == Length - 1
            //asset.Destroy();

            List[Length - 1] = null;
            Length--;
        }

        // used to grow the list
        private void ExpandArray()
        {
            if (Length >= Capacity)
            {
                int NewCapacity = Capacity + 4096;

                // make sure the new capacity 
                // is bigget than the old one
                //Utils.Assert(NewCapacity > Capacity);
                Capacity = NewCapacity;
                System.Array.Resize(ref List, Capacity);
            }
        }
        
    }

}