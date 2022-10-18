namespace InformationProtection.Encoders
{
    internal class GEncoder
    {
        public int EncodeMethodType { get; set; }

        public string Key { get; set; }

        public string Encode(string textToEncode)
        {
            string encodedText = "";
            IEncoder encoder = null;
            switch (EncodeMethodType)
            {
                case 0:
                    encoder = GReplacementEncodeMethodClass.Instance;
                    break;
                case 1:
                    encoder = GCesarEncodeMethodClass.Instance;
                    break;
                case 2:
                    encoder = GPolibiyEncoderClass.Instance;
                    break;
                case 3:
                    encoder = GGronsfeldEncoderClass.Instance;
                    break;
                case 4:
                    encoder = GBigramEncoderClass.Instance;
                    break;
                case 5:
                    encoder = GWernamEncoderClass.Instance;
                    break;
                case 6:
                    encoder = GDESEncoderClass.Instance;
                    break;
                case 7:
                    encoder = GRSAEncoderClass.Instance;
                    break;
                default:
                    encodedText = "encoder not found";
                    return encodedText; break;
            }
            encodedText = encoder.Encode(textToEncode);
            return encodedText;
        }

        public string Decode(string textToDecode)
        {
            string decodedText = "";
            IEncoder encoder = null;
            switch (EncodeMethodType)
            {
                case 0:
                    encoder = GReplacementEncodeMethodClass.Instance;
                    break;
                case 1:
                    encoder = GCesarEncodeMethodClass.Instance;
                    break;
                case 2:
                    encoder = GPolibiyEncoderClass.Instance;
                    break;
                case 3:
                    encoder = GGronsfeldEncoderClass.Instance;
                    break;
                case 4:
                    encoder = GBigramEncoderClass.Instance;
                    break;
                case 5:
                    encoder = GWernamEncoderClass.Instance;
                    break;
                case 6:
                    encoder = GDESEncoderClass.Instance;
                    break;
                case 7:
                    encoder = GRSAEncoderClass.Instance;
                    break;

                default:
                    decodedText = "encoder not found";
                    return decodedText; break;
            }
            decodedText = encoder.Decode(textToDecode);
            return decodedText;
        }

    }
}
