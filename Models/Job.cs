using System.ComponentModel.DataAnnotations;
namespace DevJobMatcher.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class Job {

    [Key]
    public int JobId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public List<JobSkill> SkillsNeeded { get; set; } =  new List<JobSkill>();

}

