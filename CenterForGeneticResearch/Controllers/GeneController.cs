using CenterForGeneticResearch.Data;
using CenterForGeneticResearch.Models.Entities;
using CenterForGeneticResearch.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CenterForGeneticResearch.Controllers;

public class GeneController : Controller
{
    private readonly ApplicationDbContext _db;

    public GeneController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index(string? nameFilter)
    {
        var genes = _db.Genes.AsQueryable();

        if (!string.IsNullOrEmpty(nameFilter))
        {
            genes = genes.Where(g => g.Name.Contains(nameFilter));
        }

        var viewModel = new GeneFilterVM
        {
            NameFilter = nameFilter,
            Genes = genes.ToList()
        };
    
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Gene());
    }

    [HttpPost]
    public IActionResult Create(Gene model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
    
        _db.Genes.Add(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var gene = _db.Genes.Find(id);
        if (gene == null)
        {
            return NotFound();
        }

        return View(gene);
    }

    [HttpPost]
    public IActionResult Update(Gene model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _db.Genes.Update(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var gene = _db.Genes.FirstOrDefault(g => g.Id == id);
        if (gene == null)
        {
            return NotFound();
        }

        _db.Genes.Remove(gene);
        _db.SaveChanges();

        return Ok(); 
    }
}