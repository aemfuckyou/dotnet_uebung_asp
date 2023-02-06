namespace Blog.Models;

public class CommentForCreationDto
{
    public string Content { get; set; } = string.Empty;
    public AuthorDto? Author { get; set; }
    public DateTime PublishingTime { get; set; } 
}