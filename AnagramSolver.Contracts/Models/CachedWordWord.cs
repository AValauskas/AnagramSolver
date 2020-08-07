using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts.Models
{
    public partial class CachedWordWord
    {
        public int WordId { get; set; }
        public int CachedWordId { get; set; }

        public virtual CachedWord CachedWord { get; set; }
        public virtual WordModel Word { get; set; }
    }
}
