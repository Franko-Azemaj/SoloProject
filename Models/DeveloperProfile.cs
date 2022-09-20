using System.ComponentModel.DataAnnotations;
namespace DevJobMatcher.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class DevProfile
{
    [Key]
    public int DevProfileId { get; set; }
    [Required]
    public string Bio { get; set; }
    [Required]
    public int SkillCode { get; set; }
    [Required]
    public string SkillName { get; set; }
    [Required]
    public string SkillImage { get; set; }

}
