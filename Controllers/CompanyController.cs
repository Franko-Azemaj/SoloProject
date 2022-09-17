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

    


}
