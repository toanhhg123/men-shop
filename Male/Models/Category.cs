using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Male.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string id { set; get; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "category name is require")]
    public string Name { get; set; } = default!;
    public string? Desc { get; set; } = default!;
}
