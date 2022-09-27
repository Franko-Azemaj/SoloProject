using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevJobMatcher.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DevJobMatcher.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet("/")]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return RedirectToAction("RegisterDev");
        }
        int id = (int)HttpContext.Session.GetInt32("userId");

        ViewBag.CurrentProfile = _context.DevProfiles.FirstOrDefault(e => e.DevId == id);
        int profileId = ViewBag.CurrentProfile.DevProfileId;
         ViewBag.Matches = _context.Matches.Include(e => e.JobMatched)
            .ThenInclude(e => e.Creator)
            .Include(e => e.JobMatched)
            .ThenInclude(e => e.SkillsNeeded)
            .Where(e => e.DevProfileId == profileId).ToList();

        return View();
    }



    [HttpGet("RegisterDev")]
    public IActionResult RegisterDev()
    {

        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return View();
        }

        return RedirectToAction("Index");

    }

    [HttpPost("RegisterDev")]
    public IActionResult RegisterDev(Dev dev)
    {
        // Check initial ModelState
        if (ModelState.IsValid)
        {
            // If a User exists with provided email
            if (_context.Devs.Any(u => u.Email == dev.Email))
            {
                // Manually add a ModelState error to the Email field, with provided
                // error message
                ModelState.AddModelError("UserName", "UserName already in use!");

                return View();
                // You may consider returning to the View at this point
            }
            PasswordHasher<Dev> Hasher = new PasswordHasher<Dev>();
            dev.Password = Hasher.HashPassword(dev, dev.Password);
            _context.Devs.Add(dev);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", dev.DevId);

            return RedirectToAction("JobNotice");
        }
        return View();
    }

    [HttpGet("LoginDev")]
    public IActionResult LoginDev()
    {
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return View("DevLogin");
        }
        return RedirectToAction("Index");
    }

    [HttpPost("LoginDev")]
    public IActionResult LoginSubmit(LoginDev userSubmission)
    {
        if (ModelState.IsValid)
        {
            // If initial ModelState is valid, query for a user with provided email
            var userInDb = _context.Devs.FirstOrDefault(u => u.Email == userSubmission.Email);
            // If no user exists with provided email
            if (userInDb == null)
            {
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("User", "Invalid Email/Password");
                return View("DevLogin");
            }

            // Initialize hasher object
            var hasher = new PasswordHasher<LoginDev>();

            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);

            // result can be compared to 0 for failure
            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View("DevLogin");
                // handle failure (this should be similar to how "existing email" is handled)
            }
            HttpContext.Session.SetInt32("userId", userInDb.DevId);

            return RedirectToAction("index");
        }
        return View("DevLogin");
    }

    [HttpGet("CreateJobNotice")]
    public IActionResult JobNotice()
    {
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return RedirectToAction("RegisterDev");
        }

        return View("CreateJobNotice");
    }

    public class JobNoticeModel
    {
        public string Description { get; set; }
        public int[] skills { get; set; }
    }

    [HttpPost("AddJobNotice")]
    public IActionResult AddJobNotice([FromForm] JobNoticeModel notice)
    {
        
            
        if (ModelState.IsValid)
        {
            int id = (int)HttpContext.Session.GetInt32("userId");

            if(notice.skills.Count() > 5)
            return BadRequest();

            DevProfile devprofile = new DevProfile
            {
                Bio = notice.Description,
                DevId = id
            };

            _context.DevProfiles.Add(devprofile);


            //All skills
            List<Skill> allSkills = new List<Skill>();
            foreach (var skill in Skill.Skills)
            {
                allSkills.Add(skill);
            }

            foreach (var code in notice.skills)
            {

                ViewBag.Skill = allSkills.FirstOrDefault(e => e.Code == code);

                SelectedSkill selectedSkill = new SelectedSkill
                {
                    Creator = devprofile,
                    Name = ViewBag.Skill.Name,
                    Image = ViewBag.Skill.Image
                };

                _context.SelectedSkills.Add(selectedSkill);

            }

            _context.SaveChanges();

        return RedirectToAction("index");

        }

        return View("CreateJobNotice");
    }

    [HttpGet("Home/logOut")]
    public IActionResult Logout()
    {

        HttpContext.Session.Clear();
        return RedirectToAction("registerDev");
    }

    [HttpGet("Home/newLogin")]
    public IActionResult NewLogin(){

        return View("newLogin");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
