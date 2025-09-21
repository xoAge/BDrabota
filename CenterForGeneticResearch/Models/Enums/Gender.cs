using System.ComponentModel.DataAnnotations;

namespace CenterForGeneticResearch.Models.Enums;

public enum Gender
{
    [Display(Name = "Мужчина")]
    Male,
        
    [Display(Name = "Женщина")]
    Female,
        
    [Display(Name = "Другое")]
    Other
}