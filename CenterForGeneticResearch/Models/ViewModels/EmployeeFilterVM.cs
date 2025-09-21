using CenterForGeneticResearch.Models.Entities;

namespace CenterForGeneticResearch.Models.ViewModels;

public class EmployeeFilterVM
{
    public string NameFilter { get; set; }
    public string TypeFilter { get; set; }
    public List<Employee> Employees { get; set; } = new();
}