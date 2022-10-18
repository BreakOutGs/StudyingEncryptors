using InformationProtection.Encoders;

namespace InformationProtection;

public partial class RSAConceptPage : ContentPage
{

	GRSAEncoderClass rsaEncryptor;

	string firstUserPublicRSAKey = "";
	string firstUserPrivateRSAKey = "";
	string firstUserReceivedRSAKey = "";
	string firstUserMessage = "";
	string firstUserEncryptedMessage = "";
	string firstUserReceivedMessage = "";

    string secondUserPublicRSAKey = "";
    string secondUserPrivateRSAKey = "";
    string secondUserReceivedRSAKey	;
	string secondUserMessage = "";
	string secondUserEncryptedMessage = "";
	string secondUserReceivedMessage = "";



    public RSAConceptPage()
	{
		InitializeComponent();
		rsaEncryptor = GRSAEncoderClass.Instance;
	}

	private void GenerateKeyButtonClicked1(Object sender, EventArgs args)
	{
		rsaEncryptor.updateCryptoService();
		firstUserPrivateRSAKey = rsaEncryptor.PrivateKey;
		firstUserPublicRSAKey = rsaEncryptor.PublicKey;
		FirstUserPublicKeyEntry.Text = firstUserPublicRSAKey;
		FirstUserPrivateKeyEntry.Text = firstUserPrivateRSAKey;
	}
    private void GenerateKeyButtonClicked2(Object sender, EventArgs args)
    {
        rsaEncryptor.updateCryptoService();
        secondUserPrivateRSAKey = rsaEncryptor.PrivateKey;
        secondUserPublicRSAKey = rsaEncryptor.PublicKey;
        SecondUserPublicKeyEntry.Text = secondUserPublicRSAKey;
        SecondUserPrivateKeyEntry.Text = secondUserPrivateRSAKey;
    }
	private void ReceiveKeyButtonClicked1(Object sender, EventArgs args)
	{
		firstUserReceivedRSAKey = secondUserPublicRSAKey;
		FirstUserReceivedPublicKeyEntry.Text = firstUserReceivedRSAKey;
		InterceptedMessageEntry.Text = firstUserReceivedRSAKey;
	}
    private void ReceiveKeyButtonClicked2(Object sender, EventArgs args)
    {
        secondUserReceivedRSAKey = firstUserPublicRSAKey;
        SecondUserReceivedPublicKeyEntry.Text = secondUserReceivedRSAKey;
		InterceptedMessageEntry.Text = secondUserReceivedRSAKey;
    }
    private void SendMessageButtonClicked1(Object sender, EventArgs args)
	{
		firstUserMessage = FirstUserMessageEntry.Text;

		rsaEncryptor.PublicKey = firstUserReceivedRSAKey.ToString();
		firstUserEncryptedMessage = rsaEncryptor.Encode(firstUserMessage);

		InterceptedMessageEntry.Text = firstUserEncryptedMessage;

		rsaEncryptor.PrivateKey = secondUserPrivateRSAKey;
		secondUserReceivedMessage = rsaEncryptor.Decode(firstUserEncryptedMessage);

        SecondUserReceivedMessage.Text = secondUserReceivedMessage;
    }
    private void SendMessageButtonClicked2(Object sender, EventArgs args)
    {
        secondUserMessage = SecondUserMessageEntry.Text;

        rsaEncryptor.PublicKey = secondUserReceivedRSAKey.ToString();
        secondUserEncryptedMessage = rsaEncryptor.Encode(secondUserMessage);

        InterceptedMessageEntry.Text = secondUserEncryptedMessage;

        rsaEncryptor.PrivateKey = firstUserPrivateRSAKey;
        firstUserReceivedMessage = rsaEncryptor.Decode(secondUserEncryptedMessage);

		FirstUserReceivedMessage.Text = firstUserReceivedMessage;

    }



}