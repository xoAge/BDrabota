using CenterForGeneticResearch.Data;
using CenterForGeneticResearch.Helpers;
using CenterForGeneticResearch.Models.Entities;
using CenterForGeneticResearch.Models.Enums;
using CenterForGeneticResearch.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CenterForGeneticResearch.Controllers;

public class GeneticSampleController : Controller
{
    private readonly ApplicationDbContext _db;

    public GeneticSampleController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index(string ownerNameFilter, string sampleTypeFilter, string statusFilter)
    {
        var samples = _db.GeneticSamples
            .Include(gs => gs.Patient)
            .AsQueryable();

        if (!string.IsNullOrEmpty(ownerNameFilter))
        {
            samples = samples.Where(gs =>
                gs.Patient.FirstName.Contains(ownerNameFilter) ||
                gs.Patient.LastName.Contains(ownerNameFilter) ||
                (gs.Patient.MiddleName != null && gs.Patient.MiddleName.Contains(ownerNameFilter)));
        }

        if (!string.IsNullOrEmpty(sampleTypeFilter))
        {
            samples = samples.Where(gs => gs.SampleType.ToString() == sampleTypeFilter);
        }

        if (!string.IsNullOrEmpty(statusFilter))
        {
            samples = samples.Where(gs => gs.Status.ToString() == statusFilter);
        }

        var viewModel = new GeneticSampleFilterVM
        {
            OwnerNameFilter = ownerNameFilter,
            SampleTypeFilter = sampleTypeFilter,
            StatusFilter = statusFilter,
            GeneticSamples = samples.ToList()
        };
    
        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Patients = GetPatientsSelectList();
        ViewBag.SampleTypes = GetSampleTypesSelectList();
        ViewBag.Statuses = GetStatusesSelectList();

        return View();
    }
    
    [HttpPost]
    public IActionResult Create(GeneticSample model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Patients = GetPatientsSelectList(model.PatientId);
            ViewBag.SampleTypes = GetSampleTypesSelectList();
            ViewBag.Statuses = GetStatusesSelectList();

            return View(model);
        }

        model.CollectionDate = DateTime.Now;
        _db.GeneticSamples.Add(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Update(int id)
    {
        var sample = _db.GeneticSamples.Find(id);
        if (sample == null)
        {
            return NotFound();
        }
    
        ViewBag.Patients = GetPatientsSelectList(sample.PatientId);
        ViewBag.SampleTypes = GetSampleTypesSelectList(sample.SampleType);
        ViewBag.Statuses = GetStatusesSelectList(sample.Status);
    
        return View(sample);
    }
    
    [HttpPost]
    public IActionResult Update(GeneticSample model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Patients = GetPatientsSelectList(model.PatientId);
            ViewBag.SampleTypes = GetSampleTypesSelectList();
            ViewBag.Statuses = GetStatusesSelectList();
    
            return View(model);
        }
    
        _db.GeneticSamples.Update(model);
        _db.SaveChanges();
    
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var sample = _db.GeneticSamples.FirstOrDefault(h => h.Id == id);
        if (sample == null)
        {
            return NotFound();
        }

        _db.GeneticSamples.Remove(sample);
        _db.SaveChanges();

        return Ok(); 
    }
    
    private SelectList GetPatientsSelectList(object selectedValue = null)
    {
        return new SelectList(_db.Patients
                .Select(p => new {
                    Id = p.Id,
                    FullName = $"{p.LastName} {p.FirstName} {p.MiddleName}"
                }),
            "Id", "FullName", selectedValue);
    }

    private SelectList GetSampleTypesSelectList(object selectedValue = null)
    {
        return new SelectList(Enum.GetValues<SampleType>()
            .Select(t => new SelectListItem
            {
                Value = t.ToString(),
                Text = t.GetDisplayName(),
                Selected = selectedValue != null && t.ToString() == selectedValue.ToString()
            }), "Value", "Text");
    }

    private SelectList GetStatusesSelectList(object selectedValue = null)
    {
        return new SelectList(Enum.GetValues<SampleStatus>()
            .Select(s => new SelectListItem
            {
                Value = s.ToString(),
                Text = s.GetDisplayName(),
                Selected = selectedValue != null && s.ToString() == selectedValue.ToString()
            }), "Value", "Text");
    }

}