using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CenterForGeneticResearch.Models.Entities;

public class Conclusion
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [Display(Name = "Дата заключения")]
    [DataType(DataType.DateTime)]
    public DateTime ConclusionDate { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(100, ErrorMessage = "Сводка не должна превышать 100 символов")]
    [Display(Name = "Сводка")]
    public string Summary { get; set; }

    public int? number { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(500, ErrorMessage = "Рекомендации не должны превышать 500 символов")]
    [Display(Name = "Рекомендации")]
    public string Recommendations { get; set; }
    
    [ValidateNever]
    public GeneticTest GeneticTest { get; set; }
    [Required(ErrorMessage = "Обязательное поле")]
    [Display(Name = "Тест")]
    public int TestId { get; set; }
}