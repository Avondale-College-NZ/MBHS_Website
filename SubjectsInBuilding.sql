CREATE PROCEDURE SubjectsInBuilding @Building NVARCHAR(255) 
AS
SELECT [S].[Title] AS [Subject]
FROM [dbo].[Subject] AS [S]
INNER JOIN [dbo].[Department] AS [D]
ON [S].[DepartmentId] = [D].[DepartmentId]
WHERE [D].[Building] = @Building;