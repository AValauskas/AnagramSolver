using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts.Models
{
    public partial class CachedWord
    {
        public CachedWord()
        {
            CachedWordWord = new HashSet<CachedWordWord>();
        }

        public string Word { get; set; }
        public int Id { get; set; }

        public virtual ICollection<CachedWordWord> CachedWordWord { get; set; }
    }
}
