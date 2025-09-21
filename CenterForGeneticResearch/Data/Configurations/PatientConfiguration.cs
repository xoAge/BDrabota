using CenterForGeneticResearch.Models.Entities;
using CenterForGeneticResearch.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterForGeneticResearch.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(p => p.MiddleName)
            .HasMaxLength(25);

        builder.Property(a => a.Gender)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(p => p.Phone)
            .HasMaxLength(11);
        
        builder.HasIndex(p => p.Phone)
            .IsUnique();

        builder.HasData(
            new Patient
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Петров",
                MiddleName = "Сергеевич",
                BirthDate = new DateTime(1985, 5, 15),
                Gender = Gender.Male,
                Phone = "79123456789"
            },
            new Patient
            {
                Id = 2,
                FirstName = "Елена",
                LastName = "Смирнова",
                MiddleName = "Александровна",
                BirthDate = new DateTime(1990, 8, 22),
                Gender = Gender.Female,
                Phone = "79234567890"
            },
            new Patient
            {
                Id = 3,
                FirstName = "Алексей",
                LastName = "Кузнецов",
                MiddleName = null,
                BirthDate = new DateTime(1978, 3, 10),
                Gender = Gender.Male,
                Phone = "79345678901"
            },
            new Patient
            {
                Id = 4,
                FirstName = "Ольга",
                LastName = "Васильева",
                MiddleName = "Игоревна",
                BirthDate = new DateTime(1995, 11, 30),
                Gender = Gender.Female,
                Phone = "79456789012"
            },
            new Patient
            {
                Id = 5,
                FirstName = "Дмитрий",
                LastName = "Новиков",
                MiddleName = "Викторович",
                BirthDate = new DateTime(1982, 7, 5),
                Gender = Gender.Male,
                Phone = "79567890123"
            },
            new Patient
            {
                Id = 6,
                FirstName = "Анна",
                LastName = "Морозова",
                MiddleName = "Дмитриевна",
                BirthDate = new DateTime(2000, 1, 18),
                Gender = Gender.Female,
                Phone = "79678901234"
            },
            new Patient
            {
                Id = 7,
                FirstName = "Максим",
                LastName = "Фёдоров",
                MiddleName = null,
                BirthDate = new DateTime(1992, 9, 25),
                Gender = Gender.Other,
                Phone = "79789012345"
            }
        );
    }
}