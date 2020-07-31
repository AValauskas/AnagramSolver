using AnagramSolver.Contracts.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Console.UI
{
    public class UIClass
    {
        private readonly IAnagramSolver _apiService;
        private UIClass(IAnagramSolver apiService)
        {
            this._apiService = apiService;
        }
        public static async Task Create(IAnagramSolver requestService)
        {
            var uiClass = new UIClass(requestService);
            await uiClass.ProcessAnagramManager();
        }
        private async Task ProcessAnagramManager()
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
                var anagrams = await _apiService.GetAnagrams(myWord);
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
