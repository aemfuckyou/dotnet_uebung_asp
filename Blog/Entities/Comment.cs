using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Entities;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    [ForeignKey("AuthorId")]
    public Author? Author { get; set; }

    public int AuthorId { get; set; }

    [ForeignKey("BlogEntryId")]
    public BlogEntry? BlogEntry { get; set; }

    public int BlogEntryId { get; set; }

    [Required]
    public DateTime PublishingTime { get; set; } 
}