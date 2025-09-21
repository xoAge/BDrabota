using CenterForGeneticResearch.Models.Entities;

namespace CenterForGeneticResearch.Models.ViewModels;

public class GeneticTestFilterVM
{
    public string PatientFilter { get; set; }
    public string TestNameFilter { get; set; }
    public List<GeneticTestWithGenes> GeneticTests { get; set; }
}

public class GeneticTestWithGenes
{
    public GeneticTest Test { get; set; }
    public List<string> GeneNames { get; set; }
}