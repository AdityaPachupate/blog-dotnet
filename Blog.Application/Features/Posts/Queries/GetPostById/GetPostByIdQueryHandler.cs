using AutoMapper;
using Blog.Application.DTOs;
using Blog.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.Posts.Queries.GetPostById
{
    public class GetPostByIdQueryHandler(
            ILogger<GetPostByIdQueryHandler> logger,
            IPostRepository postRepository,
            IMapper mapper
        )
        : IRequestHandler<GetPostByIdQuery, PostDto?>
    {
        public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting post by Id: {Id}", request.Id);

            var post = await postRepository.GetPostWithDetailsAsync(request.Id);

            if (post == null) return null;

            if (request.IncrementViews)
            {
                await postRepository.IncrementViewCountAsync(request.Id);
                post.Views += 1; // Increment the local count to reflect the change
            }

            return mapper.Map<PostDto>(post);


        }
    }
}
