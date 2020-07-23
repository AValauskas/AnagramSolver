using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.BusinessLogic.Utils
{
    public class BussinessException : Exception
    {
        public BussinessException(string message)
       : base(message)
        {

        }
    }
}
