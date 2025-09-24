using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetPostByIdAsync(int id);
        Task<IEnumerable<Post>> GetAllPostAsync();
        Task<IEnumerable<Post>> GetPostByAuthorAsync(string authorId);
        Task<IEnumerable<Post>> GetPublishedPostsAsync();
        Task<Post?> GetPostWithDetailsAsync(int id);
        Task<Post> AddPostAsync(Post post);
        Task<Post> UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
        Task IncrementPostViewsAsync(int id);
        Task IncrementPostLikesAsync(int id);

    }
}
