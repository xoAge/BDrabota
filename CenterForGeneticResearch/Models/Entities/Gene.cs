using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CenterForGeneticResearch.Models.Entities;

public class Gene
{
    [Key]
    
    public int Id { get; set; }
    [Required(ErrorMessage = "Название гена обязательно")]
    [StringLength(25, ErrorMessage = "Название гена не должно превышать 25 символов")]
    [Display(Name = "Название гена")]
    public string Name { get; set; }      
    
    [StringLength(50, ErrorMessage = "Описание функции не должно превышать 50 символов")]
    [Display(Name = "Функция")]
    public string Function { get; set; }     
    
    [StringLength(300, ErrorMessage = "Список заболеваний не должен превышать 300 символов")]
    [Display(Name = "Связанные заболевания")]
    public string RelatedDiseases { get; set; } 
    
    [ValidateNever]
    public ICollection<GeneTestRelation> TestRelations { get; set; }
}