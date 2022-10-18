
using System.Security.Cryptography;
using System.Text;


namespace InformationProtection.Encoders
{
    public partial class GRSAEncoderClass : IEncoder
    {

        RSACryptoServiceProvider rSACryptoService;

        private string privateKey;

        private string publicKey;

        private int keysize = 128;


        public string PublicKey
        {
            get { return publicKey; }
            set { publicKey = value; }
        }
        public string PrivateKey
        {
            get { return privateKey; }
            set { privateKey = value; }

        }

        public int KeySize
        {
            get { return keysize; }
            set { 
                keysize = value; 
                updateCryptoService();
            }
        }


        private GRSAEncoderClass()
        {

            updateCryptoService();

        }

        public void updateCryptoService()
        {
            rSACryptoService = new RSACryptoServiceProvider();
            privateKey = rSACryptoService.ToXmlString(true);
            publicKey = rSACryptoService.ToXmlString(false);
            
        }

        public string Decode(string textToDecode)
        {
            string decodedText = "";

            rSACryptoService.FromXmlString(privateKey);

            using (var rsa = new RSACryptoServiceProvider(keysize*8))
            {
                try
                {
                    var base64Encrypted = textToDecode;

                    // server decrypting data with private key                    
                    rsa.FromXmlString(privateKey);

                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }

        }

        public string Encode(string textToEncode)
        {
            var testData = Encoding.UTF8.GetBytes(textToEncode);
            using (var rsa = new RSACryptoServiceProvider(keysize*8))
            {
                try
                {
                    // client encrypting data with public key issued by server                    
                    rsa.FromXmlString(publicKey.ToString());

                    var encryptedData = rsa.Encrypt(testData, true);

                    var base64Encrypted = Convert.ToBase64String(encryptedData);

                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }

           
        }


    }
}
