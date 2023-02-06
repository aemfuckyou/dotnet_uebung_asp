namespace Blog.Models;

public class BlogEntryDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public AuthorDto? Author;
    public int NumberOfComments => Comments.Count;
    public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
    public DateTime? PublishingTime { get; set; } 
}
