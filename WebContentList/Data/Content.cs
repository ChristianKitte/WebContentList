using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebContentList.Data;

public class Content
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int ContentId { get; set; }

    [Required] public int SubjectId { get; set; } = 0;

    [ForeignKey("SubjectId")] public Subject Subject { get; set; }

    [Required] public string Url { get; set; } = string.Empty;

    [Required] public bool ShowPreview { get; set; } = true;

    [Required] public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}