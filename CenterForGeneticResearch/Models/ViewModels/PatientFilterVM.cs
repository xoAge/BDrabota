using CenterForGeneticResearch.Models.Entities;

namespace CenterForGeneticResearch.Models.ViewModels;

public class PatientFilterVM
{
    public string? NameFilter { get; set; }
    public string? GenderFilter { get; set; }
    public List<Patient> Patients { get; set; } = new();
}