using CenterForGeneticResearch.Data;
using CenterForGeneticResearch.Models.Entities;
using CenterForGeneticResearch.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CenterForGeneticResearch.Controllers;

public class ConclusionController : Controller
{
    private readonly ApplicationDbContext _db;

    public ConclusionController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index(string testNameFilter, DateTime? conclusionDateFilter)
    {
        var conclusions = _db.Conclusions
            .Include(c => c.GeneticTest)
            .AsQueryable();

        if (!string.IsNullOrEmpty(testNameFilter))
        {
            conclusions = conclusions.Where(c => 
                c.GeneticTest.TestName.Contains(testNameFilter));
        }

        if (conclusionDateFilter.HasValue)
        {
            conclusions = conclusions.Where(c => 
                c.ConclusionDate.Date == conclusionDateFilter.Value.Date);
        }

        var viewModel = new ConclusionFilterVM
        {
            TestNameFilter = testNameFilter,
            ConclusionDateFilter = conclusionDateFilter,
            Conclusions = conclusions.ToList()
        };

        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Tests = GetTestsWithoutConclusions();
        
        return View(new Conclusion { ConclusionDate = DateTime.Now });
    }

    [HttpPost]
    public IActionResult Create(Conclusion model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Tests = GetTestsWithoutConclusions();
            return View(model);
        }

        _db.Conclusions.Add(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Update(int id)
    {
        var conclusion = _db.Conclusions
            .Include(c => c.GeneticTest)
                .ThenInclude(t => t.GeneticSample)
                    .ThenInclude(s => s.Patient)
            .FirstOrDefault(c => c.Id == id);
        
        if (conclusion == null)
        {
            return NotFound();
        }
        
        ViewBag.TestInfo = $"Тест: {conclusion.GeneticTest.TestName} (Пациент: {conclusion.GeneticTest.GeneticSample.Patient.LastName} " +
           $"{conclusion.GeneticTest.GeneticSample.Patient.FirstName})";
    
        return View(conclusion);
    }

    [HttpPost]
    public IActionResult Update(Conclusion model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        _db.Conclusions.Update(model);
        _db.SaveChanges();
        
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var conclusion = _db.Conclusions.FirstOrDefault(h => h.Id == id);
        if (conclusion == null)
        {
            return NotFound();
        }

        _db.Conclusions.Remove(conclusion);
        _db.SaveChanges();

        return Ok(); 
    }
    
    
    private SelectList GetTestsWithoutConclusions()
    {
        var tests = _db.GeneticTests
            .Include(t => t.GeneticSample)
                .ThenInclude(s => s.Patient)
            .Where(t => !_db.Conclusions.Any(c => c.TestId == t.Id))
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $"{t.TestName} (Пациент: {t.GeneticSample.Patient.LastName} {t.GeneticSample.Patient.FirstName})"
            })
            .ToList();

        return new SelectList(tests, "Value", "Text");
    }
}