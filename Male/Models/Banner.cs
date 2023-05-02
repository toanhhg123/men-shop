using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Male.Models;

public class Banner
{
    [Key]
    public string id { set; get; } = Guid.NewGuid().ToString();

    public string src { set; get; } = default!;

    public string alt { set; get; } = default!;
    public string title { set; get; } = default!;
    public string subTitle { set; get; } = default!;

    public DateTime? createdAt { get; set; } = DateTime.Now;
    public DateTime? updateAt { get; set; } = DateTime.Now;
    public string? updateBy { get; set; }
    public string? createdBy { get; set; }







}
