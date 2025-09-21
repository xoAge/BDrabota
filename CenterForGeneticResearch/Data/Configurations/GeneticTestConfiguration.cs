using CenterForGeneticResearch.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterForGeneticResearch.Data.Configurations;

public class GeneticTestConfiguration : IEntityTypeConfiguration<GeneticTest>
{
    public void Configure(EntityTypeBuilder<GeneticTest> builder)
    {
        builder.HasKey(gt => gt.Id);
            
        builder.Property(gt => gt.TestName)
            .IsRequired()
            .HasMaxLength(75);
                
        builder.Property(gt => gt.AnalysisMethod)
            .HasMaxLength(75);
                
        builder.Property(gt => gt.Result)
            .HasMaxLength(75);
                
        builder.Property(gt => gt.Interpretation)
            .HasMaxLength(200);
                
        builder.HasOne(gt => gt.GeneticSample)
            .WithMany(gs => gs.GeneticTests)
            .HasForeignKey(gt => gt.SampleId)
            .OnDelete(DeleteBehavior.Cascade);
                
        builder.HasOne(gt => gt.Employee)
            .WithMany(e => e.GeneticTests)
            .HasForeignKey(gt => gt.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasData(
            new GeneticTest
            {
                Id = 1,
                TestName = "Генетический профиль сердечно-сосудистых заболеваний",
                ConductDate = new DateTime(2023, 1, 12),
                AnalysisMethod = "NGS-секвенирование",
                Result = "Обнаружены полиморфизмы в генах F2, F5",
                Interpretation = "Повышенный риск тромбофилии",
                SampleId = 1,
                EmployeeId = 1
            },
            new GeneticTest
            {
                Id = 2,
                TestName = "Анализ мутаций BRCA1/BRCA2",
                ConductDate = new DateTime(2023, 2, 18),
                AnalysisMethod = "ПЦР в реальном времени",
                Result = "Мутация BRCA1 не обнаружена",
                Interpretation = "Риск наследственного рака молочной железы в пределах популяционной нормы",
                SampleId = 2,
                EmployeeId = 2
            },
            new GeneticTest
            {
                Id = 3,
                TestName = "Фармакогенетический тест CYP2C19",
                ConductDate = new DateTime(2023, 3, 8),
                AnalysisMethod = "Sanger sequencing",
                Result = "Генотип CYP2C19*1/*2",
                Interpretation = "Сниженный метаболизм некоторых антиагрегантов",
                SampleId = 3,
                EmployeeId = 4
            },
            new GeneticTest
            {
                Id = 4,
                TestName = "Пренатальный скрининг на трисомии",
                ConductDate = new DateTime(2023, 1, 25),
                AnalysisMethod = "qPCR",
                Result = "Низкий риск трисомий 21, 18, 13",
                Interpretation = "Результаты в пределах нормы для срока беременности",
                SampleId = 4,
                EmployeeId = 3
            },
            new GeneticTest
            {
                Id = 5,
                TestName = "Тест на отцовство",
                ConductDate = new DateTime(2023, 3, 2),
                AnalysisMethod = "STR-анализ 16 локусов",
                Result = "99.99% вероятность отцовства",
                Interpretation = "Биологическое отцовство подтверждено",
                SampleId = 5,
                EmployeeId = 5
            },
            new GeneticTest
            {
                Id = 6,
                TestName = "Полногеномное секвенирование",
                ConductDate = new DateTime(2023, 3, 15),
                AnalysisMethod = "WGS",
                Result = "Выявлено 4 патогенных варианта в генах",
                Interpretation = "Рекомендована консультация генетика",
                SampleId = 6,
                EmployeeId = 1
            },
            new GeneticTest
            {
                Id = 7,
                TestName = "Тест на непереносимость лактозы",
                ConductDate = new DateTime(2023, 1, 8),
                AnalysisMethod = "RFLP-анализ",
                Result = "Генотип CC(-13910)",
                Interpretation = "Первичная лактазная недостаточность",
                SampleId = 7,
                EmployeeId = 2
            }
        );
    }
}