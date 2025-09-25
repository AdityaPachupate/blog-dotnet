using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    internal class PostRepository(BlogDbContext context) : IPostRepository
    {
        public async Task<Post> AddPostAsync(Post post)
        {
            context.Posts.Add(post);
            await context.SaveChangesAsync();
            return post;
        }

        public async Task DeletePostAsync(int id)
        {
            var entity = context.Posts.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                context.Posts.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
           var posts = await context.Posts.ToListAsync();
           return posts;
        }

        public async Task<IEnumerable<Post>> GetPostByAuthorAsync(string authorId)
        {
            var post = await context.Posts.Where(p => p.AuthorId == authorId).ToListAsync();
            return post;
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await context.Posts.FindAsync(id);
        }

        public Task<Post?> GetPostWithDetailsAsync(int id)
        {
            
        }

        public Task<IEnumerable<Post>> GetPublishedPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task IncrementPostLikesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task IncrementPostViewsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async  Task<Post> UpdatePostAsync(Post post)
        {
            var existingPost = await context.Posts.FindAsync(post.Id);
            if (existingPost == null)
            {
                throw new InvalidOperationException($"Post with ID {post.Id} not found.");
            }

            // Update properties
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.Summary = post.Summary;
            existingPost.CoverImageUrl = post.CoverImageUrl;
            existingPost.IsPublished = post.IsPublished;
            existingPost.AuthorId = post.AuthorId;
            existingPost.Views = post.Views;
            existingPost.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return existingPost;
        }
    }
}
