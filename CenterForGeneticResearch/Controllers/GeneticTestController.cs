using CenterForGeneticResearch.Data;
using CenterForGeneticResearch.Helpers;
using CenterForGeneticResearch.Models.Entities;
using CenterForGeneticResearch.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CenterForGeneticResearch.Controllers;

public class GeneticTestController : Controller
{
    private readonly ApplicationDbContext _db;

    public GeneticTestController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index(string patientFilter, string testNameFilter)
    {
        var tests = _db.GeneticTests
            .Include(t => t.GeneticSample)
                .ThenInclude(s => s.Patient)
            .Include(t => t.GeneRelations)
                .ThenInclude(gr => gr.Gene)
            .AsQueryable();

        if (!string.IsNullOrEmpty(patientFilter))
        {
            tests = tests.Where(t =>
                t.GeneticSample.Patient.LastName.Contains(patientFilter) ||
                t.GeneticSample.Patient.FirstName.Contains(patientFilter) ||
                (t.GeneticSample.Patient.MiddleName != null && 
                 t.GeneticSample.Patient.MiddleName.Contains(patientFilter)));
        }

        if (!string.IsNullOrEmpty(testNameFilter))
        {
            tests = tests.Where(t => t.TestName.Contains(testNameFilter));
        }

        var viewModel = new GeneticTestFilterVM
        {
            PatientFilter = patientFilter,
            TestNameFilter = testNameFilter,
            GeneticTests = tests.Select(t => new GeneticTestWithGenes
            {
                Test = t,
                GeneNames = t.GeneRelations.Select(gr => gr.Gene.Name).ToList()
            }).ToList()
        };
    
        return View(viewModel);
    }
    
    public IActionResult Details(int id)
    {
        var test = _db.GeneticTests
            .Include(t => t.GeneticSample)
                .ThenInclude(s => s.Patient)
            .Include(t => t.Employee)
            .Include(t => t.Conclusion)
            .Include(t => t.GeneRelations)
                .ThenInclude(gr => gr.Gene)
            .FirstOrDefault(t => t.Id == id);

        if (test == null)
        {
            return NotFound();
        }

        var viewModel = new GeneticTestDetailsVM
        {
            Test = test,
            GeneNames = test.GeneRelations?.Select(gr => gr.Gene.Name).ToList() ?? new List<string>(),
            EmployeeInfo = test.Employee != null ? 
                $"{test.Employee.LastName} {test.Employee.FirstName} {test.Employee.MiddleName} ({test.Employee.EmployeeType.GetDisplayName()})" : "Не назначен",
            PatientFullName = $"{test.GeneticSample.Patient.LastName} {test.GeneticSample.Patient.FirstName} {test.GeneticSample.Patient.MiddleName}",
            SampleInfo = $"{test.GeneticSample.SampleType.GetDisplayName()}",
            Conclusion = test.Conclusion
        };

        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Employees = GetEmployeesSelectList();
        ViewBag.GeneticSamples = GetGeneticSamplesSelectList();
        ViewBag.Genes = new SelectList(_db.Genes.ToList(), "Id", "Name");
    
        return View(new GeneticTest());
    }

    [HttpPost]
    public IActionResult Create(GeneticTest model, List<int> SelectedGenes)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Employees = GetEmployeesSelectList(model.EmployeeId);
            ViewBag.GeneticSamples = GetGeneticSamplesSelectList(model.SampleId);
            ViewBag.Genes = new SelectList(_db.Genes.ToList(), "Id", "Name");
            return View(model);
        }

        model.ConductDate = DateTime.Now; 
    
        _db.GeneticTests.Add(model);
        _db.SaveChanges();

        if (SelectedGenes != null && SelectedGenes.Any())
        {
            foreach (var geneId in SelectedGenes)
            {
                _db.GeneTestRelations.Add(new GeneTestRelation
                {
                    TestId = model.Id,
                    GeneId = geneId
                });
            }
            _db.SaveChanges();
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Update(int id)
    {
        var test = _db.GeneticTests
            .Include(t => t.GeneRelations)
            .FirstOrDefault(t => t.Id == id);
    
        if (test == null)
        {
            return NotFound();
        }

        ViewBag.Employees = GetEmployeesSelectList(test.EmployeeId);
        ViewBag.GeneticSamples = GetGeneticSamplesSelectList(test.SampleId);
        ViewBag.Genes = new SelectList(_db.Genes.ToList(), "Id", "Name");
    
        var selectedGenes = test.GeneRelations.Select(g => g.GeneId).ToList();
    
        var model = new GeneticTestUpdateVM
        {
            Test = test,
            SelectedGenes = selectedGenes
        };

        return View(model);
    }
    
    
    [HttpPost]
    public IActionResult Update(GeneticTestUpdateVM model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Employees = GetEmployeesSelectList(model.Test.EmployeeId);
            ViewBag.GeneticSamples = GetGeneticSamplesSelectList(model.Test.SampleId);
            ViewBag.Genes = new SelectList(_db.Genes.ToList(), "Id", "Name");
            return View(model);
        }

        var existingTest =_db.GeneticTests
            .Include(t => t.GeneRelations)
            .FirstOrDefault(t => t.Id == model.Test.Id);

        if (existingTest == null)
        {
            return NotFound();
        }
        
        existingTest.TestName = model.Test.TestName;
        existingTest.AnalysisMethod = model.Test.AnalysisMethod;
        existingTest.SampleId = model.Test.SampleId;
        existingTest.EmployeeId = model.Test.EmployeeId;
        existingTest.Result = model.Test.Result;
        existingTest.Interpretation = model.Test.Interpretation;
        
        
        var currentGeneIds = existingTest.GeneRelations.Select(g => g.GeneId).ToList();
        var selectedGeneIds = model.SelectedGenes ?? new List<int>();

        foreach (var relation in existingTest.GeneRelations.ToList())
        {
            if (!selectedGeneIds.Contains(relation.GeneId))
            {
                _db.GeneTestRelations.Remove(relation);
            }
        }

        foreach (var geneId in selectedGeneIds)
        {
            if (!currentGeneIds.Contains(geneId))
            {
                _db.GeneTestRelations.Add(new GeneTestRelation
                {
                    TestId = existingTest.Id,
                    GeneId = geneId
                });
            }
        }

        _db.SaveChanges();

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var test = _db.GeneticTests.FirstOrDefault(g => g.Id == id);
        if (test == null)
        {
            return NotFound();
        }

        _db.GeneticTests.Remove(test);
        _db.SaveChanges();

        return Ok(); 
    }
    
    private SelectList GetEmployeesSelectList(object selectedValue = null)
    {
        var employees = _db.Employees
            .Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.LastName} {e.FirstName} ({e.EmployeeType.GetDisplayName()})",
                Selected = selectedValue != null && e.Id.ToString() == selectedValue.ToString()
            }).ToList();

        return new SelectList(employees, "Value", "Text");
    }

    private SelectList GetGeneticSamplesSelectList(object selectedValue = null)
    {
        var samples = _db.GeneticSamples
            .Include(s => s.Patient)
            .Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"ID: {s.Id} | {s.SampleType.GetDisplayName()} | Пациент: {s.Patient.LastName} {s.Patient.FirstName}",
                Selected = selectedValue != null && s.Id.ToString() == selectedValue.ToString()
            }).ToList();

        return new SelectList(samples, "Value", "Text");
    }
}