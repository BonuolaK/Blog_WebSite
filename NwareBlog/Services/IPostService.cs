using NwareBlog.Entities;
using NwareBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Services
{
    public interface IPostService
    {
        public ResultModel<int> Create(Post post);

        public ResultModel<int> Update(PostDto post);

        public ResultModel<int> Delete(int Id);

        public PostDto Get(int Id);

        public IEnumerable<PostDto> GetAllPublished(int? categoryId = default);
    }
}
