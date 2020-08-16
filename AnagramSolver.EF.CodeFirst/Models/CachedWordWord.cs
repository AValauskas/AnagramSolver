using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Models
{   
    public partial class CachedWordWord
    {
        public int WordId { get; set; }
        public int CachedWordId { get; set; }

        public virtual CachedWordEntity CachedWord { get; set; }
        public virtual WordEntity Word { get; set; }
    }
}
