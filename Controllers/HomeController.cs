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
                return View("RegisterDev");
            }

            // Initialize hasher object
            var hasher = new PasswordHasher<LoginDev>();

            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);

            // result can be compared to 0 for failure
            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View("RegisterDev");
                // handle failure (this should be similar to how "existing email" is handled)
            }
            HttpContext.Session.SetInt32("userId", userInDb.DevId);

            return RedirectToAction("JobNotice");
        }

        return View("RegisterDev");

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

    [HttpGet("Home/logOut")]
    public IActionResult Logout()
    {

        HttpContext.Session.Clear();
        return RedirectToAction("registerDev");
    }

    // [HttpPost("/addSkillToDB")]
    // public IActionResult CreateSkill( Skill skill)
    // {
    //     _context.Add(skill);
    //     _context.SaveChanges();
    // }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
