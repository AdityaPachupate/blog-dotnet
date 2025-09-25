using AutoMapper;
using Blog.Application.DTOs;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler(
            ILogger<CreatePostCommandHandler> logger,
            IMapper mapper,
            IPostRepository postRepository
        ) : IRequestHandler<CreatePostCommand, PostDto>
    {
        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling CreatePostCommand for Title: {Title}", request.Title);

            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                Summary = request.Summary,
                CoverImageUrl = request.CoverImageUrl,
                IsPublished = request.IsPublished,
                AuthorId = request.AuthorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdPost = await postRepository.AddPostAsync(post);

            if (createdPost == null)
            {
                throw new NotFoundException(nameof(request.Title), request.Title.ToString());
            }

            var postWithDetails = await postRepository.GetPostWithDetailsAsync(createdPost.Id);

            return mapper.Map<PostDto>(postWithDetails);
        }
    }
}
