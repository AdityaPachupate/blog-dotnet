using AutoMapper;
using Blog.Application.DTOs;
using Blog.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.Posts.Queries.GetAllPosts
{
    public class GetAllPostsQueryHandler(
            ILogger<GetAllPostsQueryHandler> logger,
            IMapper mapper,
            IPostRepository postRepository
        ) : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
    {
        public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all posts");

            var posts = request.PublishedOnly
                ? await postRepository.GetPublishedPostsAsync()
                : await postRepository.GetAllAsync();

            return mapper.Map<IEnumerable<PostDto>>(posts);
        }
    }
}
