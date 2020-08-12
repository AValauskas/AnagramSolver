using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Console.UI
{
    public delegate void Print(string message);
    public class Display : IDisplay
    {
        public Display(Print printDelegate)
        {
            Print del = printDelegate;
        }
    }
}
