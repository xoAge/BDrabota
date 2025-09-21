using CenterForGeneticResearch.Data.Configurations;
using CenterForGeneticResearch.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CenterForGeneticResearch.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }   
    
    public DbSet<Conclusion> Conclusions { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Gene> Genes { get; set; }
    public DbSet<GeneTestRelation> GeneTestRelations { get; set; }
    public DbSet<GeneticSample> GeneticSamples { get; set; }
    public DbSet<GeneticTest> GeneticTests { get; set; }
    public DbSet<Patient> Patients { get; set; }

    public decimal GetEmployeeEfficiencyIndex(int EmployeeId) 
    {
        try
        {
            return Database
                .SqlQueryRaw<decimal>("EXEC GetEmployeeEfficiencyIndex @EmployeeId", 
                    new SqlParameter("@EmployeeId", EmployeeId))
                .AsEnumerable()
                .FirstOrDefault();
        }
        catch
        {
            return 0.0m;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ConclusionConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new GeneConfiguration());
        modelBuilder.ApplyConfiguration(new GeneTestRelationConfiguration());
        modelBuilder.ApplyConfiguration(new GeneticSampleConfiguration());
        modelBuilder.ApplyConfiguration(new GeneticTestConfiguration());
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
    }
}