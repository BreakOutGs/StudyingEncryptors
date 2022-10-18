using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProtection.Encoders
{
    public partial class GBigramEncoderClass : IEncoder
    {

        char[,] firstTable;
        char[,] secondTable;

        private int seed = 32;


        public int Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        private GBigramEncoderClass()
        {
            updateTables();
        }
        private void updateTables()
        {
            Random random = new Random(Seed);

            firstTable = TableGenerator.getUkrainianTable();
            secondTable = TableGenerator.getEnglishTable();

            firstTable = TableGenerator.replaceTable(random.Next(),firstTable);
            secondTable = TableGenerator.replaceTable(random.Next(),secondTable);

        }


        public string Decode(string textToDecode)
        {
            string decodedString = "";
            int i = 0;
            for (; i < textToDecode.Length-1; i += 2)
            {
                char firstChar = textToDecode[i];
                char secondChar = textToDecode[i + 1];

                Tuple<int, int, int> firstCharTriplePosition =
                    TableGenerator.findCharInTwoTable(firstChar, firstTable, secondTable);

                Tuple<int, int, int> secondCharTriplePosition =
                    TableGenerator.findCharInTwoTable(secondChar, firstTable, secondTable);

                if (firstCharTriplePosition.Item1 == -1 || secondCharTriplePosition.Item1 == -1)
                {
                    decodedString += firstChar;
                    decodedString += secondChar;
                }
                else
                {
                    if (firstChar == secondChar)
                    {
                        if (firstCharTriplePosition.Item1 == 1)
                        {
                            firstChar = secondTable[
                                firstCharTriplePosition.Item2 % secondTable.GetLength(0),
                                firstCharTriplePosition.Item3 % secondTable.GetLength(1)];
                            decodedString += firstChar;
                            decodedString += firstChar;
                            continue;
                        }
                        else
                        {
                            firstChar = secondTable[
                               firstCharTriplePosition.Item2 % firstTable.GetLength(0),
                               firstCharTriplePosition.Item3 % firstTable.GetLength(1)];
                            decodedString += firstChar;
                            decodedString += firstChar;
                            continue;
                        }
                    }
                    if (firstCharTriplePosition.Item1 == 1 && secondCharTriplePosition.Item1 == 2)
                    {
                        int rowIndex, colIndex;
                        rowIndex = secondCharTriplePosition.Item2;
                        colIndex = firstCharTriplePosition.Item3;
                        firstChar = secondTable[rowIndex, colIndex];

                        rowIndex = firstCharTriplePosition.Item2;
                        colIndex = secondCharTriplePosition.Item3;
                        secondChar = firstTable[rowIndex, colIndex];
                    }
                    if (firstCharTriplePosition.Item1 == 2 && secondCharTriplePosition.Item1 == 1)
                    {
                        int rowIndex, colIndex;
                        rowIndex = secondCharTriplePosition.Item2;
                        colIndex = firstCharTriplePosition.Item3;
                        firstChar = firstTable[rowIndex, colIndex];

                        rowIndex = firstCharTriplePosition.Item2;
                        colIndex = secondCharTriplePosition.Item3;
                        secondChar = secondTable[rowIndex, colIndex];
                    }
                    if (firstCharTriplePosition.Item1 == 1 && secondCharTriplePosition.Item1 == 1)
                    {
                        int rowIndex, colIndex;
                        rowIndex = secondCharTriplePosition.Item2;
                        colIndex = firstCharTriplePosition.Item3;
                        firstChar = firstTable[rowIndex, colIndex];

                        rowIndex = firstCharTriplePosition.Item2;
                        colIndex = secondCharTriplePosition.Item3;
                        secondChar = firstTable[rowIndex, colIndex];
                    }
                    if (firstCharTriplePosition.Item1 == 2 && secondCharTriplePosition.Item1 == 2)
                    {
                        int rowIndex, colIndex;
                        rowIndex = secondCharTriplePosition.Item2;
                        colIndex = firstCharTriplePosition.Item3;
                        firstChar = secondTable[rowIndex, colIndex];

                        rowIndex = firstCharTriplePosition.Item2;
                        colIndex = secondCharTriplePosition.Item3;
                        secondChar = secondTable[rowIndex, colIndex];
                    }
                    decodedString += firstChar;
                    decodedString += secondChar;
                }

            }

            if (i == textToDecode.Length - 1)
            {
                decodedString += textToDecode[i];
            }

            return decodedString;
        }

        public string Encode(string textToEncode)
        {
            string encodedString = "";
            int i = 0;
            for(; i < textToEncode.Length-1; i+=2)
            {
                char firstChar = textToEncode[i];
                char secondChar = textToEncode[i+1];

                Tuple<int, int, int> firstCharTriplePosition =
                    TableGenerator.findCharInTwoTable(firstChar, firstTable, secondTable);

                Tuple<int, int, int> secondCharTriplePosition =
                    TableGenerator.findCharInTwoTable(secondChar, firstTable, secondTable);

                if (firstCharTriplePosition.Item1 == -1 || secondCharTriplePosition.Item1 == -1)
                {
                    encodedString += firstChar;
                    encodedString += secondChar;
                }
                else
                {
                    if(firstChar==secondChar)
                    {
                        if (firstCharTriplePosition.Item1 == 1)
                        {
                            firstChar = secondTable[
                                firstCharTriplePosition.Item2 % secondTable.GetLength(0),
                                firstCharTriplePosition.Item3 % secondTable.GetLength(1)];
                            encodedString += firstChar;
                            encodedString += firstChar;
                            continue;
                        }
                        else
                        {
                            firstChar = secondTable[
                               firstCharTriplePosition.Item2 % firstTable.GetLength(0),
                               firstCharTriplePosition.Item3 % firstTable.GetLength(1)];
                            encodedString += firstChar;
                            encodedString += firstChar;
                            continue;
                        }
                    }
                    if(firstCharTriplePosition.Item1==1 && secondCharTriplePosition.Item1 == 2)
                    {
                        int rowIndex, colIndex;
                        rowIndex = secondCharTriplePosition.Item2;
                        colIndex = firstCharTriplePosition.Item3;
                        firstChar = secondTable[rowIndex, colIndex];

                        rowIndex = firstCharTriplePosition.Item2;
                        colIndex = secondCharTriplePosition.Item3;
                        secondChar = firstTable[rowIndex, colIndex];
                    }
                    if (firstCharTriplePosition.Item1 == 2 && secondCharTriplePosition.Item1 == 1)
                    {
                        int rowIndex, colIndex;
                        rowIndex = secondCharTriplePosition.Item2;
                        colIndex = firstCharTriplePosition.Item3;
                        firstChar = firstTable[rowIndex, colIndex];

                        rowIndex = firstCharTriplePosition.Item2;
                        colIndex = secondCharTriplePosition.Item3;
                        secondChar = secondTable[rowIndex, colIndex];
                    }
                    if (firstCharTriplePosition.Item1 == 1 && secondCharTriplePosition.Item1 == 1)
                    {
                        int rowIndex, colIndex;
                        rowIndex = secondCharTriplePosition.Item2;
                        colIndex = firstCharTriplePosition.Item3;
                        firstChar = firstTable[rowIndex, colIndex];

                        rowIndex = firstCharTriplePosition.Item2;
                        colIndex = secondCharTriplePosition.Item3;
                        secondChar = firstTable[rowIndex, colIndex];
                    }
                    if (firstCharTriplePosition.Item1 ==2  && secondCharTriplePosition.Item1 == 2)
                    {
                        int rowIndex, colIndex;
                        rowIndex = secondCharTriplePosition.Item2;
                        colIndex = firstCharTriplePosition.Item3;
                        firstChar = secondTable[rowIndex, colIndex];

                        rowIndex = firstCharTriplePosition.Item2;
                        colIndex = secondCharTriplePosition.Item3;
                        secondChar = secondTable[rowIndex, colIndex];
                    }
                    encodedString += firstChar;
                    encodedString += secondChar;
                }

            }
            if (i == textToEncode.Length - 1)
            {
                encodedString += textToEncode[i];
            }

            return encodedString;
        }
    }
}
