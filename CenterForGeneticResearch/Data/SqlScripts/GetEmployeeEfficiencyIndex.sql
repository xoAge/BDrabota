USE GeneticResearch
go
CREATE OR ALTER PROCEDURE GetEmployeeEfficiencyIndex
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EfficiencyIndex DECIMAL(10,4) = 0;
    DECLARE @TotalTests INT = 0, 
            @UniquePatients INT = 0, 
            @UniqueGenes INT = 0, 
            @WorkExperience INT = 0;

    -- Получаем опыт работы
    SELECT @WorkExperience = WorkExperience 
    FROM Employees 
    WHERE Id = @EmployeeId;
    
    IF @WorkExperience IS NULL
    BEGIN
        SELECT CAST(0.0 AS DECIMAL(10,4)) AS EfficiencyIndex;
        RETURN;
    END;

    -- Считаем тесты и пациентов 
    SELECT 
        @TotalTests = COUNT(gt.Id),
        @UniquePatients = COUNT(DISTINCT gs.PatientId)
    FROM Employees e
    LEFT JOIN GeneticTests gt ON e.Id = gt.EmployeeId
    LEFT JOIN GeneticSamples gs ON gt.SampleId = gs.Id
    WHERE e.Id = @EmployeeId;

    -- Считаем уникальные гены 
    SELECT @UniqueGenes = COUNT(DISTINCT gtr.GeneId)
    FROM Employees e
    LEFT JOIN GeneticTests gt ON e.Id = gt.EmployeeId
    LEFT JOIN GeneTestRelations gtr ON gt.Id = gtr.TestId
    WHERE e.Id = @EmployeeId;

    -- Формула для индекса эффективности
    SET @EfficiencyIndex = 
          (ISNULL(@TotalTests,0) * 0.5) 
        + (ISNULL(@UniquePatients,0) * 1.0) 
        + (ISNULL(@UniqueGenes,0) * 0.7) 
        + (ISNULL(@WorkExperience,0) * 0.3);

    -- Возвращаем результат
    SELECT @EfficiencyIndex AS EfficiencyIndex;
END;
GO
