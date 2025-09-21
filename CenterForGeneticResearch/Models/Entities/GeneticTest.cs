using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CenterForGeneticResearch.Models.Entities;

public class GeneticTest
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Название теста обязательно")]
    [StringLength(75, ErrorMessage = "Название теста не должно превышать 75 символов")]
    [Display(Name = "Название теста")]
    public string TestName { get; set; }
    
    [Display(Name = "Дата проведения")]
    public DateTime ConductDate { get; set; }
    
    [StringLength(75, ErrorMessage = "Метод анализа не должен превышать 75 символов")]
    [Display(Name = "Метод анализа")]
    public string AnalysisMethod { get; set; }
    
    [StringLength(75, ErrorMessage = "Результат не должен превышать 75 символов")]
    [Display(Name = "Результат")]
    public string Result { get; set; }
    
    [StringLength(200, ErrorMessage = "Интерпретация не должна превышать 200 символов")]
    [Display(Name = "Интерпретация")]
    public string Interpretation { get; set; }
    
    [ValidateNever]
    public GeneticSample GeneticSample { get; set; }
    
    [Required(ErrorMessage = "Образец обязателен")]
    [Display(Name = "Образец")]
    public int SampleId { get; set; }
    
    [ValidateNever]
    public Employee Employee { get; set; }
    [Display(Name = "Ответственный сотрудник")]
    public int? EmployeeId { get; set; }
    
    [ValidateNever]
    public Conclusion Conclusion { get; set; }
    [ValidateNever]
    public ICollection<GeneTestRelation> GeneRelations { get; set; }
}