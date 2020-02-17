using NwareBlog.DataAccess.Repository;
using NwareBlog.Entities;
using NwareBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepo;
        private readonly ICategoryService _categoryService;
        public PostService(IRepository<Post> postRepo, ICategoryService categoryService)
        {
            _postRepo = postRepo;
            _categoryService = categoryService;
        }
        public ResultModel<int> Create(Post post)
        {
            var result = new ResultModel<int>();

            if (VerifyPostTitleExists(post.Title))
            {
                result.AddError("This title already exists. Please try another");
                return result;
            }

            var category = _categoryService.Get(post.CategoryId);

            if (category == null)
            {
                result.AddError("Category not found");
                return result;
            }
               

            _postRepo.Insert(post);

            result.Data = post.Id;

            return result;
        }

        public ResultModel<int> Delete(int Id)
        {
            var result = new ResultModel<int>();
            var post = _postRepo.Get(Id);

            if (post == null)
            {
                result.AddError("Post not found");
                result.NotFound = true;
                return result;
            }
               
            _postRepo.Delete(post);

            return result;
        }

        public PostDto Get(int Id)
        {
            return _postRepo.GetAllIncluding()
                .Select(x => new PostDto
                {
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Content = x.Content,
                    Id = x.Id,
                    PublicationDate = x.PublicationDate,
                    Title = x.Title
                })
                .FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<PostDto> GetAllPublished(int? categoryId = default)
        {
            var query = _postRepo
                .GetAllIncluding(x => x.Category)
                .Where(x => x.PublicationDate <= DateTime.Now.Date);

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            var posts = query
                 .Select(x => new PostDto
                 {
                     Category = x.Category.Name,
                     CategoryId = x.CategoryId,
                     Content = x.Content,
                     Id = x.Id,
                     PublicationDate = x.PublicationDate,
                     Title = x.Title
                 })
                .OrderByDescending(x => x.PublicationDate)
                .ToList();

            return posts;
        }

        public ResultModel<int> Update(PostDto post)
        {

            var result = new ResultModel<int>();

            if (VerifyPostTitleExists(post.Title, post.Id))
            {
                result.AddError("This title already exists. Please try another");
                return result;
            }

            var category = _categoryService.Get(post.CategoryId);

            if (category == null)
            {
                result.AddError("Category not found");
                return result;
            }

            var postEntity = _postRepo.Get(post.Id);

            if (postEntity == null)
            {
                result.NotFound = true;
                result.AddError("Post not found");
                return result;
            }
                

            postEntity.CategoryId = post.CategoryId;
            postEntity.PublicationDate = post.PublicationDate;
            postEntity.DateModified = DateTime.Now;
            postEntity.Content = post.Content;
            postEntity.Title = post.Title;

            _postRepo.Update(postEntity);

            return result;
        }

        private bool VerifyPostTitleExists(string name, int Id = 0)
        {
            var postNameCount = _postRepo
                 .GetAllIncluding()
                 .Where(x => x.Id != Id && x.Title.Equals(name))
                 .Count();

            return postNameCount > 0;
        }
    }
}
