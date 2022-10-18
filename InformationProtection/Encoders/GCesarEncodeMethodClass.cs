

using InformationProtection.Encoders;

namespace InformationProtection
{
    public partial class GCesarEncodeMethodClass : IEncoder
    {
        int replaceIncrement;

        public int ReplaceIncrement
        {
            get { return replaceIncrement; }
            set { replaceIncrement = value; }
        }

        private GCesarEncodeMethodClass()
        {
            replaceIncrement = 4;
        }
       
        
        public string Encode(string textToEncode)
        {
            string encodedText = "";
            foreach(char c in textToEncode)
            {
                encodedText += (char)((int)c + replaceIncrement);
            }
            return encodedText;
          
        }
        public string Decode(string textToDecode)
        {
            string decodedText = "";
            foreach (char c in textToDecode)
            {
                decodedText += (char)((int)c - replaceIncrement);
            }
            return decodedText;
        }
    }
}
