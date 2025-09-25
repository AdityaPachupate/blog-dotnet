// Blog.Domain/Interfaces/ICommentRepository.cs
using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByPostAsync(int postId);
    }
}