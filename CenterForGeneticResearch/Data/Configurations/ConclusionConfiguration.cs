using CenterForGeneticResearch.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterForGeneticResearch.Data.Configurations;

public class ConclusionConfiguration : IEntityTypeConfiguration<Conclusion>
{
    public void Configure(EntityTypeBuilder<Conclusion> builder)
    {
        builder.HasKey(c => c.Id);
            
        builder.Property(c => c.Summary)
            .HasMaxLength(100);
                
        builder.Property(c => c.Recommendations)
            .HasMaxLength(500);
                
        builder.HasOne(c => c.GeneticTest)
            .WithOne(t => t.Conclusion)
            .HasForeignKey<Conclusion>(c => c.TestId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasData(
            new Conclusion
            {
                Id = 1,
                ConclusionDate = new DateTime(2023, 1, 15),
                Summary = "Выявлен высокий риск тромбофилии из-за полиморфизмов в генах F2 и F5",
                Recommendations = "Рекомендована консультация гематолога, контроль коагулограммы 2 раза в год, избегать обезвоживания",
                TestId = 1
            },
            new Conclusion
            {
                Id = 2,
                ConclusionDate = new DateTime(2023, 2, 20),
                Summary = "Мутации в генах BRCA1/BRCA2 не обнаружены",
                Recommendations = "Стандартный скрининг рака молочной железы согласно возрасту",
                TestId = 2
            },
            new Conclusion
            {
                Id = 3,
                ConclusionDate = new DateTime(2023, 3, 10),
                Summary = "Обнаружен генотип CYP2C19*1/*2, указывающий на промежуточный метаболизм",
                Recommendations = "Коррекция дозы клопидогрела, рассмотреть альтернативные антиагреганты",
                TestId = 3
            },
            new Conclusion
            {
                Id = 4,
                ConclusionDate = new DateTime(2023, 1, 28),
                Summary = "Пренатальный скрининг показал низкий риск хромосомных аномалий",
                Recommendations = "Плановое наблюдение беременности, УЗИ в 20-22 недели",
                TestId = 4
            },
            new Conclusion
            {
                Id = 5,
                ConclusionDate = new DateTime(2023, 3, 5),
                Summary = "Отцовство подтверждено с вероятностью 99.99%",
                Recommendations = "Выдать юридическое заключение для судебных целей",
                TestId = 5
            },
            new Conclusion
            {
                Id = 6,
                ConclusionDate = new DateTime(2023, 3, 18),
                Summary = "Выявлены патогенные варианты в генах, связанных с наследственными заболеваниями",
                Recommendations = "Консультация клинического генетика, обследование родственников",
                TestId = 6
            },
            new Conclusion
            {
                Id = 7,
                ConclusionDate = new DateTime(2023, 1, 12),
                Summary = "Подтверждена первичная лактазная недостаточность",
                Recommendations = "Безлактозная диета, препараты лактазы при необходимости",
                TestId = 7
            }
        );
    }
}