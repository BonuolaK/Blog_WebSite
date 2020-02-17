using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Model
{
    public class PaginationResponseModel<T> where T : class
    {
        public PaginationResponseModel(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;

        }

        public int TotalRecords { get; }

        public IEnumerable<T> Items { get; }

        public int PageNumber { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public int PageCount => TotalRecords > 0
                            ? (int)Math.Ceiling(TotalRecords / (double)PageSize)
                            : 0;

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < PageCount;
    }
}
