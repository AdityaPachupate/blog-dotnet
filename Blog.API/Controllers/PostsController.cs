using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Security.Claims;
using Blog.Application.Features.Posts.Commands.CreatePost;
using Blog.Application.Features.Posts.Queries.GetAllPosts;
using Blog.Application.Features.Posts.Queries.GetPostById;
using Blog.Application.DTOs;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IMediator mediator, ILogger<PostsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPosts([FromQuery] bool publishedOnly = true)
        {
            _logger.LogInformation("Fetching all posts. Published only: {PublishedOnly}", publishedOnly);
            
            var query = new GetAllPostsQuery { PublishedOnly = publishedOnly };
            var posts = await _mediator.Send(query);
            
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id, [FromQuery] bool incrementViews = true)
        {
            _logger.LogInformation("Fetching post with ID: {PostId}", id);
            
            var query = new GetPostByIdQuery(id, incrementViews);
            var post = await _mediator.Send(query);
            
            if (post == null)
            {
                _logger.LogWarning("Post not found with ID: {PostId}", id);
                return NotFound($"Post with ID {id} not found");
            }
            
            return Ok(post);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token");
            }

            _logger.LogInformation("Creating new post for user: {UserId}", userId);
            
            var command = new CreatePostCommand
            {
                Title = createPostDto.Title,
                Content = createPostDto.Content,
                Summary = createPostDto.Summary,
                CoverImageUrl = createPostDto.CoverImageUrl,
                IsPublished = createPostDto.IsPublished,
                AuthorId = userId
            };

            var createdPost = await _mediator.Send(command);
            
            _logger.LogInformation("Post created successfully with ID: {PostId}", createdPost.Id);
            
            return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
        }
    }
} 