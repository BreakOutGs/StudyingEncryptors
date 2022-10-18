using InformationProtection.Encoders;
using System.Linq;

namespace InformationProtection;

public partial class SettingsPage : ContentPage
{

	List<String> alphabetSelect = new List<String>() { 
		"Український",
		"English"
	};
	//displacement params
	int DisplacementBlockSize=5;
	int[] DisplacementKey = { 4, 2, 1, 5, 3 };
	//replacement params
	int ReplacementNumber = 4;

	//Polibiy params
	int polibiyAlphabetIndex=0;
	const int PolibiyKeyLength = 5;
	char[] PolibiyKey = GPolibiyEncoderClass.Instance.defaultUkrainianKey;

	string errorsText = "";


	public SettingsPage()
	{
		InitializeComponent();
        PolibiyLanguagePicker.ItemsSource = alphabetSelect;
		DESCodeWordEntry.Placeholder = GDESEncoderClass.defaultKeyWord;

    }

	public void SaveSettingsBtnClick(Object sender, EventArgs args)
	{
		errorsText = "";
		DisplacementSettingsSave();
		ReplacementSettingsSave();
		PolibiySettingsSave();
		GronsfeldSettingsSave();
		BigramSettingsSave();
		WernamSettingsSave();
		DESSettingsSave();

		WrongSettingsLabel.Text = errorsText;
		UpdateDisplacementClassSettings();
		UpdateReplacementClassSettings();
    }

	private void DisplacementSettingsSave()
	{
		if(DisplacementBlockSizeEntry.Text != null &&
            DisplacementBlockSizeEntry.Text!="" )
			if(!int.TryParse(DisplacementBlockSizeEntry.Text,out DisplacementBlockSize)) 
		{
			errorsText += "\n Не коректний розмір блоку в методі перестановки \n";
				return;
		}

		if (DisplacementKeyEntry.Text != null &&
            DisplacementKeyEntry.Text != "")
		{
			String[] keyDigits = DisplacementKeyEntry.Text.Split(',');
			
			if(keyDigits.Length != DisplacementBlockSize)
			{
				errorsText += "Не відповідна кількість ключів в методі перестановки \n";
				return;
			}

			int[] keyCombo= new int[DisplacementBlockSize];
			
			for(int i=0;i<keyDigits.Length;i++)
			{
				int digit = 0;
				if (!int.TryParse(keyDigits[i], out digit)){
					errorsText += "В ключі перестановки неправильне число \n";
					return;
				}
				if(digit <=0 || digit > DisplacementBlockSize)
				{
					errorsText += $"В ключі перестановки потрібні бути цілі числа від(1,{DisplacementBlockSize})\n";
				}

				keyCombo[i] = digit;
            }
			if (isCorrectCombination(DisplacementBlockSize, keyCombo))
			{
				DisplacementKey = keyCombo;
			}
			else errorsText += "Комбінація перестановки не коректна \n";
		}
        if (DisplacementBlockSizeEntry.Text != null &&
            DisplacementBlockSizeEntry.Text != "")
		{
            DisplacementBlockSizeEntry.Placeholder = DisplacementBlockSizeEntry.Text;
            DisplacementBlockSizeEntry.Text = "";
		}
        if (DisplacementKeyEntry.Text != null &&
          DisplacementKeyEntry.Text != "")
        {
            DisplacementKeyEntry.Placeholder = DisplacementKeyEntry.Text;
            DisplacementKeyEntry.Text = "";
        }
        

	}

	private void ReplacementSettingsSave()
	{
		if (ReplacementKeyEntry.Text!="" && ReplacementKeyEntry.Text!=null)
		{
			int replacementNumber = 0;
			if (!int.TryParse(ReplacementKeyEntry.Text, out replacementNumber))
			{
				errorsText += "Не коректне число \n";
				return;
			}
			if(replacementNumber <= 0)
			{
				errorsText += "Введіть число більше нуля \n";
				return;
			}
			ReplacementNumber = replacementNumber;
			ReplacementKeyEntry.Placeholder = ReplacementKeyEntry.Text;
			ReplacementKeyEntry.Text = "";
        }
	}

	private void PolibiySettingsSave()
	{
		GPolibiyEncoderClass polibiyEncoder = GPolibiyEncoderClass.Instance;
		//todo: alphabet select, key entry
		int newSelectedAlpbetIndex = PolibiyLanguagePicker.SelectedIndex;
		
		if (newSelectedAlpbetIndex != (int)polibiyEncoder.Language)
		{
			polibiyEncoder.Language = (PolibiyLanguage)newSelectedAlpbetIndex;
		}

		if(PolibiyKeyEntry.Text!=null && PolibiyKeyEntry.Text != "")
		{
            char[] newKey = PolibiyKeyEntry.Text.ToCharArray();
            newKey = newKey.Where(ch =>ch!=',').ToArray<char>();

            if (newKey.Length != PolibiyKeyLength)
            {
                errorsText += "Невірна довжина ключу в Полібія\n";
                return;
            }

            if (polibiyEncoder.isKeyCorrect(newKey))
            {
                polibiyEncoder.Key = newKey;
				PolibiyKeyEntry.Placeholder = PolibiyKeyEntry.Text;
				PolibiyKeyEntry.Text = "";
            }
			else
			{
				errorsText += "Не коректний ключ до Полібія \n";
			}
        }

		
	}

