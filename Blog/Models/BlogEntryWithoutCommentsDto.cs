
namespace Blog.Models;

public class BlogEntryWithoutCommentsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public AuthorDto? Author;
    public DateTime? PublishingTime { get; set; } 
}
