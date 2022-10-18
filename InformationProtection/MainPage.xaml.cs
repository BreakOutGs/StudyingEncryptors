using InformationProtection.Encoders;

namespace InformationProtection;

public partial class MainPage : ContentPage
{
	GEncoder gEncoder;
	List<string> EncryptMethodsList = new List<string>()
	{
        "Шифр перестановки",
		"Шифр заміни",
		"Шифр Полібія",
		"Шифр Гронсфельда",
		"Шифр Біграм",
		"Шифр Вернама",
		"Шифр DES",
		"Шифр RSA"
	};

	public MainPage()
	{
		InitializeComponent();
		gEncoder = new GEncoder();
		EncryptMethodPicker.ItemsSource = EncryptMethodsList;
		EncryptMethodPicker.SelectedIndex = 0;
	}

	private void OnEncodeBtnClicked(object sender, EventArgs e)
	{
		int EncoderType = EncryptMethodPicker.SelectedIndex;
		gEncoder.EncodeMethodType = EncoderType;
		OutputTextEditor.Text = gEncoder.Encode(InputTextEditor.Text);
		InputTextEditor.Text = "";
	}
    private void OnDecodeBtnClicked(object sender, EventArgs e)
    {
        int EncoderType = EncryptMethodPicker.SelectedIndex;
        gEncoder.EncodeMethodType = EncoderType;
        InputTextEditor.Text = gEncoder.Decode(OutputTextEditor.Text);
		OutputTextEditor.Text = "";
    }
}

