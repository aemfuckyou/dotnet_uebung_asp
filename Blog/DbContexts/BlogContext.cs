using Blog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DbContexts;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) 
    {
        Database.EnsureCreated();
    }

    public DbSet<BlogEntry> Entries { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;

    public DbSet<Author> Authors { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
                .HasData(
                new Author()
                {
                    Id = 1,
                    FirstName = "Marion",
                    LastName = "Neuhauser"
                },
                new Author{
                    Id = 2,
                    FirstName = "Hugo",
                    LastName = "Sonderegger"
                });
                
        modelBuilder.Entity<BlogEntry>()
                .HasData(
                new BlogEntry()
                {
                    Id = 1,
                    Title = "Dotnet on Linux",
                    Content = "dotnet...blabla..... linux... blabla",
                    AuthorId = 1,
                    PublishingTime = new DateTime(2022, 1, 1, 12, 0, 34),
                },
                new BlogEntry()
                {
                    Id = 2,
                    Title = "Dotnet on Windows",
                    Content = "dotnet...blabla..... windows... blabla",
                    AuthorId = 2,
                    PublishingTime = new DateTime(2022, 1, 3, 16, 7, 21),
                }
                );

        modelBuilder.Entity<Comment>()
                .HasData(
                new Comment()
                {
                    Id = 1,
                    Content = "good Blog... well written...blabla",
                    AuthorId = 2,
                    BlogEntryId = 1,
                    PublishingTime = new DateTime(2022, 1, 2, 13, 5, 25)
                },
                new Comment()
                {
                    Id = 2,
                    Content = "good Blog... well written...f windows",
                    AuthorId = 1,
                    BlogEntryId = 2,
                    PublishingTime = new DateTime(2022, 1, 4, 3, 35, 2)
                });  
                
    }
}