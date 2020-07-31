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
            IAnagramSolver requestService = new AnagramClient();
            await UIClass.Create(requestService);
        }

       
    }
}
