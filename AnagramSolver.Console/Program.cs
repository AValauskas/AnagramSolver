using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Console.UI;
using AnagramSolver.Contracts.Interfaces;
using System.Threading.Tasks;

namespace AnagramSolver.Console
{
    class Program
    {
     
        static async Task Main(string[] args)
        {
            IAnagramSolver requestService = new BusinessLogic.AnagramSolver(new WordRepository());
           // IAnagramSolver requestService = new AnagramClient();
            await UIClass.Create(requestService);
        }

       
    }
}
