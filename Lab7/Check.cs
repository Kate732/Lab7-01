using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab7
{
    public class Check
    {
        public static List<string> CheckOnTypos(string str)
        {
            string glossary = File.ReadAllText("words_list.txt");
            string[] words = ConvertToArrayOfWords(str);
            List<string> typos = new List<string>();
            foreach (var word in words)
            {
                if (glossary.Contains(word) == false)
                {
                    typos.Add(word);
                }
            }

            return typos;
        }

        private static string[] ConvertToArrayOfWords(String str)
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