using CenterForGeneticResearch.Models.Entities;

namespace CenterForGeneticResearch.Models.ViewModels;

public class GeneticTestDetailsVM
{
    public GeneticTest Test { get; set; }
    public List<string> GeneNames { get; set; }
    public string EmployeeInfo { get; set; }
    public string PatientFullName { get; set; }
    public string SampleInfo { get; set; }
    public Conclusion Conclusion { get; set; }
}