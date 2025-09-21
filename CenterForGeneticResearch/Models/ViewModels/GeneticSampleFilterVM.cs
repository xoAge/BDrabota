using CenterForGeneticResearch.Models.Entities;

namespace CenterForGeneticResearch.Models.ViewModels;

public class GeneticSampleFilterVM
{
    public string? OwnerNameFilter { get; set; }
    public string? SampleTypeFilter { get; set; }
    public string? StatusFilter { get; set; }
    public List<GeneticSample> GeneticSamples { get; set; } = new();
}