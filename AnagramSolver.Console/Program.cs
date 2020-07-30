using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Console.UI;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.Console
{
    class Program
    {
     
        static async Task Main(string[] args)
        {
            IAnagramClient requestService = new AnagramClient();
            await UIClass.Create(requestService);
        }

       
    }
}
