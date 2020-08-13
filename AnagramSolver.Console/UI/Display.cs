using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Console.UI
{
    public delegate void Print(string message);
    public delegate void FormPrint(List<string> anagrams);
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
                //  var anagramsobject = await _apiService.GetAnagrams(myWord);
                //var anagrams = anagramsobject.Select(x => x.Word).ToList();
                var anagrams = new List<string>() { "alus", "sula" };
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
                FormPrint form = new FormPrint(CapitalizeFirstLetter);
                FormattedPrint(form, anagrams.ToList());
                //foreach (var item in anagrams)
                //{
                //    print(item);
                //}
            }
            System.Console.ReadLine();
        }
        //public void ProcessAnagrams(List<string> anagrams)
        //{
        //    foreach (var item in anagrams)
        //    {
        //        print(item);
        //    }
        //}

        public void FormattedPrint(FormPrint form, List<string> anagrams)
        {

            foreach (var item in anagrams)
            {
                print(item);
            }
            form(anagrams);
        }

        public void CapitalizeFirstLetter(List<string> anagrams)
        {
            string letter="";
            anagrams.ForEach(x => letter += x.First());
            print(letter);
        }
    }
}
