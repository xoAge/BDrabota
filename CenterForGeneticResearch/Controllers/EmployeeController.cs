using CenterForGeneticResearch.Data;
using CenterForGeneticResearch.Helpers;
using CenterForGeneticResearch.Models.Entities;
using CenterForGeneticResearch.Models.Enums;
using CenterForGeneticResearch.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CenterForGeneticResearch.Controllers;

public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _db;

    public EmployeeController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index(string nameFilter, string typeFilter)
    {
        var employees = _db.Employees.AsQueryable();

        if (!string.IsNullOrEmpty(nameFilter))
        {
            employees = employees.Where(e =>
                e.FirstName.Contains(nameFilter) ||
                e.LastName.Contains(nameFilter) ||
                (e.MiddleName != null && e.MiddleName.Contains(nameFilter)));
        }

        if (!string.IsNullOrEmpty(typeFilter))
        {
            employees = employees.Where(e => e.EmployeeType.ToString() == typeFilter);
        }

        var viewModel = new EmployeeFilterVM
        {
            NameFilter = nameFilter,
            TypeFilter = typeFilter,
            Employees = employees.ToList()
        };
    
        return View(viewModel);
    }
    
    public IActionResult Details(int id)
    {
        var employee = _db.Employees.FirstOrDefault(e => e.Id == id);
        if (employee == null) return NotFound();

        var efficiencyIndex = _db.GetEmployeeEfficiencyIndex(id);
        ViewBag.EfficiencyIndex = efficiencyIndex;
    
        ViewBag.UniquePatientsCount = _db.GeneticTests
            .Where(gt => gt.EmployeeId == id)
            .Select(gt => gt.GeneticSample.PatientId)
            .Distinct()
            .Count();

        return View(employee);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.EmployeeTypes = GetEmployeeTypesSelectList();
        
        return View(new Employee());
    }

    [HttpPost]
    public IActionResult Create(Employee model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.EmployeeTypes = GetEmployeeTypesSelectList();
            return View(model);
        }
        
        if (_db.Employees.Any(e => e.Email == model.Email))
        {
            ModelState.AddModelError("Email", "Сотрудник с таким email уже существует");
            ViewBag.EmployeeTypes = GetEmployeeTypesSelectList();
            return View(model);
        }

        if (_db.Employees.Any(e => e.Phone == model.Phone))
        {
            ModelState.AddModelError("Phone", "Сотрудник с таким телефоном уже существует");
            ViewBag.EmployeeTypes = GetEmployeeTypesSelectList();
            return View(model);
        }
    
        model.HireDate = DateTime.Now;
        _db.Employees.Add(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Update(int id)
    {
        var employee = _db.Employees.Find(id);
        if (employee == null)
        {
            return NotFound();
        }

        ViewBag.EmployeeTypes = GetEmployeeTypesSelectList();

        return View(employee);
    }
    
    [HttpPost]
    public IActionResult Update(Employee model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.EmployeeTypes = GetEmployeeTypesSelectList();
            return View(model);
        }
        
        if (_db.Employees.Any(e => e.Email == model.Email && e.Id != model.Id))
        {
            ModelState.AddModelError("Email", "Сотрудник с таким email уже существует");
            ViewBag.EmployeeTypes = GetEmployeeTypesSelectList();
            return View(model);
        }

        if (_db.Employees.Any(e => e.Phone == model.Phone && e.Id != model.Id))
        {
            ModelState.AddModelError("Phone", "Сотрудник с таким телефоном уже существует");
            ViewBag.EmployeeTypes = GetEmployeeTypesSelectList();
            return View(model);
        }

        
        _db.Employees.Update(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var employee = _db.Employees.FirstOrDefault(h => h.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        _db.Employees.Remove(employee);
        _db.SaveChanges();

        return Ok(); 
    }
    
    private SelectList GetEmployeeTypesSelectList()
    {
        return new SelectList(Enum.GetValues<EmployeeType>()
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.GetDisplayName()
            }), "Value", "Text");
    }
}