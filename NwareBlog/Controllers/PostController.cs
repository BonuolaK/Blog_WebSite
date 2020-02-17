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
    [Route("posts")]
    public class PostController : BaseController
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;
        public PostController(ILogger<PostController> logger,
            IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostDto>), 200)]
        public IActionResult GetPosts()
        {
            var result = _postService.GetAllPublished();
           
            if (result.Count() == 0)
                return NoContent();

            return Ok(result);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PostDto), 200)]
        public IActionResult GetPost([FromRoute] int id)
        {
            var result = _postService.Get(id);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }


        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(200)]
        public IActionResult DeletePost([FromRoute] int id)
        {
            var result = _postService.Delete(id);

            if (result.NotFound)
                return NotFound();

            if (result.HasError)
                return HandleBadRequest(result.ErrorMessages);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create([FromBody] PostCreateVm model)
        {
            if (!ModelState.IsValid)
                return HandleBadRequest(ListModelErrors);

            var post = new Post
            {
                DateCreated = DateTime.Now,
                CategoryId = model.CategoryId,
                Content = model.Content,
                PublicationDate = model.PublicationDateTime,
                Title = model.Title
            };

            var result = _postService.Create(post);

            if (result.HasError)
                return HandleBadRequest(result.ErrorMessages);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(200)]
        public IActionResult Update([FromBody] PostCreateVm model)
        {
            if (!ModelState.IsValid)
                return HandleBadRequest(ListModelErrors);

            var post = new PostDto
            {
                Id = model.Id,
                Title = model.Title,
                PublicationDate = model.PublicationDateTime,
                Content = model.Content,
                CategoryId = model.CategoryId
            };

            var result = _postService.Update(post);

            if (result.NotFound)
                return NotFound();

            if (result.HasError)
                return HandleBadRequest(result.ErrorMessages);

            return Ok();
        }
    }
}
