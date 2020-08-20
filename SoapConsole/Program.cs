using ServiceReference1;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SoapConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {           
            var conf = new CalculatorSoapClient.EndpointConfiguration();
            var calculator = new CalculatorSoapClient(conf);

            var anagramService = new AnagramServiceReference.AnagramServiceClient();

           // await SoapLogic(calculator);
            await CalculateAnagram(anagramService);
        }

        public static async Task SoapLogic(CalculatorSoapClient calculator)
        {
            while (true)
            {              
                Console.WriteLine("Choose action (+ - / *) x - to exit");
                var act = Console.ReadLine();

                if (act == "x")
                    break;
                int num1, num2;                
                Console.WriteLine("Write first number");
                if (!int.TryParse(Console.ReadLine(), out num1))
                {
                    Console.WriteLine("You must write number in numbers part\n");
                    continue;
                }
               
                Console.WriteLine("Write Second number");
                if (!int.TryParse(Console.ReadLine(), out num2))
                {
                    Console.WriteLine("You must write number in numbers part");
                    continue;
                }

                switch (act)
                {
                    case "+":
                        Console.WriteLine($"{num1} + {num2} = {await calculator.AddAsync(num1, num2)}");
                        break;
                    case "-":
                        Console.WriteLine($"{num1} - {num2} = {await calculator.SubtractAsync(num1, num2)}");
                        break;
                    case "*":
                        Console.WriteLine($"{num1} * {num2} = {await calculator.MultiplyAsync(num1, num2)}");
                        break;
                    case "/":
                        Console.WriteLine($"{num1} / {num2} = {await calculator.DivideAsync(num1, num2)}");
                        break;                
                      
                    default:
                        Console.WriteLine("wrong action was written ");
                        break;
                }
                Console.WriteLine("Press any to continue");
                Console.ReadLine();
            }
        }

        public static async Task CalculateAnagram(AnagramServiceReference.AnagramServiceClient anagramService)
        {
            while (true)
            {

                Console.WriteLine("Write Word or write <x> to exit");
                var word = Console.ReadLine();

                if (word == "x")
                    break;

                var anagrams = await anagramService.GetAnagramsAsync(word);

                Console.WriteLine("\n Anagrams:");
                foreach (var item in anagrams)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("\nPress any to continue");
                Console.ReadLine();
            }
        }
    }
}
