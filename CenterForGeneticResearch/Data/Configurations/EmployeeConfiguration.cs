using CenterForGeneticResearch.Models.Entities;
using CenterForGeneticResearch.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterForGeneticResearch.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
            
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(25);
                
        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(25);
                
        builder.Property(e => e.MiddleName)
            .HasMaxLength(25);
                
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(50);
                
        builder.Property(e => e.Phone)
            .HasMaxLength(11);
        
        builder.Property(a => a.EmployeeType)
            .IsRequired()
            .HasConversion<string>() 
            .HasMaxLength(50);
                
        builder.Property(e => e.Bio)
            .HasMaxLength(500);
        
        builder.HasIndex(e => e.Email)
            .IsUnique();
            
        builder.HasIndex(e => e.Phone)
            .IsUnique();
        
        builder.HasData(
            new Employee
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Петров",
                MiddleName = "Сергеевич",
                Email = "ivan.petrov@genlab.ru",
                Phone = "79161234567",
                EmployeeType = EmployeeType.Geneticist,
                Bio = "Специалист по молекулярной генетике, эксперт в области наследственных заболеваний",
                BirthDate = new DateTime(1985, 5, 15),
                WorkExperience = 10,
                HireDate = new DateTime(2015, 6, 1)
            },
            new Employee
            {
                Id = 2,
                FirstName = "Елена",
                LastName = "Смирнова",
                MiddleName = "Александровна",
                Email = "elena.smirnova@genlab.ru",
                Phone = "79269876543",
                EmployeeType = EmployeeType.Doctor,
                Bio = "Врач-генетик, специализация - превентивная медицина",
                BirthDate = new DateTime(1990, 8, 22),
                WorkExperience = 7,
                HireDate = new DateTime(2018, 3, 10)
            },
            new Employee
            {
                Id = 3,
                FirstName = "Анна",
                LastName = "Козлова",
                MiddleName = "Михайловна",
                Email = "anna.kozlova@genlab.ru",
                Phone = "79031234578",
                EmployeeType = EmployeeType.Nurse,
                Bio = "Медсестра процедурного кабинета, забор биоматериалов",
                BirthDate = new DateTime(1992, 11, 30),
                WorkExperience = 6,
                HireDate = new DateTime(2019, 1, 15)
            },
            new Employee
            {
                Id = 4,
                FirstName = "Дмитрий",
                LastName = "Волков",
                MiddleName = null,
                Email = "dmitry.volkov@genlab.ru",
                Phone = "79167654321",
                EmployeeType = EmployeeType.LabTechnician,
                Bio = "Подготовка проб, проведение ПЦР-анализов",
                BirthDate = new DateTime(1993, 3, 17),
                WorkExperience = 5,
                HireDate = new DateTime(2020, 5, 20)
            },
            new Employee
            {
                Id = 5,
                FirstName = "Ольга",
                LastName = "Новикова",
                MiddleName = "Игоревна",
                Email = "olga.novikova@genlab.ru",
                Phone = "79253215476",
                EmployeeType = EmployeeType.Researcher,
                Bio = "Научный сотрудник, исследования в области геномной медицины",
                BirthDate = new DateTime(1988, 7, 8),
                WorkExperience = 8,
                HireDate = new DateTime(2017, 9, 1)
            },
            new Employee
            {
                Id = 6,
                FirstName = "Сергей",
                LastName = "Фёдоров",
                MiddleName = "Павлович",
                Email = "sergey.fedorov@genlab.ru",
                Phone = "79087654321",
                EmployeeType = EmployeeType.Administrator,
                Bio = "Управление лабораторными процессами и координация работы",
                BirthDate = new DateTime(1980, 9, 25),
                WorkExperience = 12,
                HireDate = new DateTime(2015, 2, 10)
            },
            new Employee
            {
                Id = 7,
                FirstName = "Алексей",
                LastName = "Морозов",
                MiddleName = "Владимирович",
                Email = "alexey.morozov@genlab.ru",
                Phone = "79104561237",
                EmployeeType = EmployeeType.LabTechnician,
                Bio = "Обслуживание лабораторного оборудования",
                BirthDate = new DateTime(1991, 4, 12),
                WorkExperience = 4,
                HireDate = new DateTime(2021, 7, 1)
            }
        );
    }
}