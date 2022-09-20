using System.Collections.Generic;

namespace DevJobMatcher.Models;

public class Skill 
{
    public static IReadOnlySet<Skill> Skills { get; } = new HashSet<Skill>(){
        new Skill{
            Code = 1,
            Name = "C#",
            Image = "assets/C-Sharp.webp"
        },
        new Skill{
            Code = 2,
            Name = "Java",
            Image = "assets/java.jpg"
        }
    }; 

    public int Code { get; set; }
    public string Name { get; set; }
    public string  Image { get; set; }

}

