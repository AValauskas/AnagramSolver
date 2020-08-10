using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts.Models
{
    public class CachedWord_WordEntity
    {
        public WordModel Word { get; set; }
        public CachedWord CachedWord { get; set; }
        public int Id { get; set; }
    }
}
