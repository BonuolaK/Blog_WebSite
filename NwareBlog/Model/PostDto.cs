using NwareBlog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Model
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Category { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public string PublicationDateFormat => PublicationDate.ToString(AppConsts.DateConsts.SystemDateTimeFormat);

        public string Content { get; set; }
    }
}
