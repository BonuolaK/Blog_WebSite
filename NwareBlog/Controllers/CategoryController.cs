using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NwareBlog.Entities;
using NwareBlog.Model;
using NwareBlog.Services;
using NwareBlog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : BaseController
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        public CategoryController(ILogger<CategoryController> logger,
            ICategoryService categoryService, IPostService postService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _postService = postService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        public IActionResult GetCategories()
        {
            var result = _categoryService.GetAll();
            if (result.Count() == 0)
                return NoContent();

            return Ok(result);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        public IActionResult GetCategory([FromRoute] int id)
        {
            var result = _categoryService.Get(id);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [Route("{id}/posts")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostDto>), 200)]
        public IActionResult GetCategoryPosts([FromRoute] int id)
        {
            var result = _postService.GetAllPublished(id);
            if (result.Count() == 0)
                return NoContent();
            else
                return Ok(result);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(200)]
        public IActionResult DeleteCategory([FromRoute] int id)
        {
            var result = _categoryService.Delete(id);

            if (result.NotFound)
                return NotFound();

            if (result.HasError)
                return HandleBadRequest(result.ErrorMessages);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create([FromBody] CategoryCreateVm model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ListModelErrors.ToArray());

            var category = new Category
            {
                DateCreated = DateTime.Now,
                Name = model.Name
            };

            var result = _categoryService.Create(category);
            if (result.HasError)
                return HandleBadRequest(result.ErrorMessages);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(200)]
        public IActionResult Update([FromBody] CategoryCreateVm model)
        {
            if (!ModelState.IsValid)
                return HandleBadRequest(ListModelErrors);

            var category = new CategoryDto
            {
                Name = model.Name,
                Id = model.Id
            };

            var result = _categoryService.Update(category);

            if (result.NotFound)
                return NotFound();

            if (result.HasError)
                return HandleBadRequest(result.ErrorMessages);

            return Ok();
        }
    }
}
