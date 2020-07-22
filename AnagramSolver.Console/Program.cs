using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Console.UI;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.Console
{
    class Program
    {
     
        static void Main(string[] args)
        {
            IAnagramSolver anagramSolver = new BusinessLogic.AnagramSolver()
            {
                WordRepository = new WordRepository()
            };

            UIClass ui = new UIClass(anagramSolver);
        }

       
    }
}
