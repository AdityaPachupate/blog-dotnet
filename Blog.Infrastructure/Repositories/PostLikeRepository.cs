using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    internal class PostLikeRepository(BlogDbContext context) : IPostLikeRepository
    {
        public async Task<PostLike> AddLikeAsync(PostLike like)
        {
            context.PostLikes.Add(like);
            await context.SaveChangesAsync();
            return like;
        }

        public async Task<PostLike?> GetLikeAsync(int postId, string userId)
        {
            return await context.PostLikes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
        }

        public async Task<int> GetLikeCountAsync(int postId)
        {
            return await context.PostLikes
               .CountAsync(pl => pl.PostId == postId);
        }

        public async Task RemoveLikeAsync(PostLike like)
        {
           context.PostLikes.Remove(like);
           await context.SaveChangesAsync();
        }
    }
}
