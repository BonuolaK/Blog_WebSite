using NwareBlog.Entities;
using NwareBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Services
{
    public interface ICategoryService
    {
        public ResultModel<int> Create(Category post);

        public ResultModel<int> Update(CategoryDto post);

        public ResultModel<int> Delete(int Id);

        public CategoryDto Get(int Id);

        public IEnumerable<CategoryDto> GetAll();
    }
}
