﻿// Blog.Application/DTOs/CreatePostCommandValidator.cs
using FluentValidation;

namespace Blog.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator() {
            RuleFor(x => x.Title)
               .NotEmpty()
               .WithMessage("Title is required")
               .MaximumLength(200)
               .WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required")
                .MinimumLength(10)
                .WithMessage("Content must be at least 10 characters");

            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .WithMessage("Author ID is required");

        }
    }
}
