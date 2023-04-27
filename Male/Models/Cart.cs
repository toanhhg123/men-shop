using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Male.Models;

public class Cart
{
    [Key]
    public string id { set; get; } = Guid.NewGuid().ToString();

    public Account Account { set; get; } = default!;

    public Product product { set; get; } = default!;

    public int Quantity { set; get; } = default!;

}
