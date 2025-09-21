using CenterForGeneticResearch.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterForGeneticResearch.Data.Configurations;

public class GeneConfiguration : IEntityTypeConfiguration<Gene>
{
    public void Configure(EntityTypeBuilder<Gene> builder)
    {
        builder.HasKey(g => g.Id);
            
        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(25);
                
        builder.Property(g => g.Function)
            .HasMaxLength(50);
                
        builder.Property(g => g.RelatedDiseases)
            .HasMaxLength(300);
        
        builder.HasData(
            new Gene
            {
                Id = 1,
                Name = "BRCA1",
                Function = "Подавление опухолей, репарация ДНК",
                RelatedDiseases = "Рак молочной железы, рак яичников"
            },
            new Gene
            {
                Id = 2,
                Name = "CFTR",
                Function = "Регуляция хлоридных каналов",
                RelatedDiseases = "Муковисцидоз"
            },
            new Gene
            {
                Id = 3,
                Name = "HBB",
                Function = "Кодирование бета-глобина",
                RelatedDiseases = "Серповидноклеточная анемия, бета-талассемия"
            },
            new Gene
            {
                Id = 4,
                Name = "F8",
                Function = "Фактор свертывания крови VIII",
                RelatedDiseases = "Гемофилия A"
            },
            new Gene
            {
                Id = 5,
                Name = "HTT",
                Function = "Кодирование белка хантингтина",
                RelatedDiseases = "Хорея Гентингтона"
            },
            new Gene
            {
                Id = 6,
                Name = "APOE",
                Function = "Транспорт липидов",
                RelatedDiseases = "Болезнь Альцгеймера, гиперлипопротеинемия"
            },
            new Gene
            {
                Id = 7,
                Name = "G6PD",
                Function = "Фермент глюкозо-6-фосфат-дегидрогеназа",
                RelatedDiseases = "Дефицит G6PD, гемолитическая анемия"
            },
            new Gene { Id = 8, Name = "F2", Function = "Протромбин", RelatedDiseases = "Тромбофилия" },
            new Gene { Id = 9, Name = "F5", Function = "Фактор V", RelatedDiseases = "Лейденская тромбофилия" },
            new Gene { Id = 10, Name = "MTHFR", Function = "Метилентетрагидрофолатредуктаза", RelatedDiseases = "Гипергомоцистеинемия" },
            new Gene { Id = 11, Name = "BRCA2", Function = "Подавление опухолей", RelatedDiseases = "Рак молочной железы" },
            new Gene { Id = 12, Name = "CYP2C19", Function = "Метаболизм лекарств", RelatedDiseases = "Нарушения метаболизма лекарств" },
            new Gene { Id = 13, Name = "T21", Function = "Трисомия 21", RelatedDiseases = "Синдром Дауна" },
            new Gene { Id = 14, Name = "T18", Function = "Трисомия 18", RelatedDiseases = "Синдром Эдвардса" },
            new Gene { Id = 15, Name = "LCT", Function = "Лактаза", RelatedDiseases = "Непереносимость лактозы" }
        );
    }
}