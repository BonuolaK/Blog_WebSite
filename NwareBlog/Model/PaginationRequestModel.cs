using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NwareBlog.Utils.AppConsts;

namespace NwareBlog.Model
{
    public class PaginationRequestModel
    {
        public string Keyword { get; set; }
        public string Filter { get; set; }
        public int PageIndex { get; set; } = PaginationConsts.PageIndex;
        public int? PageTotal { get; set; }
        public int PageSize { get; set; } = PaginationConsts.PageSize;
        public int PageSkip => (PageIndex - 1) * PageSize;
    }

    
}
