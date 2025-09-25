// Blog.Domain/Interfaces/IPostRepository.cs
using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByAuthorAsync(string authorId);
        Task<IEnumerable<Post>> GetPublishedPostsAsync();
        Task<Post?> GetPostWithDetailsAsync(int id);
        Task IncrementViewCountAsync(int postId);
    }
}