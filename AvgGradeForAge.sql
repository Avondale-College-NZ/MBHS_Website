CREATE PROCEDURE AgeAvgGrade @Age int
AS
SELECT AVG([G].[Mark])
FROM [dbo].[Grade] as [G]
INNER JOIN [dbo].[Student] as [S]
ON [S].[StudentId] = [G].[StudentId]
WHERE [S].[DateOfBirth] <= DATEADD(Year, -@Age, GETDATE()) AND [S].[DateOfBirth] >= DATEADD(Year, (-@Age-1), GETDATE())

