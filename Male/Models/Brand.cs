using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;                                             

namespace Male.Models;

public class Brand
{
    [Key]
    public string id { set; get; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "Brand name is require")]
    public string Name { get; set; } = default!;
    public string? Desc { get; set; } = default!;
}
