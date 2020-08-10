using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class UserLog
    {
        public string UserIp { get; set; }
        public string SearchedWord { get; set; }
        public DateTime Time { get; set; }
        public string Anagrams { get; set; }
        public int UserLogId { get; set; }
    }
}
