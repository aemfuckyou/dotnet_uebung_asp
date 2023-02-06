using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Entities;

public class BlogEntry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = string.Empty;
    [ForeignKey("AuthorId")]
    public Author? Author { get; set; }
    public int AuthorId { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    [Required]
    public DateTime PublishingTime { get; set; } 


}