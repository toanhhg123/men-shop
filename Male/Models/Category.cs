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
    public string? metaTiltle { get; set; } = default!;
    public string? metaKeyword { get; set; } = default!;
    public DateTime? createdAt { get; set; } = DateTime.Now;
    public DateTime? updateAt { get; set; } = DateTime.Now;
    public string? updateBy { get; set; }
    public string? createdBy { get; set; }

    public string? Desc { get; set; } = default!;
}
