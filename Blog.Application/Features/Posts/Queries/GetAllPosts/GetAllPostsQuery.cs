using Blog.Application.DTOs;
using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetAllPosts
{
    public class GetAllPostsQuery : IRequest<IEnumerable<PostDto>>
    {
        public bool PublishedOnly { get; set; } = true;
    }
}
