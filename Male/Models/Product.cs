using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Male.Models;

public class Product
{
    [Key]
    public string id { set; get; } = default!;

    public string Name { get; set; } = default!;


    [Required]
    [Range(0, 5)]
    public int rating { set; get; } = 5;

    [Required]
    public string description { get; set; } = default!;

    public int Price { get; set; }

    public int CountStock { set; get; }

    public string img1 { set; get; } = default!;
    public string img2 { set; get; } = default!;
    public string img3 { set; get; } = default!;
    public string img4 { set; get; } = default!;
    public Category? category { set; get; }
    public Brand? Brand { set; get; }

}


public class ProductUpload
{
    public string Name { get; set; } = default!;

    public string description { get; set; } = default!;

    public int Price { get; set; }

    public int CountStock { set; get; }

    public List<IFormFile> files { set; get; } = default!;
}
