namespace Blog.Models;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public AuthorDto? Author { get; set; }
    public DateTime PublishingTime { get; set; } 
}