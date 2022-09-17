using System.ComponentModel.DataAnnotations;
namespace DevJobMatcher.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class Skill 
{

    [Key]
    public int SkillId { get; set; }
    [Required]
    public string SkillName { get; set; }
    [Required]
    public byte[]  Image { get; set; }

}

