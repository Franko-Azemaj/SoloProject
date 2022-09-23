using System.ComponentModel.DataAnnotations;
namespace DevJobMatcher.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class JobSkill {

    [Key]
    public int JobSkillId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string  Image { get; set; }
    public int JobId { get; set; }
    public Job? SkillCreator { get; set; }

}

