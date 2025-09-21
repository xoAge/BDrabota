using System.ComponentModel.DataAnnotations;

namespace CenterForGeneticResearch.Models.Enums;

public enum SampleStatus
{
    [Display(Name = "Зарегистрирован")]
    Registered,      
    
    [Display(Name = "В обработке")]
    InProcessing,     
    
    [Display(Name = "Готов к анализу")]
    ReadyForAnalysis,
    
    [Display(Name = "В анализе")]
    InAnalysis,       
    
    [Display(Name = "Анализ завершен")]
    AnalysisComplete, 
    
    [Display(Name = "Архивирован")]
    Archived,        
    
    [Display(Name = "Испорчен")]
    Spoiled,         
    
    [Display(Name = "Уничтожен")]
    Destroyed
}