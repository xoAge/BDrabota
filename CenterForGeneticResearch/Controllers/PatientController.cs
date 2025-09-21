using CenterForGeneticResearch.Data;
using CenterForGeneticResearch.Helpers;
using CenterForGeneticResearch.Models.Entities;
using CenterForGeneticResearch.Models.Enums;
using CenterForGeneticResearch.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CenterForGeneticResearch.Controllers;

public class PatientController : Controller
{
    private readonly ApplicationDbContext _db;

    public PatientController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index(string nameFilter, string genderFilter)
    {
        var patients = _db.Patients.AsQueryable();

        if (!string.IsNullOrEmpty(nameFilter))
        {
            patients = patients.Where(p =>
                p.FirstName.Contains(nameFilter) ||
                p.LastName.Contains(nameFilter) ||
                (p.MiddleName != null && p.MiddleName.Contains(nameFilter)));
        }

        if (!string.IsNullOrEmpty(genderFilter))
        {
            patients = patients.Where(p => p.Gender.ToString() == genderFilter);
        }

        var viewModel = new PatientFilterVM
        {
            NameFilter = nameFilter,
            GenderFilter = genderFilter,
            Patients = patients.ToList()
        };
    
        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Genders = GetGendersSelectList();
        
        return View(new Patient());
    }

    [HttpPost]
    public IActionResult Create(Patient model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Genders = GetGendersSelectList();
            return View(model);
        }
        
        if (_db.Patients.Any(p => p.Phone == model.Phone))
        {
            ModelState.AddModelError("Phone", "Пациент с таким телефоном уже существует");
            ViewBag.Genders = GetGendersSelectList();
            return View(model);
        }
    
        _db.Patients.Add(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Update(int id)
    {
        var patient = _db.Patients.Find(id);
        if (patient == null)
        {
            return NotFound();
        }

        ViewBag.Genders = GetGendersSelectList(patient.Gender);
        
        return View(patient);
    }

    [HttpPost]
    public IActionResult Update(Patient model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Genders = GetGendersSelectList();
            return View(model);
        }

        if (_db.Patients.Any(p => p.Phone == model.Phone && p.Id != model.Id))
        {
            ModelState.AddModelError("Phone", "Пациент с таким телефоном уже существует");
            ViewBag.Genders = GetGendersSelectList();
            return View(model);
        }
        
        _db.Patients.Update(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var patient = _db.Patients.FirstOrDefault(h => h.Id == id);
        if (patient == null)
        {
            return NotFound();
        }

        _db.Patients.Remove(patient);
        _db.SaveChanges();

        return Ok(); 
    }
    
    private SelectList GetGendersSelectList(object selectedValue = null)
    {
        return new SelectList(Enum.GetValues<Gender>()
            .Select(g => new SelectListItem
            {
                Value = g.ToString(),
                Text = g.GetDisplayName(),
                Selected = selectedValue != null && g.ToString() == selectedValue.ToString()
            }), "Value", "Text");
    }
}