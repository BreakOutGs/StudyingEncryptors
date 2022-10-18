
using System.Linq;
using InformationProtection.Encoders;

namespace InformationProtection
{
    public enum PolibiyLanguage {
        Ukrainian,
        English
    }
    public partial class GPolibiyEncoderClass : IEncoder
    {

        char[,] _tableForEncoding = TableGenerator.getUkrainianTable();
        public PolibiyLanguage Language { get; set; } = PolibiyLanguage.Ukrainian;

        public readonly char[] defaultEnglishKey = { 's', 'P', 'e', 'k', 'a' };

        public readonly char[] defaultUkrainianKey = { 'С', 'П', 'е', 'к', 'а' };

        private char[] key = { 'С', 'П', 'е', 'к', 'а' };

        public char[] Key
        {
            get { return key; }
            set { if (isKeyCorrect(value)) key = value; }
        }


        public string Decode(string textToDecode)
        {
            string decodedText = "";

            switch (Language)
            {
                case PolibiyLanguage.English:
                    _tableForEncoding = TableGenerator.getEnglishTable();
                    _tableForEncoding = TableGenerator.generateTableWithKey(_tableForEncoding, key);
                    break;
                case PolibiyLanguage.Ukrainian:
                    _tableForEncoding = TableGenerator.getUkrainianTable();
                    _tableForEncoding = TableGenerator.generateTableWithKey(_tableForEncoding, key);
                    break;
                default: throw new NotImplementedException();
            }
            foreach (char c in textToDecode)
            {
                Tuple<int, int> posTuple = TableGenerator.findCharInTable(c,_tableForEncoding);
                int rowIndex = posTuple.Item1;
                int colIndex = posTuple.Item2;

                if (rowIndex == -1 || colIndex == -1)
                {
                    decodedText += c;
                    continue;
                }
                if (rowIndex <= 0)
                    rowIndex = _tableForEncoding.GetLength(0)-1;
                else rowIndex--;

                decodedText += _tableForEncoding[rowIndex, colIndex];

            }

            return decodedText;
        }

        public string Encode(string textToEncode)
        {
            string encodedText = ""; 

            switch (Language)
            {
                case PolibiyLanguage.English: 
                    _tableForEncoding = TableGenerator.getEnglishTable();
                    _tableForEncoding = TableGenerator.generateTableWithKey(_tableForEncoding,key);
                    break;
                case PolibiyLanguage.Ukrainian: 
                    _tableForEncoding = TableGenerator.getUkrainianTable();
                    _tableForEncoding = TableGenerator.generateTableWithKey(_tableForEncoding, key);
                    break;
                default: throw new NotImplementedException();
            }
            foreach (char c in textToEncode)
            {
                Tuple<int, int> posTuple = TableGenerator.findCharInTable(c,_tableForEncoding);
                int rowIndex = posTuple.Item1;
                int colIndex = posTuple.Item2;

                if (rowIndex == -1|| colIndex == -1)
                {
                    encodedText += c;
                    continue;
                }
                if (rowIndex >= _tableForEncoding.GetLength(0)-1) 
                    rowIndex = 0;
                else rowIndex++;

                encodedText += _tableForEncoding[rowIndex, colIndex];

            }

            return encodedText;
        }

        

        public bool isKeyCorrect(char[] key)
        {
            char[,] AlphabetTable;
            switch (Language)
            {
                case PolibiyLanguage.Ukrainian:
                    AlphabetTable = TableGenerator.getUkrainianTable();
                    break;
                case PolibiyLanguage.English:
                    AlphabetTable = TableGenerator.getEnglishTable();
                    break;
                default:
                    AlphabetTable = TableGenerator.getUkrainianTable();
                    break;
            }
            foreach(char c in key)
            {
                bool isInAlphabetTable = false;
                for(int i=0; i<AlphabetTable.GetLength(0); i++)
                {
                    for(int j=0; j<AlphabetTable.GetLength(1); j++)
                    {
                        if (c == AlphabetTable[i, j])
                        {
                            isInAlphabetTable= true;
                            goto doublebreak;
                        }
                    }
                }
            doublebreak:
                if(!isInAlphabetTable) return false;
            }
            return true;
        }

    }
}
