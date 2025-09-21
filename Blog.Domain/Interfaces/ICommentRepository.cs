using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsForPostAsync(Guid postId);
        Task DeleteCommentAsync(Guid commentId, string userId);
        Task UpdateCommentAsync(Comment comment, string userId);
    }
}
