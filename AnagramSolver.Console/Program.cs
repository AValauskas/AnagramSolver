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

namespace AnagramSolver.Console
{
    class Program
    {
     
        static void Main(string[] args)
        {
            IRequestService requestService = new RequestService();
            UIClass ui = new UIClass(requestService);
        }

       
    }
}
