using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;                                             

namespace Male.Models;

public class Blog
{
    [Key]
    public string id { set; get; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "Title is require")]
    public string Title { get; set; } = default!;
    public string Desc1 { get; set; } = default!;
    public string Desc2 { get; set; } = default!;
    public string Desc3 { get; set; } = default!;
    public string Desc4 { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string Auth { get; set; } = default!;
    public string Img { get; set; } = default!;

    
    




}
