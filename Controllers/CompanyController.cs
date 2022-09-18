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

    public CompanyController(ILogger<HomeController> logger,MyContext context)
    {
        _logger = logger;
        _context = context;
    }

     [HttpGet("Companyindex")]
    public ActionResult CompanyIndex()
    {   
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return RedirectToAction("RegisterCompany");
        }

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
                return View("RegisterCompany");
            }

            // Initialize hasher object
            var hasher = new PasswordHasher<LoginCompany>();

            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);

            // result can be compared to 0 for failure
            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View("RegisterCompany");
                // handle failure (this should be similar to how "existing email" is handled)
            }
            HttpContext.Session.SetInt32("userId", userInDb.CompanyId);

            return RedirectToAction("Companyindex");
        }

        return View("RegisterCompany");

    }




}
