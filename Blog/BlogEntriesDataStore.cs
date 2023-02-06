using Blog.Models;

namespace Blog;

public class BlogEntriesDataStore
{
    public List<BlogEntryDto> Entries { get; set; } = new();
    public BlogEntriesDataStore()
    {
        var Authors = new List<AuthorDto>()
            {
                new AuthorDto()
                {
                    Id = 1,
                    FirstName = "Marion",
                    LastName = "Neuhauser"
                },
                new AuthorDto{
                    Id = 2,
                    FirstName = "Hugo",
                    LastName = "Sonderegger"
                }
            };
        Entries = new List<BlogEntryDto>()
            {
                new BlogEntryDto()
                {
                    Id = 1,
                    Title = "Dotnet on Linux",
                    Content = "dotnet...blabla..... linux... blabla",
                    Author = Authors[0],
                    PublishingTime = new DateTime(2022, 1, 1, 12, 0, 34),
                    Comments = new List<CommentDto>(){
                        new CommentDto()
                        {
                            Id = 1,
                            Content = "good Blog... well written...blabla",
                            Author = Authors[1],
                            PublishingTime = new DateTime(2022, 1, 2, 13, 5, 25)
                        }                        
                    }
                },
                new BlogEntryDto()
                {
                    Id = 2,
                    Title = "Dotnet on Windows",
                    Content = "dotnet...blabla..... windows... blabla",
                    Author = Authors[1],
                    PublishingTime = new DateTime(2022, 1, 3, 16, 7, 21),
                    Comments = new List<CommentDto>(){
                        new CommentDto()
                        {
                            Id = 2,
                            Content = "good Blog... well written...f windows",
                            Author = Authors[0],
                            PublishingTime = new DateTime(2022, 1, 4, 3, 35, 2)
                        }                        
                    }
                }
            };
    }
}
