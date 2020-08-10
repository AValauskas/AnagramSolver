using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts.Models
{
    public class UserLog
    {
        public string UserIp { get; set; }
        public string Word { get; set; }
        public DateTime Time { get; set; }
        public string Anagrams { get; set; }
    }
}
