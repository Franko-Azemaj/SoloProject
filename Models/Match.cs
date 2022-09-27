using System.ComponentModel.DataAnnotations;
namespace DevJobMatcher.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class Match {

    [Key]
    public int MatchId { get; set; }
    [Required]
    public int DevProfileId { get; set; }
    [Required]
    public int JobId { get; set; }
    public Job? JobMatched { get; set; }
    public DevProfile? DevProfileMatched { get; set; }

}

