using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class Word
    {
        public Word()
        {
            CachedWordWord = new HashSet<CachedWordWord>();
        }

        public int Id { get; set; }
        public string Word1 { get; set; }
        public string Category { get; set; }
        public string SortedWord { get; set; }

        public virtual ICollection<CachedWordWord> CachedWordWord { get; set; }
    }
}
