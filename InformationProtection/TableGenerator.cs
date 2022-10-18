using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProtection
{
    internal class TableGenerator
    {
        static readonly char[,] EnglishTable = {
        {'A', 'B', 'C', 'D', 'E'},
        {'F', 'G', 'H', 'I', 'K' },
        {'L', 'M', 'N', 'O', 'P'},
        {'Q', 'R', 'S', 'T', 'U'},  
        {'V', 'W', 'X', 'Y', 'Z' },
        {'a', 'b', 'c', 'd', 'e'},
        {'f', 'g', 'h', 'i', 'k' },
        {'l', 'm', 'n', 'o', 'p'},
        {'q', 'r', 's', 't', 'u'},
        {'v', 'w', 'x', 'y', 'z' },
        {'1', '2', '3', '4', '5' },
        {'6', '7', '8', '9', '0' },
        };

        static readonly char[,] UkrainianTable2 =
        {
            {'а', 'б', 'в', 'г', 'ґ', 'д', 'е' },
            { 'є', 'ж', 'з', 'и', 'і', 'ї', 'й' },
            { 'к', 'л', 'м', 'н', 'о', 'п', 'р' },
            { 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч'},
            { 'ш', 'щ', 'ь', 'ю', 'я', '.',','} 
        };

        static readonly char[,] UkrainianTable =
        {
            {'А', 'Б', 'В', 'Г', 'Ґ'},
            {'Д', 'Е', 'Є', 'Ж', 'З' },
            {'И', 'І', 'Ї', 'Й', 'К' },
            {'Л', 'М', 'Н', 'О', 'П' },
            {'Р', 'С', 'Т', 'У', 'Ф' },
            {'Х', 'Ц', 'Ч', 'Ш', 'Щ' },
            {'Ь', 'Ю', 'Я', '1', '2'},
            {'а', 'б', 'в', 'г', 'ґ'},
            {'д', 'е', 'є', 'ж', 'з' },
            {'и', 'і', 'ї', 'й', 'к' },
            {'л', 'м', 'н', 'о', 'п' },
            {'р', 'с', 'т', 'у', 'ф' },
            {'х', 'ц', 'ч', 'ш', 'щ' },
            {'ь', 'ю', 'я', '3', '4'},
            {'5', '6', '7', '8', '9' }
        };
        
        public static readonly char[,] CombinedTable = {
            {'A', 'B', 'C', 'D', 'E'},
            {'F', 'G', 'H', 'I', 'K' },
            {'L', 'M', 'N', 'O', 'P'},
            {'Q', 'R', 'S', 'T', 'U'},
            {'V', 'W', 'X', 'Y', 'Z' },
            {'a', 'b', 'c', 'd', 'e'},
            {'f', 'g', 'h', 'i', 'k' },
            {'l', 'm', 'n', 'o', 'p'},
            {'q', 'r', 's', 't', 'u'},
            {'v', 'w', 'x', 'y', 'z' },
            {'1', '2', '3', '4', '5' },
            {'6', '7', '8', '9', '0' },
            {'А', 'Б', 'В', 'Г', 'Ґ'},
            {'Д', 'Е', 'Є', 'Ж', 'З' },
            {'И', 'І', 'Ї', 'Й', 'К' },
            {'Л', 'М', 'Н', 'О', 'П' },
            {'Р', 'С', 'Т', 'У', 'Ф' },
            {'Х', 'Ц', 'Ч', 'Ш', 'Щ' },
            {'Ь', 'Ю', 'Я', '1', '2'},
            {'а', 'б', 'в', 'г', 'ґ'},
            {'д', 'е', 'є', 'ж', 'з' },
            {'и', 'і', 'ї', 'й', 'к' },
            {'л', 'м', 'н', 'о', 'п' },
            {'р', 'с', 'т', 'у', 'ф' },
            {'х', 'ц', 'ч', 'ш', 'щ' },
            {'ь', 'ю', 'я', '3', '4'},
            {'5', '6', '7', '8', '9' }
        };

        public static char[,] getEnglishTable() => EnglishTable;
        public static char[,] getUkrainianTable() => UkrainianTable;

        public static char[,] generateTableWithKey(char[,] tableChars, char[] keyRow)
        {
            int rowSize = tableChars.GetLength(1);
            int colSize = tableChars.GetLength(0);
            int currentRow=1, currentCol=0;
            char[,] generatedTable = new char[tableChars.GetLength(0), tableChars.GetLength(1)];
            if (keyRow.Length != rowSize) return null;

            for(int i =0; i < rowSize; i++)
            {
                generatedTable[0,i] = keyRow[i];
            }

            for (int i = 0; i < colSize; i++)
            {
                for (int j = 0; j < rowSize; j++)
                {
                    char c = tableChars[i,j];
                    if (keyRow.FirstOrDefault(ch=>ch==c,' ')==' ')
                    {
                        generatedTable[currentRow, currentCol] = tableChars[i, j];
                        currentCol++;
                        if(currentCol >= rowSize)
                        {
                            currentCol = 0;
                            currentRow++;
                        }
                        if (currentRow == colSize) return generatedTable;
                    }
                }
            }

            return generatedTable;

        }
        private static bool isCharInArray(char c, char[,] chars)
        {
            for(int i = 0; i < chars.GetLength(0); i++)
            {
                for(int j = 0; j < chars.GetLength(1); j++)
                if (chars[i,j] == c) return true;
            }
            return false;
        }

        public static char[,] replaceTable(int replaceIndex, char[,] tableToReplace)
        {
            int rowSize = tableToReplace.GetLength(1);
            int colSize = tableToReplace.GetLength(0);

            char[,] newTable = new char[colSize, rowSize];

            int startingIndex = replaceIndex % (rowSize * colSize);
            int rowIndex = replaceIndex / rowSize;
            int colIndex = replaceIndex % rowSize;

            for(int i = 0; i < colSize; i++)
            {
                for(int j =0;j<rowSize; j++)
                {
                    int replaceAddition = i * rowSize + j +replaceIndex;
                    newTable[i,j] = tableToReplace[(replaceAddition/rowSize)%colSize,replaceAddition%rowSize];
                }
            }

            return newTable;
            
        }

        public static Tuple<int, int> findCharInTable(char c, char[,] searchTable)
        {
            for (int i = 0; i < searchTable.GetLength(0); i++)
            {
                for (int j = 0; j < searchTable.GetLength(1); j++)
                {
                    if (c == searchTable[i, j])
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }

            return Tuple.Create(-1, -1);
        }
        public static Tuple<int,int,int> findCharInTwoTable(char c, char[,] firstTable, char[,] secondTable)
        {
            Tuple<int, int, int> triplePosition = new Tuple<int, int, int>(-1,-1,-1);//1 pos-> table, 2pos->row, 3pos->col
            for (int i = 0; i < firstTable.GetLength(0); i++)
            {
                for (int j = 0; j < firstTable.GetLength(1); j++)
                {
                    if (c == firstTable[i, j])
                    {
                        int tableIndex = 1;
                        int rowIndex = i;
                        int colIndex = j;
                        return Tuple.Create(tableIndex,rowIndex, colIndex);
                    }
                }
            }
            for (int i = 0; i < secondTable.GetLength(0); i++)
            {
                for (int j = 0; j < secondTable.GetLength(1); j++)
                {
                    if (c == secondTable[i, j])
                    {
                        int tableIndex = 2;
                        int rowIndex = i;
                        int colIndex = j;
                        return Tuple.Create(tableIndex, rowIndex, colIndex);
                    }
                }
            }

            return triplePosition;
        }

    }
}
