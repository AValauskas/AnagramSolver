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
            var wordRepository = new WordRepository();
            IAnagramSolver anagramSolver = new BusinessLogic.AnagramSolver(wordRepository);

            UIClass ui = new UIClass(anagramSolver);
        }

       
    }
}
