using System.ComponentModel.DataAnnotations;

namespace webapi.Models;

public class AuthModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}