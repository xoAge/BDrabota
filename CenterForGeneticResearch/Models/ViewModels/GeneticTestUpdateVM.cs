using CenterForGeneticResearch.Models.Entities;

namespace CenterForGeneticResearch.Models.ViewModels;

public class GeneticTestUpdateVM
{
    public GeneticTest Test { get; set; }
    public List<int> SelectedGenes { get; set; }
}