namespace CenterForGeneticResearch.Models.Entities;

public class GeneTestRelation
{
    public int GeneId { get; set; }    
    public Gene Gene { get; set; }

    public int TestId { get; set; } 
    public GeneticTest GeneticTest { get; set; }
}