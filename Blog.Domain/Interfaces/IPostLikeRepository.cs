// Blog.Domain/Interfaces/IPostLikeRepository.cs  
using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces
{
    public interface IPostLikeRepository 
    {
        Task<PostLike?> GetLikeAsync(int postId, string userId);
        Task<PostLike> AddLikeAsync(PostLike like);
        Task RemoveLikeAsync(PostLike like);
        Task<int> GetLikeCountAsync(int postId);
    }
}