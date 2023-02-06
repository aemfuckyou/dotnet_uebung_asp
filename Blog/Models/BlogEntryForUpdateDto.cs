using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class BlogEntryForUpdateDto
{
    [Required(ErrorMessage = "Content can't be empty")]
    public string Content { get; set; }
    [Required(ErrorMessage = "Title can't be empty")]
    public string Title { get; set; }
    public AuthorDto? Author;
    public DateTime? PublishingTime { get; set; } 
}
