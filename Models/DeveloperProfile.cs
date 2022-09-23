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
    public IList<SelectedSkill> SelectedSkills { get; set; }
    [Required]
    public int DevId { get; set; }
    public Dev? Creator { get; set; }

}
