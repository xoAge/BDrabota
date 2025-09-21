using System.ComponentModel.DataAnnotations;
using CenterForGeneticResearch.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CenterForGeneticResearch.Models.Entities;

public class Patient
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(25, ErrorMessage = "Имя не должно превышать 25 символов")]
    [Display(Name = "Имя")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(25, ErrorMessage = "Фамилия не должна превышать 25 символов")]
    [Display(Name = "Фамилия")]
    public string LastName { get; set; }
    
    [StringLength(25, ErrorMessage = "Отчество не должно превышать 25 символов")]
    [Display(Name = "Отчество")]
    public string? MiddleName { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [Display(Name = "Дата рождения")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [Display(Name = "Пол")]
    public Gender Gender { get; set; } 
    
    [Required(ErrorMessage = "Обязательное поле")]
    [Phone(ErrorMessage = "Некорректный номер телефона")]
    [StringLength(11, ErrorMessage = "Телефон не должен превышать 11 символов")]
    [Display(Name = "Телефон")]
    public string Phone { get; set; } 
    
    [ValidateNever]
    public ICollection<GeneticSample> GeneticSamples { get; set; }
}