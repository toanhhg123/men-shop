using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Male.Models;

public class Account
{
    [Key]
    public string id { set; get; } = default!;

    [Required(ErrorMessage = "User name is require")]
    [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "account name has at least 6 characters")]
    public string userName { set; get; } = default!;

    [Required(ErrorMessage = "email is require")]
    [EmailAddress(ErrorMessage = "is valid email")]
    public string email { set; get; } = default!;

    [Required(ErrorMessage = "Password is require")]
    [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "password has at least 6 characters")]
    public string hashPassword { set; get; } = default!;

    [Required]
    public string salt { get; set; } = default!;

    public string? address { get; set; } = default!;

    public string? phoneNumber { get; set; } = default!;

    public string? img {set;get;}  = default!;


    public Role Role { set; get; } = default!;

}

public class UserProfile
{
    [Required(ErrorMessage = "User name is require")]
    [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "account name has at least 6 characters")]
    public string userName { set; get; } = default!;
   
    public string? address { get; set; } = default!;

    public string? phoneNumber { get; set; } = default!;

    public string? img {set;get;}  = default!;

}


public class UserRegister {
   

    [Required(ErrorMessage = "User name is require")]
    [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "account name has at least 6 characters")]
    public string userName { set; get; } = default!;

    [Required(ErrorMessage = "email is require")]
    [EmailAddress(ErrorMessage = "is valid email")]
    public string email { set; get; } = default!;

    [Required]    
    [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "password has at least 6 characters")]
    public string password { get; set; } = default!;

    [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "password has at least 6 characters")]
    public string? confirmPassword { get; set; } = default!;
  
    public string? address { get; set; } = default!;

    public string? phoneNumber { get; set; } = default!;

    public string? img {set;get;}  = default!;

}
