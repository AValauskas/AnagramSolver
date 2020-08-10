using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{   
    public partial class CachedWordWord
    {
        public int WordId { get; set; }
        public int CachedWordId { get; set; }
        public int Id { get; set; }

        public virtual CachedWord CachedWord { get; set; }
        public virtual Word Word { get; set; }
        public void HasNoKey()
        { }
    }
}
