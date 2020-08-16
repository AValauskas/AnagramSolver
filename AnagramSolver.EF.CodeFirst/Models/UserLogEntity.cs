using AnagramSolver.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public partial class UserLogEntity
    {
        public string UserIp { get; set; }
        public string SearchedWord { get; set; }
        public DateTime Time { get; set; }
        public string Anagrams { get; set; }
        public int Id { get; set; }
        public TaskType Type { get; set; }
    }
}
