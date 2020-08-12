using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Console.UI
{
    public delegate void Print(string message);
    public class Display : IDisplay
    {
        private Print print { get; set; }
        private readonly IAnagramSolver _apiService;
        public Display(Print printDelegate, IAnagramSolver apiService)
        {
            print = printDelegate;
            this._apiService = apiService;
        }       
        public async Task ProcessAnagramManager()
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
                    print("\nĮvestas žodis per trumpas");
                    continue;
                }
                print("Anagramos:\n");
                var anagramsobject = await _apiService.GetAnagrams(myWord);
                var anagrams = anagramsobject.Select(x => x.Word).ToList();
                DisplayAnagrams(anagrams);

            }
            print("Darbas baigtas!");
        }

        private string WriteWord()
        {
            print("\nĮrašykite žodį/žodžių junginį arba X-norėdami išeiti");
            string myWord = System.Console.ReadLine();
            return myWord;
        }

        private void DisplayAnagrams(IList<string> anagrams)
        {
            if (anagrams == null)
            {
                print("Šis žodis anagramų neturi");
            }
            else
            {
                foreach (var item in anagrams)
                {
                    print(item);
                }
            }
            System.Console.ReadLine();
        }
    }
}
