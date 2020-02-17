using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Entities
{
    public class Post : BaseEntity
    {
        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Content { get; set; }
    }
}
