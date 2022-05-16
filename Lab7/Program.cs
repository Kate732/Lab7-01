using System;
using System.Collections.Generic;
using static Lab7.Check;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> incorectWords = CheckOnTypos(Console.ReadLine());
            foreach (var word in incorectWords)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine(incorectWords.Count);
        }
    }
}