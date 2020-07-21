using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace AnagramSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            IAnagramSolver anagramSolver = new BusinessLogic.AnagramSolver() { 
                WordRepository = new WordRepository()};


            while (true)
            {
                Console.WriteLine("\nĮrašykite žodį");
                var word = Console.ReadLine();

                Console.WriteLine("Anagramos:");

                var anagrams = anagramSolver.GetAnagrams(word);

                if (anagrams.Count==0)
                {
                    Console.WriteLine("Šis žodis anagramų neturi");
                }
                foreach (var item in anagrams)
                {
                    Console.WriteLine(item);
                }
                Console.ReadLine();
            }
            

        }
    }
}
