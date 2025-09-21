using CenterForGeneticResearch.Models.Entities;
using CenterForGeneticResearch.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterForGeneticResearch.Data.Configurations;

public class GeneticSampleConfiguration : IEntityTypeConfiguration<GeneticSample>
{
    public void Configure(EntityTypeBuilder<GeneticSample> builder)
    {
        builder.HasKey(gs => gs.Id);
            
        builder.Property(gs => gs.StorageLocation)
            .HasMaxLength(25);
        
        builder.Property(a => a.SampleType)
            .IsRequired()
            .HasConversion<string>() 
            .HasMaxLength(25);
                
        builder.Property(gs => gs.TemperatureRegime)
            .HasMaxLength(25);
        
        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<string>() 
            .HasMaxLength(25);
                
        builder.HasOne(gs => gs.Patient)
            .WithMany(p => p.GeneticSamples)
            .HasForeignKey(gs => gs.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        builder.HasData(
            new GeneticSample
            {
                Id = 1,
                SampleType = SampleType.Blood,
                CollectionDate = new DateTime(2023, 1, 10),
                StorageLocation = "Холодильник A1",
                TemperatureRegime = "-20°C",
                Status = SampleStatus.ReadyForAnalysis,
                PatientId = 1
            },
            new GeneticSample
            {
                Id = 2,
                SampleType = SampleType.Saliva,
                CollectionDate = new DateTime(2023, 2, 15),
                StorageLocation = "Морозильник B2",
                TemperatureRegime = "-80°C",
                Status = SampleStatus.InAnalysis,
                PatientId = 2
            },
            new GeneticSample
            {
                Id = 3,
                SampleType = SampleType.Biopsy,
                CollectionDate = new DateTime(2023, 3, 5),
                StorageLocation = "Холодильник C3",
                TemperatureRegime = "-196°C",
                Status = SampleStatus.AnalysisComplete,
                PatientId = 3
            },
            new GeneticSample
            {
                Id = 4,
                SampleType = SampleType.Urine,
                CollectionDate = new DateTime(2023, 1, 22),
                StorageLocation = "Морозильник D4",
                TemperatureRegime = "-20°C",
                Status = SampleStatus.Archived,
                PatientId = 4
            },
            new GeneticSample
            {
                Id = 5,
                SampleType = SampleType.Hair,
                CollectionDate = new DateTime(2023, 2, 28),
                StorageLocation = "Шкаф E5",
                TemperatureRegime = "Комнатная",
                Status = SampleStatus.Registered,
                PatientId = 5
            },
            new GeneticSample
            {
                Id = 6,
                SampleType = SampleType.Other,
                CollectionDate = new DateTime(2023, 3, 12),
                StorageLocation = "Холодильник F6",
                TemperatureRegime = "4°C",
                Status = SampleStatus.InProcessing,
                PatientId = 6
            },
            new GeneticSample
            {
                Id = 7,
                SampleType = SampleType.Blood,
                CollectionDate = new DateTime(2023, 1, 5),
                StorageLocation = "Морозильник G7",
                TemperatureRegime = "-80°C",
                Status = SampleStatus.Destroyed,
                PatientId = 7
            }
        );
    }
}