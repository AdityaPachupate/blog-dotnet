using System.Xml.Linq;

namespace Blog.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string AuthorId { get; set; } = null!; // Identity user id
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public string? CoverImageUrl { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
