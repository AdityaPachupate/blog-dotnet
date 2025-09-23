using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetPostByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllPostAsync(int page, int pageSize);
        Task<IEnumerable<Post>> GetPostByAuthorAsync(string authorId);
        Task<IEnumerable<Post>> GetPublishedPostsAsync();
        Task<Post?> GetPostWithDetailsAsync(int id);
        Task<Post> AddPostAsync(Post post);
        Task<Post> UpdatePostAsync(Post post);
        Task DeletePostAsync(Guid id);
        Task IncrementPostViewsAsync(Guid id);
        Task IncrementPostLikesAsync(Guid id);

    }
}
