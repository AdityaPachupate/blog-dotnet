// Blog.Domain/Entities/Post.cs
using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        //public string? Summary { get; set; }
        //public string? CoverImageUrl { get; set; }
        public int Views { get; set; } = 0;
        public bool IsPublished { get; set; } = true;

        // Foreign Key to User
        public string AuthorId { get; set; } = string.Empty;

        // Navigation properties
        public virtual User Author { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<PostLike> Likes { get; set; } = new List<PostLike>();

        // Computed property for like count
        public int LikeCount => Likes?.Count ?? 0;
    }
}