

namespace InformationProtection.Encoders
{
    public partial class GWernamEncoderClass : IEncoder
    {

        private int seed = 64;

        short [] encodingCombo;
        int comboSize = 150;

        public int Seed
        {
            get { return seed; }
            set { 
                seed = value; 
                generateCombo();
            }
        }

        private GWernamEncoderClass()
        {
            encodingCombo = new short[comboSize];
            generateCombo();
        }

        private void generateCombo()
        {
            Random random = new Random(Seed);
            for(int i=0; i < comboSize; i++)
            {
                encodingCombo[i] = (short)random.Next();
            }
        }

        public string Decode(string textToDecode)
        {
            string decodedText = "";

            for (int i = 0; i < textToDecode.Length; i++)
            {
                char c = textToDecode[i];
                c = (char)(c ^ encodingCombo[i%comboSize]);
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


                c = (char) (c ^ encodingCombo[i%comboSize]);
                encodedText += c;
            }

            return encodedText;
        }
    }
}
