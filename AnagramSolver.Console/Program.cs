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
        const string filePath = @"Console";
        static async Task Main(string[] args)
        {
            ClearFile();
            var program = new Program();            
            await program.Process();         
        }
        public async Task Process()
        {
            IAnagramSolver requestService = new AnagramClient();
            //IAnagramSolver requestService = new AnagramicaClient();
            //-----------------------delegate/action-------------
            //
            //var display = new Display(print=> WriteToConsole(print), requestService);


            var display = new DisplayWithEvents(requestService);
            display.Print += new DisplayWithEvents.PrintHandler(WriteToConsole);
            display.Print += new DisplayWithEvents.PrintHandler(WriteToFile);
            await display.ProcessAnagramManager();
        }

        private void WriteToConsole(string message)
        {
            System.Console.WriteLine(message);
        }

        private void WriteToDebug(string message)
        {
            Debug.WriteLine(message);
        }
        private void WriteToFile(string message)
        {
            using (StreamWriter file = File.AppendText(filePath))
            {
                file.WriteLine(message);              
            }           
        }
        private static void ClearFile()
        {
            File.WriteAllText(filePath, string.Empty);
        }
    }
}
