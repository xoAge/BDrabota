namespace CenterForGeneticResearch.Models.ViewModels;

public class EmployeeStatsResult
{
    public int UniquePatientsCount { get; set; }
    public int TotalTestsConducted { get; set; }
    public int AdultPatientsCount { get; set; }
    public int MinorPatientsCount { get; set; }
    public int MalePatientsCount { get; set; }
    public int FemalePatientsCount { get; set; }
    public int UniqueGenesAnalyzed { get; set; }
    public DateTime? FirstTestDate { get; set; }
    public DateTime? LastTestDate { get; set; }
}