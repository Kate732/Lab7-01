using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab7
{
    public class Check
    {
        public static Dictionary<string, List<string>> CheckOnTypos(string str)
        {
            List<string> glossary = File.ReadLines("words_list.txt").ToList();
            string[] wordsFromInput = ConvertToArrayOfWords(str);
            Dictionary<string, List<string>> typoToOffers = new Dictionary<string, List<string>>();
            foreach (var word in wordsFromInput)
            {
                if (glossary.Contains(word) == false)
                {
                    typoToOffers[word] = GetFiveOfferedWords(word, glossary);
                }
            }

            return typoToOffers;
        }
        
        private static List<string> GetFiveOfferedWords(string wrongWord, List<string> glossary)
        {
            Dictionary<string, int> wordsAndValues = new Dictionary<string, int>();
            foreach(var correctWord in glossary)
            {
                bool needToBeAdded = DoesWordNeedToBeAdded(wordsAndValues, wrongWord, correctWord);
                if (needToBeAdded)
                {
                    wordsAndValues[correctWord] = FindLCS(wrongWord, correctWord);
                }
            }
            List<string> fiveOfferedWords = new List<string>(wordsAndValues.Keys);

            return fiveOfferedWords;
        }

        private static bool DoesWordNeedToBeAdded(Dictionary<string, int> wordsAndValues, string wrongWord, string correctWord)
        {
            string minValueOfKey = null;
            int lcsOfTheOfferedWord = FindLCS(wrongWord, correctWord);
            if (wordsAndValues.Keys.Count < 5)
            {
                return true;
            }
            foreach (var key in wordsAndValues.Keys)
            {
                if (minValueOfKey == null || wordsAndValues[key] < wordsAndValues[minValueOfKey])
                {
                    minValueOfKey = key;
                }
            }

            if (minValueOfKey != null && wordsAndValues[minValueOfKey] < lcsOfTheOfferedWord)
            {
                wordsAndValues.Remove(minValueOfKey);
                return true;
            }
            
            return false;
        }

        //  && Math.Abs(wrongWord.Length - correctWord.Length) <= 2
        
        public static int FindLCS(string misstipedWord, string offeredWord)
        {
            // wednesday & endlessly => 5 (edesy)
            //     w e d n e s d a y
            //   0 0 0 0 0 0 0 0 0 0 
            // e 0 0 1 1 1 1 1 1 1 1
            // n 0 0 1 1 2 2 2 2 2 2 
            // d 0 0 1 2
            // l 0

            int mistypedLen = misstipedWord.Length;
            int offeredLen = offeredWord.Length;
            int[,] tableLCS = new int[mistypedLen + 1, offeredLen + 1];
            
            // first row and first column are expected to be filled with 0

            for (int m = 1; m <= mistypedLen; m++)
            {
                for (int o = 1; o <= offeredLen; o++)
                {
                    if (misstipedWord[m - 1] == offeredWord[o - 1])
                    {
                        tableLCS[m, o] = tableLCS[m - 1, o - 1] + 1;
                    }
                    else
                    {
                        tableLCS[m, o] = Math.Max(tableLCS[m - 1, o], tableLCS[m, o - 1]);
                    }
                }
            }

            return tableLCS[mistypedLen, offeredLen];
        }

        public static string[] ConvertToArrayOfWords(String str)
        {
            var arrayStr = str.ToCharArray();
            for (int i = 0; i < arrayStr.Length; i++)
            {
                if (i > 0 && i < arrayStr.Length - 1 && (arrayStr[i] == '-' || arrayStr[i] == '\'') &&
                    (char.IsLetter(arrayStr[i - 1]) && char.IsLetter(arrayStr[i + 1])))
                {
                    continue;
                }

                if (char.IsPunctuation(arrayStr[i]))
                {
                    arrayStr[i] = ' ';
                }
            }

            str = new string(arrayStr);
            string[] splited = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return splited;
        }
    }
}