using CenterForGeneticResearch.Models.Entities;

namespace CenterForGeneticResearch.Models.ViewModels;

public class GeneFilterVM
{
    public string? NameFilter { get; set; }
    public List<Gene> Genes { get; set; }
}