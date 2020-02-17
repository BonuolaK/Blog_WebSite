using NwareBlog.DataAccess.Repository;
using NwareBlog.Entities;
using NwareBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Post> _postRepo;
        public CategoryService(IRepository<Category> categoryRepo,
            IRepository<Post> postRepo)
        {
            _categoryRepo = categoryRepo;
            _postRepo = postRepo;
        }


        private bool VerifyCategoryNameExists(string name, int Id = 0)
        {
            var categoryNameCount = _categoryRepo
                 .GetAllIncluding()
                 .Where(x => x.Id != Id && x.Name.Equals(name))
                 .Count();

            return categoryNameCount > 0;
        }

        private bool VerifyCategoryHasPost(int categoryId)
        {
            var categoryPostsCount = _postRepo
                .GetAllIncluding()
                .Where(x => x.CategoryId == categoryId)
                .Count();

            return categoryPostsCount > 0;
        }

        public ResultModel<int> Create(Category category)
        {
            var result = new ResultModel<int>();

            if (VerifyCategoryNameExists(category.Name))
            {
                result.AddError("This name already exists. Please try another");
                return result;
            }

            _categoryRepo.Insert(category);

            result.Data = category.Id;

            return result;
        }

        public ResultModel<int> Delete(int Id)
        {
            var result = new ResultModel<int>();

            if (VerifyCategoryHasPost(Id))
            {
                result.AddError("Some posts belong to this category. Please delete all related posts and try again");
                return result;
            }

            var category = _categoryRepo.Get(Id);

            if (category == null)
            {
                result.AddError("Category not found");
                result.NotFound = true;
                return result;
            }

            _categoryRepo.Delete(category);

            return result;
        }

        public CategoryDto Get(int Id)
        {
            var category = _categoryRepo
                .GetAllIncluding()
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PostCount = x.BlogPosts.Count()
                })
                .FirstOrDefault(x => x.Id == Id);

            return category;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var categories = _categoryRepo
                .GetAllIncluding()
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PostCount = x.BlogPosts.Count()
                }).ToList();

            return categories;
        }

        public ResultModel<int> Update(CategoryDto category)
        {

            var result = new ResultModel<int>();
            var categoryEntity = _categoryRepo.Get(category.Id);

            if (categoryEntity == null)
            {
                result.AddError("Category not found");
                result.NotFound = true;
                return result;
            }

            if (VerifyCategoryNameExists(category.Name, category.Id))
            {
                result.AddError("This name already exists. Please try another");
                return result;
            }


            categoryEntity.Name = category.Name;
            categoryEntity.DateModified = DateTime.Now;

            _categoryRepo.Update(categoryEntity);

            return result;
        }


    }
}
