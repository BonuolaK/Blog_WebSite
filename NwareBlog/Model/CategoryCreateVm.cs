using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Model
{
    public class CategoryCreateVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Name { get; set; }
    }
}
