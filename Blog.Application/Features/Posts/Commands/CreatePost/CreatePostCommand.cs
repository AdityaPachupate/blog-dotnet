// Blog.Application/DTOs/CreatePostCommand.cs
using Blog.Application.DTOs;
using MediatR;

namespace Blog.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<PostDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsPublished { get; set; } = true;
        public string AuthorId { get; set; } = string.Empty;
    }
}
