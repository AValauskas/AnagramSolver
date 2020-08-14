using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class CachedWord
    {
        public CachedWord()
        {
            CachedWordWord = new HashSet<CachedWordWord>();
        }

        public string Word { get; set; }
        public int CachedWordId { get; set; }

        public virtual ICollection<CachedWordWord> CachedWordWord { get; set; }
    }
}
