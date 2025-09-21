using System.ComponentModel.DataAnnotations;

namespace CenterForGeneticResearch.Models.Enums;

public enum SampleType
{
    [Display(Name = "Кровь")]
    Blood,
        
    [Display(Name = "Слюна")]
    Saliva,
        
    [Display(Name = "Волосы")]
    Hair,
        
    [Display(Name = "Биопсия")]
    Biopsy,
        
    [Display(Name = "Моча")]
    Urine,
        
    [Display(Name = "Другое")]
    Other
}