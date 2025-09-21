using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetPostByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllPostAsync(int page, int pageSize);
        Task<IEnumerable<Post>> GetPostByAuthorAsync(string authorId);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(Guid id);
        Task IncrementPostViewsAsync(Guid id);
        Task IncrementPostLikesAsync(Guid id);

    }
}
