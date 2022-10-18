

using InformationProtection.Encoders;

namespace InformationProtection
{
    internal partial class GReplacementEncodeMethodClass : IEncoder
    {
        private int blockSize = 5;

        public int BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }
        }

        private int[] key = { 4,2,1,5,3};

        public int[] Key
        {
            get { return key; }
            set { key = value; }
        }


        private GReplacementEncodeMethodClass()
        {

        }

        public string Encode(string textToEncode)
        {
            string encodedText = "";
            foreach(string s in SliceTextToBlocks(blockSize,textToEncode))
            {
                encodedText += EncodeBlock(s);
            }
            return encodedText;
        }

        public List<string> SliceTextToBlocks(int blockSize, string textToSlice)
        {
            //int chCount = textToSlice.Length;
            List<string> blocks = new List<string>();
            int sliceStartPosition = 0;
            while (sliceStartPosition + blockSize <= textToSlice.Length)
            {
                blocks.Add(textToSlice.Substring(sliceStartPosition, blockSize));
                sliceStartPosition += blockSize;
            }
            int sliceSize = textToSlice.Length - sliceStartPosition;
            if (sliceStartPosition != textToSlice.Length && sliceSize<BlockSize)
            {
                string stringWithLesserThanBlockSize = textToSlice.Substring(sliceStartPosition, sliceSize);
                stringWithLesserThanBlockSize += new string(' ', BlockSize - sliceSize);
                blocks.Add(stringWithLesserThanBlockSize);
            }
                
            return blocks;
        }
        private string EncodeBlock(string textBlock)
        {
            string encodedBlock = "";

            for(int i=0; i < textBlock.Length; i++)
            {
                encodedBlock += textBlock[key[i]-1];
            }

            return encodedBlock;
        }

        public string Decode(string textToDecode)
        {
            string decodedText = "";
            foreach (string s in SliceTextToBlocks(blockSize, textToDecode))
            {
                decodedText += DecodeBlock(s);
            }
            return decodedText;
        }

        private string DecodeBlock(string textBlock)
        {
            string decodedBLock = "";

            for (int i = 0; i < blockSize; i++)
            {
                int pos = getPos(i+1,key);
                if(pos != -1)
                decodedBLock += textBlock[pos];
            }

            return decodedBLock;
        }

        public int getPos(int value, int[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (value == array[i]) return i;
            }
            return -1;
        }
        
       
    }
}
