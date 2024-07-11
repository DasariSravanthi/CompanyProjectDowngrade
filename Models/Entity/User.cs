using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class User 
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Column(TypeName = "varchar(256)")]
    public string Username { get; set; } = null!;

    [Column(TypeName = "varchar(256)")]
    public string Password { get; set; } = null!;

    [Column(TypeName = "varchar(256)")]
    public string Email { get; set; } = null!;    

    [Column(TypeName = "varchar(256)")]
    public string Role { get; set; } = null!;
}