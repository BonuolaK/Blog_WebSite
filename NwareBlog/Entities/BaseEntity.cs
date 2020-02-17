using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
