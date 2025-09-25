using Blog.Domain.Common;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence;

public class BlogDbContext : IdentityDbContext<User>
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        #region Configuaration for Post 
        builder.Entity<Post>(entity =>
        {

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Content).IsRequired();
            entity.Property(p => p.Summary).HasMaxLength(500);

            // Config relationships with user
            entity.HasOne(p => p.Author)
                  .WithMany(u => u.Posts)
                  .HasForeignKey(p => p.AuthorId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(p => p.AuthorId);
            entity.HasIndex(p => p.CreatedAt);
            entity.HasIndex(p => new { p.AuthorId, p.CreatedAt });
        });
        #endregion

        #region Configuaration for Comment 
        builder.Entity<Comment>(entity =>
        {

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Content).IsRequired().HasMaxLength(1000);

            entity.HasOne(c => c.Post)
                  .WithMany(p => p.Comments)
                  .HasForeignKey(c => c.PostId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            entity.HasIndex(c => c.PostId);


        });
        #endregion

        #region Configuaration for PostLike 
        builder.Entity<PostLike>(entity =>
        {

            entity.HasKey(pl => pl.Id);

            entity.HasOne(pl=>pl.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(pl => pl.PostId)
                    .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(pl => pl.User)
                    .WithMany(u => u.PostLikes)
                    .HasForeignKey(pl => pl.UserId)
                    .OnDelete(DeleteBehavior.Restrict);


            entity.HasIndex(pl => new { pl.PostId, pl.UserId }).IsUnique();

        });
        #endregion

        #region Configuaration for User 
        builder.Entity<User>(entity =>
        {

            builder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Bio).HasMaxLength(500);
            });

        });
        #endregion
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entities = ChangeTracker
            .Entries()
            .Where(x => x.Entity is BaseEntity &&
                       (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var baseEntity = (BaseEntity)entity.Entity;

            if (entity.State == EntityState.Added)
            {
                baseEntity.CreatedAt = DateTime.UtcNow;
            }

            baseEntity.UpdatedAt = DateTime.UtcNow;
        }
    }


}
