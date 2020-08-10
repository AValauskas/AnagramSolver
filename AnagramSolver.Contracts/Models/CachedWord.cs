using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts.Models
{
    public class CachedWord
    {
        public string Word { get; set; }
        public int CachedWordId { get; set; }
    }
}
