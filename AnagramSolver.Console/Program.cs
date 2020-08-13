using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Console.UI;
using AnagramSolver.Contracts.Interfaces;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AnagramSolver.Console
{
    class Program
    {    
        static async Task Main(string[] args)
        {
            ////AdditionalWordsInsertClass.InsertWords();
            ////IAnagramSolver requestService = new BusinessLogic.AnagramSolver(new WordRepository());
            //IAnagramSolver requestService = new AnagramClient();
            //await UIClass.Create(requestService);


            var program = new Program();
            await program.action();         
        }
        public async Task action()
        {
            IAnagramSolver requestService = new AnagramClient();
            var display = new Display(print=> WriteToConsole(print), requestService);
            await display.ProcessAnagramManager();
        }

        public void WriteToConsole(string message)
        {
            System.Console.WriteLine(message);
        }

        public void WriteToDebug(string message)
        {
            Debug.WriteLine(message);
        }
        public void WriteToFile(string message)
        {
            using (StreamWriter file = new StreamWriter(@"Console"))
            {
                file.WriteLine(message);
                file.Close();
            }
           
        }

    }
}
