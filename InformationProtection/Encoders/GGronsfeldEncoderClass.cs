using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationProtection.Encoders;

namespace InformationProtection
{
    public partial class GGronsfeldEncoderClass : IEncoder
    {

        private int[] codeArray;

        public int _seed = 64; //default seed
        public int Seed
        {
            get { return _seed; }
            set { 
                _seed = value; 
                generateCodeArray();
            }
        }


        private void generateCodeArray()
        {
            Random random = new Random(_seed);
            codeArray = new int[150];
            for(int i = 0; i < codeArray.Length; i++)
            {
                codeArray[i] = random.Next(1,100);
            }
        }

        private GGronsfeldEncoderClass()
        {
            generateCodeArray();
        }
        public string Decode(string textToDecode)
        {
            string decodedText = "";
            for (int i = 0; i < textToDecode.Length; i++)
            {
                char c = textToDecode[i];
                c = (char)((int)c - codeArray[i % codeArray.Length]);
                decodedText += c;
            }
            return decodedText;
        }

        public string Encode(string textToEncode)
        {
            string encodedText = "";
            for(int i = 0; i < textToEncode.Length; i++)
            {
                char c = textToEncode[i];
                c = (char)((int)c+codeArray[i % codeArray.Length]);
                encodedText += c;
            }
            return encodedText;
        }
    }
}
