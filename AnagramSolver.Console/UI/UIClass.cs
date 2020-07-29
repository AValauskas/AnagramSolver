using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Console.UI
{
    public class UIClass
    {
        private readonly IRequestService _requestService;
        public UIClass(IRequestService requestService)
        {
            this._requestService = requestService;
            ProcessAnagramManager();
        }

        public void ProcessAnagramManager()
        {
            bool exit = false;
            string myWord;
            while (!exit)
            {
                myWord = WriteWord();
                if (myWord == "x" || myWord == "X")
                {
                    break;
                }
                if (!UILogic.CheckIfLengthCorrect(myWord))
                {
                    System.Console.WriteLine("\nĮvestas žodis per trumpas");
                    continue;
                }
                System.Console.WriteLine("Anagramos:\n");
                var anagrams = _requestService.GetAnagramRequest(myWord);
                DisplayAnagrams(anagrams);

            }
            System.Console.WriteLine("Darbas baigtas!");
        }

        private string WriteWord()
        {
            System.Console.WriteLine("\nĮrašykite žodį/žodžių junginį arba X-norėdami išeiti");
            string myWord = System.Console.ReadLine();
            return myWord;
        }

      

        private void DisplayAnagrams(IList<string> anagrams)
        {
            if (anagrams == null)
            {
                System.Console.WriteLine("Šis žodis anagramų neturi");
            }
            else
            {
                foreach (var item in anagrams)
                {
                    System.Console.WriteLine(item);
                }
            }
            System.Console.ReadLine();
        }

    }
}
