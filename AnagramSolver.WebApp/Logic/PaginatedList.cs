using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.WebApp.Logic
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage { get => PageIndex > 1; }
        public bool HasNextPage { get => PageIndex < TotalPages; }

        private PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public static PaginatedList<T> Create(List<T> items,int totalWordsCount, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(items, totalWordsCount, pageIndex, pageSize);
        }
    }
}
