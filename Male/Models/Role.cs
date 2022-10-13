using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Male.Models;

public class Role
{
    [Key]
    public string id { set; get; } = default!;

    [Required(ErrorMessage = "")]
    public string RoleName { get; set; } = default!;
    public string? Desc { get; set; } = default!;
}
