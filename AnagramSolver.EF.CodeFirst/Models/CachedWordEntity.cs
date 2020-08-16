using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public partial class CachedWordEntity
    {
        public CachedWordEntity()
        {
            CachedWordWord = new HashSet<CachedWordWord>();
        }

        public string Word { get; set; }
        public int Id { get; set; }

        public virtual ICollection<CachedWordWord> CachedWordWord { get; set; }
    }
}
