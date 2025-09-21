using System.ComponentModel.DataAnnotations;

namespace CenterForGeneticResearch.Models.Enums;

public enum EmployeeType
{
    [Display(Name = "Генетик")]
    Geneticist,
    
    [Display(Name = "Врач")]
    Doctor,
        
    [Display(Name = "Медсестра")]
    Nurse,
        
    [Display(Name = "Лаборант")]
    LabTechnician,
        
    [Display(Name = "Исследователь")]
    Researcher,
        
    [Display(Name = "Администратор")]
    Administrator
}
