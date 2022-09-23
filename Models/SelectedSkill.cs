using System.ComponentModel.DataAnnotations;
namespace DevJobMatcher.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class SelectedSkill {

    [Key]
    public int SelectedSkillId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string  Image { get; set; }
    public int DevProfileId { get; set; }
    public DevProfile? Creator { get; set; }

}

