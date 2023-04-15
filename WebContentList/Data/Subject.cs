using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;

namespace WebContentList.Data;

[Authorize]
public class Subject
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int SubjectId { get; set; }

    [Required] public bool isDefault { get; set; } = false;
    [Required] public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}