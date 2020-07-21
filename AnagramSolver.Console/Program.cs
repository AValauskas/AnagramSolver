using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver
{
    class Program
    {
     
        static void Main(string[] args)
        {
            ProcessAnagramManager();
        }

        public static void ProcessAnagramManager()
        {
            IAnagramSolver anagramSolver = new BusinessLogic.AnagramSolver()
            {
                WordRepository = new WordRepository()
            };            
            bool exit = false;
            string myWord;
            while (!exit)
            {
                myWord = WriteWord();
                if (myWord == "x" || myWord == "X")
                {
                    break;
                }
                if (!CheckIfLengthCorrect(myWord))
                {
                    Console.WriteLine("\nĮvestas žodis per trumpas");
                    continue;
                }
                Console.WriteLine("Anagramos:\n");
                var anagrams = anagramSolver.GetAnagrams(myWord);
                DisplayAnagrams(anagrams);

            }
            Console.WriteLine("Darbas baigtas!");
        }

        public static string WriteWord()
        {
            Console.WriteLine("\nĮrašykite žodį/žodžių junginį arba X-norėdami išeiti");
            string myWord = Console.ReadLine();
            return myWord;
        }

        public static bool CheckIfLengthCorrect(string myWord)
        {
            int minLength = Helper.GetMinLength();
            if (myWord.Length >= minLength)
                return true;
            return false;   
        }

        public static void DisplayAnagrams(IList<string> anagrams)
        {
            if (anagrams == null)
            {
                Console.WriteLine("Šis žodis anagramų neturi");   
            }
            else
            {
                foreach (var item in anagrams)
                {
                    Console.WriteLine(item);
                }
            }
            Console.ReadLine();
        }
    }
}
