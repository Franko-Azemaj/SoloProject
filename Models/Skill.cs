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
            Image = "assets/java.png"
        },
        new Skill{
            Code = 3,
            Name = "Python",
            Image = "assets/python.png"
        },
        new Skill{
            Code = 4,
            Name = "JavaScript",
            Image = "assets/javascript.png"
        },
        new Skill{
            Code = 5,
            Name = "Sql",
            Image = "assets/sql.png"
        },
        new Skill{
            Code = 6,
            Name = "Scala",
            Image = "assets/scala.png"
        },
        new Skill{
            Code = 7,
            Name = "Html",
            Image = "assets/html5.png"
        },
        new Skill{
            Code = 8,
            Name = "CSS",
            Image = "assets/css.png"
        },
        new Skill{
            Code = 9,
            Name = "Perl",
            Image = "assets/perl.png"
        },
        new Skill{
            Code = 10,
            Name = "C",
            Image = "assets/clogo.png"
        },
        new Skill{
            Code = 11,
            Name = "C++",
            Image = "assets/C++.png"
        },
        new Skill{
            Code = 12,
            Name = "Objective-C",
            Image = "assets/objective-c.png"
        },
        new Skill{
            Code = 13,
            Name = "Php",
            Image = "assets/php.png"
        },
        new Skill{
            Code = 14,
            Name = "Ruby",
            Image = "assets/ruby2.png"
        },
       
        new Skill{
            Code = 15,
            Name = "Swift",
            Image = "assets/swiftt.png"
        },
        new Skill{
            Code = 16,
            Name = "TypeScript",
            Image = "assets/typeScript.png"
        }
    }; 

    public int Code { get; set; }
    public string Name { get; set; }
    public string  Image { get; set; }

}

