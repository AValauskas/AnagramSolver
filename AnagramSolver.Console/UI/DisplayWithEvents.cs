using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Console.UI
{
    public class DisplayWithEvents : IDisplay
    {
        public delegate void PrintHandler(string message);

        public event PrintHandler Print;

        private Action<List<string>> FormPrint;

        private readonly IAnagramSolver _apiService;
        public DisplayWithEvents(IAnagramSolver apiService)
        {
            FormPrint = CapitalizeFirstLetter;
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
                    OnPrint("\nĮvestas žodis per trumpas");
                    continue;
                }
                OnPrint("Anagramos:\n");
                //  var anagramsobject = await _apiService.GetAnagrams(myWord);
                //var anagrams = anagramsobject.Select(x => x.Word).ToList();
                var anagrams = new List<string>() { "alus", "sula" };
                DisplayAnagrams(anagrams);

            }
            OnPrint("Darbas baigtas!");
        }

        private string WriteWord()
        {
            OnPrint("\nĮrašykite žodį/žodžių junginį arba X-norėdami išeiti");
            string myWord = System.Console.ReadLine();
            return myWord;
        }

        private void DisplayAnagrams(IList<string> anagrams)
        {
            if (anagrams == null)
            {
                OnPrint("Šis žodis anagramų neturi");
            }
            else
            {
                FormattedPrint(FormPrint, anagrams.ToList());
            }
            System.Console.ReadLine();
        }

        public void FormattedPrint(Action<List<string>> form, List<string> anagrams)
        {
            foreach (var item in anagrams)
            {
                OnPrint(item);
            }
            FormPrint(anagrams);
        }

        public void CapitalizeFirstLetter(List<string> anagrams)
        {
            string letter = "";
            anagrams.ForEach(x => letter += x.First());
            OnPrint(letter);
        }

        private void OnPrint(string message)
        {
            if (Print != null)
            {
                Print(message);
            }
        }
    }
}
