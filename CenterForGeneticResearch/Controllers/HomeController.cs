using CenterForGeneticResearch.Data;
using CenterForGeneticResearch.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CenterForGeneticResearch.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            TotalEmployees = _db.Employees.Count(),
            TotalPatients = _db.Patients.Count(),
            TotalTests = _db.GeneticTests.Count(),
            TotalSamples = _db.GeneticSamples.Count()
        };
        
        return View(model);
    }
    
    public IActionResult UserIndex()
    {
        var model = new HomeViewModel
        {
            TotalEmployees = _db.Employees.Count(),
            TotalPatients = _db.Patients.Count(),
            TotalTests = _db.GeneticTests.Count(),
            TotalSamples = _db.GeneticSamples.Count()
        };
        
        return View(model);
    }
}