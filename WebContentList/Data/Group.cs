using Microsoft.AspNetCore.Authorization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebContentList.Data;

[Authorize]
public class Group
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int SubjectId { get; set; }

    [Required] public bool isDefault { get; set; } = false;
    [Required] public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}