using System.ComponentModel.DataAnnotations;
namespace DevJobMatcher.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;


public class Company
{
    [Key]
    public int CompanyId { get; set; }
    [Required]
    public string CampanyName { get; set; } 
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string City { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required]
    [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string Confirm { get; set; }

    public List<Job> CreatedJobs { get; set; } =  new List<Job>();

}
public class LoginCompany
{
    // No other fields!
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}


