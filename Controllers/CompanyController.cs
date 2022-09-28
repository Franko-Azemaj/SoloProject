using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevJobMatcher.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DevJobMatcher.Controllers;

public class CompanyController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public CompanyController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("Companyindex")]
    public ActionResult CompanyIndex()
    {
        //Check if the user is loged in or not 
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return RedirectToAction("RegisterCompany");
        }
        //All developers available
        ViewBag.AllDevs = _context.DevProfiles
            .Include(d => d.Creator)
            .Include(m => m.SelectedSkills)
            .ToList();


        //All Positions to fill
        ViewBag.AllPositions = _context.Jobs.ToList();

        return View();
    }

    [HttpGet("Company/RegisterCompany")]
    public IActionResult RegisterCompany()
    {

        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return View();
        }

        return RedirectToAction("Companyindex");

    }

    [HttpPost("RegisterCompany")]
    public IActionResult RegisterCompany(Company company)
    {
        // Check initial ModelState
        if (ModelState.IsValid)
        {
            // If a User exists with provided email
            if (_context.Companies.Any(c => c.Email == company.Email))
            {
                // Manually add a ModelState error to the Email field, with provided
                // error message
                ModelState.AddModelError("UserName", "UserName already in use!");

                return View();
                // You may consider returning to the View at this point
            }
            PasswordHasher<Company> Hasher = new PasswordHasher<Company>();
            company.Password = Hasher.HashPassword(company, company.Password);
            _context.Companies.Add(company);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", company.CompanyId);

            return RedirectToAction("Companyindex");
        }
        return View();
    }

    [HttpGet("Company/LoginCompany")]
    public IActionResult LoginCompany()
    {

        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return View("CompanyLogin");
        }

        return RedirectToAction("Companyindex");

    }

    [HttpPost("LoginCompany")]
    public IActionResult LoginSubmit(LoginCompany userSubmission)
    {
        if (ModelState.IsValid)
        {
            // If initial ModelState is valid, query for a user with provided email
            var userInDb = _context.Companies.FirstOrDefault(u => u.Email == userSubmission.Email);
            // If no user exists with provided email
            if (userInDb == null)
            {
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("User", "Invalid Email/Password");
                return View("CompanyLogin");
            }

            // Initialize hasher object
            var hasher = new PasswordHasher<LoginCompany>();

            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);

            // result can be compared to 0 for failure
            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View("CompanyLogin");
                // handle failure (this should be similar to how "existing email" is handled)
            }
            HttpContext.Session.SetInt32("userId", userInDb.CompanyId);

            return RedirectToAction("Companyindex");
        }

        return View("CompanyLogin");


    }

    [HttpGet("/jobs/new")]
    public IActionResult CreatePosition()
    {
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return View("CompanyLogin");
        }

        return View("CreatePosition");
    }

    //Here is a temporary model that helps in getting both the job fields and skills 
    public class PositionModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] skills { get; set; }
    }


    [HttpPost("CreatePosition")]
    public IActionResult CreatePosition([FromForm] PositionModel position)
    {
        if (ModelState.IsValid)
        {
            int id = (int)HttpContext.Session.GetInt32("userId");

            Job newJob = new Job
            {
                Name = position.Name,
                Description = position.Description,
                CompanyId = id
            };
            _context.Jobs.Add(newJob);

            List<Skill> AllSkills = new List<Skill>();
            //adding all the skills in this temporary list 
            foreach (var skill in Skill.Skills)
            {
                AllSkills.Add(skill);
            }

            foreach (var code in position.skills)
            {      
                //getting the skill by code 
                // code is just an unique number
                ViewBag.skill = AllSkills.FirstOrDefault(e => e.Code == code);
                //adding skills to the database
                JobSkill newJobSkill = new JobSkill
                {
                    SkillCreator = newJob,
                    Name = ViewBag.skill.Name,
                    Image = ViewBag.skill.Image,
                };

                _context.JobSkills.Add(newJobSkill);
            }
            _context.SaveChanges();
            //Getting all the developers
            var DevProf = _context.DevProfiles.Include(e => e.SelectedSkills).ToList();
            //The current job 
            var CurrentJob = _context.Jobs.Include(e => e.SkillsNeeded).FirstOrDefault(e => e.JobId == newJob.JobId);
            int count = 0;
            //foreach developer we check if all the skills that he has are the same skills that the job requires
            foreach (var devP in DevProf)
            {
                foreach (var job in devP.SelectedSkills)
                {
                    foreach (var jobskill in CurrentJob.SkillsNeeded)
                    {
                        if (job.Name == jobskill.Name)
                        {
                            count++;
                        }
                    }

                    if (count == 5)
                    {
                        Match newMatch = new Match
                        {
                            DevProfileMatched = devP,
                            JobMatched = CurrentJob
                        };
                        _context.Matches.Add(newMatch);
                        count = 0;
                    }
                }

            }
            _context.SaveChanges();

            return RedirectToAction("CompanyIndex");
        }

        return View("CreatePosition");
    }

    [HttpGet("jobs/{id}")]
    public IActionResult ShowMatches(int id)
    {
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return View("CompanyLogin");
        }

        ViewBag.Matches = _context.Matches.Include(e => e.DevProfileMatched)
    .ThenInclude(e => e.SelectedSkills)
    .Include(e => e.DevProfileMatched)
    .ThenInclude(e => e.Creator)
            .Where(e => e.JobId == id).ToList();

        return View("ShowMatches");
    }

    [HttpGet("logOut")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("RegisterCompany");
    }

}


