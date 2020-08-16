using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public partial class WordEntity
    {
        public WordEntity()
        {
            CachedWordWord = new HashSet<CachedWordWord>();
        }

        public int Id { get; set; }
        public string Word { get; set; }
        public string Category { get; set; }
        public string SortedWord { get; set; }

        public virtual ICollection<CachedWordWord> CachedWordWord { get; set; }
    }
}
