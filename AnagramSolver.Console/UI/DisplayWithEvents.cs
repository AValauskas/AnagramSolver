using AnagramSolver.Contracts.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Console.Extensions;

namespace AnagramSolver.Console.UI
{
    public class DisplayWithEvents : IDisplay
    {
        public delegate void PrintHandler(string message);

        public event PrintHandler Print;

        private readonly IAnagramSolver _apiService;
        public DisplayWithEvents(IAnagramSolver apiService)
        {
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
                var anagramsobject = await _apiService.GetAnagrams(myWord);
                var anagrams = anagramsobject.Select(x => x.Word).ToList();               
                DisplayAnagrams(anagrams);

            }
            OnPrint("Darbas baigtas!");
        }   

        public void FormattedPrint(List<string> anagrams)
        {
            foreach (var item in anagrams)
            {
                OnPrint(item);
            }
            OnPrint(anagrams.CapitalizeFirstLetter());
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
                FormattedPrint(anagrams.ToList());
            }
            System.Console.ReadLine();
        }
        private void OnPrint(string message)
        {
            Print?.Invoke(message);
        }
    }
}