	private void GronsfeldSettingsSave()
	{
		int newSeed = 64;
		GGronsfeldEncoderClass gronsfeldEncoderClass = GGronsfeldEncoderClass.Instance;
		if(GronsfeldSeedEtry.Text!=null && GronsfeldSeedEtry.Text!="")
		if (int.TryParse(GronsfeldSeedEtry.Text, out newSeed))
		{
			gronsfeldEncoderClass.Seed = newSeed;
				GronsfeldSeedEtry.Placeholder = GronsfeldSeedEtry.Text;
				GronsfeldSeedEtry.Text = "";
		}
		else errorsText += "Неправильний Seed, введіть ціле число \n";
	}

	private void BigramSettingsSave()
	{
		GBigramEncoderClass gBigramEncoder = GBigramEncoderClass.Instance;
		int bigramSeed = 0;
	
		if(BigramSeedEntry.Text!=null && BigramSeedEntry.Text!="")
		{
            if (int.TryParse(BigramSeedEntry.Text, out bigramSeed))
            {
                gBigramEncoder.Seed = bigramSeed;
				BigramSeedEntry.Placeholder = BigramSeedEntry.Text;
				BigramSeedEntry.Text = "";
            }
            else errorsText += "Введений Seed шифру Біграм не є цілим числом\n";
        }
		
	}
    private void WernamSettingsSave()
    {
        GWernamEncoderClass gWernamEncoder = GWernamEncoderClass.Instance;
        int wernamSeed = 0;

        if (WernamSeedEntry.Text != null && WernamSeedEntry.Text != "")
        {
            if (int.TryParse(WernamSeedEntry.Text, out wernamSeed))
            {
                gWernamEncoder.Seed = wernamSeed;
                WernamSeedEntry.Placeholder = WernamSeedEntry.Text;
                WernamSeedEntry.Text = "";
            }
            else errorsText += "Введений Seed шифру Вернама не є цілим числом\n";
        }

    }

	private void DESSettingsSave()
	{
		if(DESCodeWordEntry.Text!=null && DESCodeWordEntry.Text != "")
		{
			string newCodeWord = DESCodeWordEntry.Text;
			DESCodeWordEntry.Placeholder = newCodeWord;
			DESCodeWordEntry.Text = "";
			GDESEncoderClass.Instance.KeyWord = newCodeWord;
		}
	}

	private void RSASettingSave()
	{
		GRSAEncoderClass gRSAEncoderClass = GRSAEncoderClass.Instance;
		if(RSAPrivateKeyEntry.Text!=null && RSAPrivateKeyEntry.Text != "")
		{
			RSAPrivateKeyEntry.Placeholder = RSAPrivateKeyEntry.Text;
			gRSAEncoderClass.PrivateKey = RSAPrivateKeyEntry.Text;
			RSAPrivateKeyEntry.Text = "";
		}
        if (RSAPublicKeyEntry.Text != null && RSAPublicKeyEntry.Text != "")
        {
            RSAPublicKeyEntry.Placeholder = RSAPublicKeyEntry.Text;
            gRSAEncoderClass.PublicKey = RSAPublicKeyEntry.Text;
            RSAPublicKeyEntry.Text = "";
        }
    }

	private void GenerateRSAKeys(Object sender, EventArgs eventArgs)
	{
		GRSAEncoderClass gRSAEncoder = GRSAEncoderClass.Instance;
		gRSAEncoder.updateCryptoService();
		RSAPrivateKeyEntry.Text = gRSAEncoder.PrivateKey;
		RSAPublicKeyEntry.Text = gRSAEncoder.PublicKey;
	}

    private void UpdateDisplacementClassSettings()
	{
		GReplacementEncodeMethodClass gReplacementEncodeMethod = GReplacementEncodeMethodClass.Instance;
		gReplacementEncodeMethod.BlockSize = DisplacementBlockSize;
		gReplacementEncodeMethod.Key = DisplacementKey;
	}

	private void UpdateReplacementClassSettings()
	{
		GCesarEncodeMethodClass gCesarEncodeMethod = GCesarEncodeMethodClass.Instance;
		gCesarEncodeMethod.ReplaceIncrement = ReplacementNumber;
	}

    private bool isCorrectCombination(int BlockSize, int[] keyCombo)
    {
        if (BlockSize != keyCombo.Length) return false;

        for (int i = 0; i < keyCombo.Length; i++)
        {
            for (int j = i + 1; j < keyCombo.Length; j++)
            {
                if (i == j) return false;
            }
        }

        return true;

    }

	


}