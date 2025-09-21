using CenterForGeneticResearch.Models.Entities;

namespace CenterForGeneticResearch.Models.ViewModels;

public class ConclusionFilterVM
{
    public string TestNameFilter { get; set; }
    public DateTime? ConclusionDateFilter { get; set; }
    public List<Conclusion> Conclusions { get; set; }
}