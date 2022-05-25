using System;
using System.Collections.Generic;
using static Lab7.Check;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Dictionary<string, List<string>> typosAndOffers = CheckOnTypos(Console.ReadLine());
            foreach (var word in typosAndOffers.Keys)
            {
                Console.WriteLine($"incorrect: {word}");
                foreach (var offer in typosAndOffers[word])
                {
                    Console.WriteLine(offer);
                }
            }
            
            Console.WriteLine(typosAndOffers.Keys.Count);
            */
            Console.WriteLine(FindLCS("good", "kood"));
        }
    }
}

// wednessay was quita goos