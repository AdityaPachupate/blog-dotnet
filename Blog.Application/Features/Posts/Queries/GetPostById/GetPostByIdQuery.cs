using Blog.Application.DTOs;
using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetPostById
{
    public class GetPostByIdQuery(int id, bool incrementViews) : IRequest<PostDto?>
    {
        public int Id { get; set; } = id;
        public bool IncrementViews { get; set; } = incrementViews;
    }
}
