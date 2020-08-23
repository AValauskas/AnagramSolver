using AnagramSolver.Console.Extensions;
using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.Console.UI
{
    // public delegate void Print(string message);
    // public delegate void FormPrint(List<string> anagrams);

    public class Display : IDisplay
    {
        private Action<string> print;      
        //  private Print print { get; set; }  //->Delegate
        // private FormPrint form { get; set; } //->Delegate
        private readonly IAnagramSolver _apiService;
        public Display(Action<string> printDelegate, IAnagramSolver apiService) //Print printDelegate 
        {
            // FormPrint form = new FormPrint(CapitalizeFirstLetter); //->Delegate
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

        public void FormattedPrint(List<string> anagrams)
        {
            foreach (var item in anagrams)
            {
                print(item);
            }
            print(anagrams.CapitalizeFirstLetter());
        }

        //public void FormattedPrint(FormPrint form, List<string> anagrams)   //->Delegate
        //{

        //    foreach (var item in anagrams)
        //    {
        //        print(item);
        //    }
        //    form(anagrams);
        //}

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
                // FormattedPrint(form, anagrams.ToList());   //->Delegate
                FormattedPrint(anagrams.ToList());
            }
            System.Console.ReadLine();
        }
    }
}
