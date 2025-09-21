using System.ComponentModel.DataAnnotations;
using CenterForGeneticResearch.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CenterForGeneticResearch.Models.Entities;

public class GeneticSample : IValidatableObject
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Тип образца обязателен")]
    [Display(Name = "Тип образца")]
    public SampleType SampleType { get; set; } 
    
    [Required(ErrorMessage = "Дата сбора обязательна")]
    [Display(Name = "Дата сбора")]
    [DataType(DataType.Date)]
    public DateTime CollectionDate { get; set; }
    
    [Required(ErrorMessage = "Место хранения обязательно")]
    [StringLength(25, ErrorMessage = "Не более 25 символов")]
    [Display(Name = "Место хранения")]
    public string StorageLocation { get; set; }
    
    [Required(ErrorMessage = "Температурный режим обязателен")]
    [StringLength(25, ErrorMessage = "Не более 25 символов")]
    [Display(Name = "Температурный режим")]
    public string TemperatureRegime { get; set; }
    
    [Required(ErrorMessage = "Статус обязателен")]
    [Display(Name = "Статус")]
    public SampleStatus Status { get; set; } 

    [ValidateNever]
    public Patient Patient { get; set; }
    
    [Required(ErrorMessage = "Пациент обязателен")]
    [Display(Name = "Пациент")]
    public int PatientId { get; set; }
    
    [ValidateNever]
    public ICollection<GeneticTest> GeneticTests { get; set; } 
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CollectionDate > DateTime.Now)
        {
            yield return new ValidationResult(
                "Дата сбора не может быть позже текущей даты", 
                new[] { nameof(CollectionDate) });
        }
    }
}