using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Post> BlogPosts { get; set; }
    }
}
