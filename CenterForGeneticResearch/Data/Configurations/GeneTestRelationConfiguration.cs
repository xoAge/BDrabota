using CenterForGeneticResearch.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterForGeneticResearch.Data.Configurations;

public class GeneTestRelationConfiguration : IEntityTypeConfiguration<GeneTestRelation>
{
    public void Configure(EntityTypeBuilder<GeneTestRelation> builder)
    {
        builder.HasKey(tr => new { tr.GeneId, tr.TestId });
            
        builder.HasOne(tr => tr.Gene)
            .WithMany(g => g.TestRelations)
            .HasForeignKey(tr => tr.GeneId);
                
        builder.HasOne(tr => tr.GeneticTest)
            .WithMany(t => t.GeneRelations)
            .HasForeignKey(tr => tr.TestId);
        
        builder.HasData(
            // Кардиогенетический тест (1) связан с генами, влияющими на свертываемость крови
            new GeneTestRelation { GeneId = 8, TestId = 1 }, 
            new GeneTestRelation { GeneId = 9, TestId = 1 }, 
            new GeneTestRelation { GeneId = 10, TestId = 1 }, 
    
            // Тест на BRCA (2) - только гены BRCA1 и BRCA2
            new GeneTestRelation { GeneId = 1, TestId = 2 },
            new GeneTestRelation { GeneId = 11, TestId = 2 }, 
    
            // Фармакогенетический тест (3) - гены, влияющие на метаболизм лекарств
            new GeneTestRelation { GeneId = 12, TestId = 3 }, 
    
            // Пренатальный скрининг (4) - гены, связанные с хромосомными аномалиями
            new GeneTestRelation { GeneId = 13, TestId = 4 }, 
            new GeneTestRelation { GeneId = 14, TestId = 4 }, 
    
            // Полногеномное секвенирование (6) - включает множество генов
            new GeneTestRelation { GeneId = 1, TestId = 6 },
            new GeneTestRelation { GeneId = 3, TestId = 6 },
            new GeneTestRelation { GeneId = 6, TestId = 6 }, 
    
            // Тест на лактозу (7) - ген LCT
            new GeneTestRelation { GeneId = 15, TestId = 7 } 
        );
    }
}