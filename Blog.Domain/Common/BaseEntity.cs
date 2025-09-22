// Blog.Domain/Common/BaseEntity.cs
namespace Blog.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // This helps with tracking when entities are modified
        public void MarkAsModified()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}