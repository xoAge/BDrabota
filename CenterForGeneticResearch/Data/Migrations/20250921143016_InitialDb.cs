using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CenterForGeneticResearch.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    EmployeeType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkExperience = table.Column<short>(type: "smallint", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Function = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RelatedDiseases = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneticSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SampleType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StorageLocation = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TemperatureRegime = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneticSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneticSamples_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneticTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    ConductDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnalysisMethod = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Interpretation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SampleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneticTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneticTests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_GeneticTests_GeneticSamples_SampleId",
                        column: x => x.SampleId,
                        principalTable: "GeneticSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conclusions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConclusionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    number = table.Column<int>(type: "int", nullable: true),
                    Recommendations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conclusions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conclusions_GeneticTests_TestId",
                        column: x => x.TestId,
                        principalTable: "GeneticTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneTestRelations",
                columns: table => new
                {
                    GeneId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneTestRelations", x => new { x.GeneId, x.TestId });
                    table.ForeignKey(
                        name: "FK_GeneTestRelations_Genes_GeneId",
                        column: x => x.GeneId,
                        principalTable: "Genes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneTestRelations_GeneticTests_TestId",
                        column: x => x.TestId,
                        principalTable: "GeneticTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Bio", "BirthDate", "Email", "EmployeeType", "FirstName", "HireDate", "LastName", "MiddleName", "Phone", "WorkExperience" },
                values: new object[,]
                {
                    { 1, "Специалист по молекулярной генетике, эксперт в области наследственных заболеваний", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ivan.petrov@genlab.ru", "Geneticist", "Иван", new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петров", "Сергеевич", "79161234567", (short)10 },
                    { 2, "Врач-генетик, специализация - превентивная медицина", new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "elena.smirnova@genlab.ru", "Doctor", "Елена", new DateTime(2018, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Смирнова", "Александровна", "79269876543", (short)7 },
                    { 3, "Медсестра процедурного кабинета, забор биоматериалов", new DateTime(1992, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.kozlova@genlab.ru", "Nurse", "Анна", new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Козлова", "Михайловна", "79031234578", (short)6 },
                    { 4, "Подготовка проб, проведение ПЦР-анализов", new DateTime(1993, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "dmitry.volkov@genlab.ru", "LabTechnician", "Дмитрий", new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Волков", null, "79167654321", (short)5 },
                    { 5, "Научный сотрудник, исследования в области геномной медицины", new DateTime(1988, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "olga.novikova@genlab.ru", "Researcher", "Ольга", new DateTime(2017, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новикова", "Игоревна", "79253215476", (short)8 },
                    { 6, "Управление лабораторными процессами и координация работы", new DateTime(1980, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "sergey.fedorov@genlab.ru", "Administrator", "Сергей", new DateTime(2015, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Фёдоров", "Павлович", "79087654321", (short)12 },
                    { 7, "Обслуживание лабораторного оборудования", new DateTime(1991, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "alexey.morozov@genlab.ru", "LabTechnician", "Алексей", new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Морозов", "Владимирович", "79104561237", (short)4 }
                });

            migrationBuilder.InsertData(
                table: "Genes",
                columns: new[] { "Id", "Function", "Name", "RelatedDiseases" },
                values: new object[,]
                {
                    { 1, "Подавление опухолей, репарация ДНК", "BRCA1", "Рак молочной железы, рак яичников" },
                    { 2, "Регуляция хлоридных каналов", "CFTR", "Муковисцидоз" },
                    { 3, "Кодирование бета-глобина", "HBB", "Серповидноклеточная анемия, бета-талассемия" },
                    { 4, "Фактор свертывания крови VIII", "F8", "Гемофилия A" },
                    { 5, "Кодирование белка хантингтина", "HTT", "Хорея Гентингтона" },
                    { 6, "Транспорт липидов", "APOE", "Болезнь Альцгеймера, гиперлипопротеинемия" },
                    { 7, "Фермент глюкозо-6-фосфат-дегидрогеназа", "G6PD", "Дефицит G6PD, гемолитическая анемия" },
                    { 8, "Протромбин", "F2", "Тромбофилия" },
                    { 9, "Фактор V", "F5", "Лейденская тромбофилия" },
                    { 10, "Метилентетрагидрофолатредуктаза", "MTHFR", "Гипергомоцистеинемия" },
                    { 11, "Подавление опухолей", "BRCA2", "Рак молочной железы" },
                    { 12, "Метаболизм лекарств", "CYP2C19", "Нарушения метаболизма лекарств" },
                    { 13, "Трисомия 21", "T21", "Синдром Дауна" },
                    { 14, "Трисомия 18", "T18", "Синдром Эдвардса" },
                    { 15, "Лактаза", "LCT", "Непереносимость лактозы" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "BirthDate", "FirstName", "Gender", "LastName", "MiddleName", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иван", "Male", "Петров", "Сергеевич", "79123456789" },
                    { 2, new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Елена", "Female", "Смирнова", "Александровна", "79234567890" },
                    { 3, new DateTime(1978, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Алексей", "Male", "Кузнецов", null, "79345678901" },
                    { 4, new DateTime(1995, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ольга", "Female", "Васильева", "Игоревна", "79456789012" },
                    { 5, new DateTime(1982, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дмитрий", "Male", "Новиков", "Викторович", "79567890123" },
                    { 6, new DateTime(2000, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Анна", "Female", "Морозова", "Дмитриевна", "79678901234" },
                    { 7, new DateTime(1992, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Максим", "Other", "Фёдоров", null, "79789012345" }
                });

            migrationBuilder.InsertData(
                table: "GeneticSamples",
                columns: new[] { "Id", "CollectionDate", "PatientId", "SampleType", "Status", "StorageLocation", "TemperatureRegime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Blood", "ReadyForAnalysis", "Холодильник A1", "-20°C" },
                    { 2, new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Saliva", "InAnalysis", "Морозильник B2", "-80°C" },
                    { 3, new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Biopsy", "AnalysisComplete", "Холодильник C3", "-196°C" },
                    { 4, new DateTime(2023, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Urine", "Archived", "Морозильник D4", "-20°C" },
                    { 5, new DateTime(2023, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Hair", "Registered", "Шкаф E5", "Комнатная" },
                    { 6, new DateTime(2023, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Other", "InProcessing", "Холодильник F6", "4°C" },
                    { 7, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Blood", "Destroyed", "Морозильник G7", "-80°C" }
                });

            migrationBuilder.InsertData(
                table: "GeneticTests",
                columns: new[] { "Id", "AnalysisMethod", "ConductDate", "EmployeeId", "Interpretation", "Result", "SampleId", "TestName" },
                values: new object[,]
                {
                    { 1, "NGS-секвенирование", new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Повышенный риск тромбофилии", "Обнаружены полиморфизмы в генах F2, F5", 1, "Генетический профиль сердечно-сосудистых заболеваний" },
                    { 2, "ПЦР в реальном времени", new DateTime(2023, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Риск наследственного рака молочной железы в пределах популяционной нормы", "Мутация BRCA1 не обнаружена", 2, "Анализ мутаций BRCA1/BRCA2" },
                    { 3, "Sanger sequencing", new DateTime(2023, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Сниженный метаболизм некоторых антиагрегантов", "Генотип CYP2C19*1/*2", 3, "Фармакогенетический тест CYP2C19" },
                    { 4, "qPCR", new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Результаты в пределах нормы для срока беременности", "Низкий риск трисомий 21, 18, 13", 4, "Пренатальный скрининг на трисомии" },
                    { 5, "STR-анализ 16 локусов", new DateTime(2023, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Биологическое отцовство подтверждено", "99.99% вероятность отцовства", 5, "Тест на отцовство" },
                    { 6, "WGS", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Рекомендована консультация генетика", "Выявлено 4 патогенных варианта в генах", 6, "Полногеномное секвенирование" },
                    { 7, "RFLP-анализ", new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Первичная лактазная недостаточность", "Генотип CC(-13910)", 7, "Тест на непереносимость лактозы" }
                });

            migrationBuilder.InsertData(
                table: "Conclusions",
                columns: new[] { "Id", "ConclusionDate", "Recommendations", "Summary", "TestId", "number" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рекомендована консультация гематолога, контроль коагулограммы 2 раза в год, избегать обезвоживания", "Выявлен высокий риск тромбофилии из-за полиморфизмов в генах F2 и F5", 1, null },
                    { 2, new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Стандартный скрининг рака молочной железы согласно возрасту", "Мутации в генах BRCA1/BRCA2 не обнаружены", 2, null },
                    { 3, new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Коррекция дозы клопидогрела, рассмотреть альтернативные антиагреганты", "Обнаружен генотип CYP2C19*1/*2, указывающий на промежуточный метаболизм", 3, null },
                    { 4, new DateTime(2023, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Плановое наблюдение беременности, УЗИ в 20-22 недели", "Пренатальный скрининг показал низкий риск хромосомных аномалий", 4, null },
                    { 5, new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Выдать юридическое заключение для судебных целей", "Отцовство подтверждено с вероятностью 99.99%", 5, null },
                    { 6, new DateTime(2023, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Консультация клинического генетика, обследование родственников", "Выявлены патогенные варианты в генах, связанных с наследственными заболеваниями", 6, null },
                    { 7, new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Безлактозная диета, препараты лактазы при необходимости", "Подтверждена первичная лактазная недостаточность", 7, null }
                });

            migrationBuilder.InsertData(
                table: "GeneTestRelations",
                columns: new[] { "GeneId", "TestId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 6 },
                    { 3, 6 },
                    { 6, 6 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 2 },
                    { 12, 3 },
                    { 13, 4 },
                    { 14, 4 },
                    { 15, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conclusions_TestId",
                table: "Conclusions",
                column: "TestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Phone",
                table: "Employees",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneTestRelations_TestId",
                table: "GeneTestRelations",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneticSamples_PatientId",
                table: "GeneticSamples",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneticTests_EmployeeId",
                table: "GeneticTests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneticTests_SampleId",
                table: "GeneticTests",
                column: "SampleId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Phone",
                table: "Patients",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conclusions");

            migrationBuilder.DropTable(
                name: "GeneTestRelations");

            migrationBuilder.DropTable(
                name: "Genes");

            migrationBuilder.DropTable(
                name: "GeneticTests");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "GeneticSamples");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
