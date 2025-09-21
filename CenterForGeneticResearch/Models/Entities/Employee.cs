using System.ComponentModel.DataAnnotations;
using CenterForGeneticResearch.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CenterForGeneticResearch.Models.Entities;

public class Employee
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
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [StringLength(50, ErrorMessage = "Email не должен превышать 50 символов")]
    [Display(Name = "Email")]
    public string Email { get; set; } 
    
    [Required(ErrorMessage = "Обязательное поле")]
    [Phone(ErrorMessage = "Некорректный телефон")]
    [StringLength(11, ErrorMessage = "Телефон не должен превышать 11 символов")]
    [Display(Name = "Телефон")]
    public string Phone { get; set; } 
    
    [Required(ErrorMessage = "Обязательное поле")]
    [Display(Name = "Должность")]
    public EmployeeType EmployeeType { get; set; } 
    
    [StringLength(500, ErrorMessage = "Биография не должна превышать 500 символов")]
    [Display(Name = "Биография")]
    public string Bio { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [Display(Name = "Дата рождения")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [Range(0, 50, ErrorMessage = "Опыт должен быть от 0 до 50 лет")]
    [Display(Name = "Опыт работы (лет)")]
    public short WorkExperience { get; set; }
    
    [Display(Name = "Дата найма")]
    public DateTime HireDate { get; set; } = DateTime.Now; 

    [ValidateNever]
    public ICollection<GeneticTest> GeneticTests { get; set; }
}