using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Male.Models;

public class Contact
{
    [Key]
    public string id { set; get; } = Guid.NewGuid().ToString();

    public string name { get; set; } = default!;
    public string address { get; set; } = default!;
    public string phoneNumber { get; set; } = default!;



}
