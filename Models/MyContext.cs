#pragma warning disable CS8618
/* 
Disabled Warning: "Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable."
We can disable this safely because we know the framework will assign non-null values when it constructs this class for us.
*/
using Microsoft.EntityFrameworkCore;
namespace DevJobMatcher.Models;
// the MyContext class representing a session with our MySQL database, allowing us to query for or save data
public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<Dev> Devs { get; set; } 
    public DbSet<DevProfile> DevProfiles { get; set; } 
    public DbSet<Company> Companies { get; set; } 
    public DbSet<Job> Jobs { get; set; } 
    public DbSet<SelectedSkill> SelectedSkills { get; set; } 

}
