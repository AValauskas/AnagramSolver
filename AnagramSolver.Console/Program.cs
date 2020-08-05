using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Console.UI;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Data;
using System.Threading.Tasks;

namespace AnagramSolver.Console
{
    class Program
    {
     
        static async Task Main(string[] args)
        {
            AdditionalWordsInsertClass.InsertWords();
            //IAnagramSolver requestService = new BusinessLogic.AnagramSolver(new WordRepository());
            IAnagramSolver requestService = new AnagramClient();
            await UIClass.Create(requestService);
        }

       
    }
}
