using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.BusinessLogic.Utils
{
    public class BusinessException : Exception
    {

        public BusinessException() { }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception inner) : base(message, inner) { }
    }
}
