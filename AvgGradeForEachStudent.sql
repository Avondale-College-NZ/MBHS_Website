SELECT [St].[FirstName] AS [First Name], [St].[LastName] AS [Last Name], AVG([G].[Mark]) AS [Average Grade], COUNT([G].[ExamId]) AS [Number of Exams] 
FROM [dbo].[Student] AS [St]
INNER JOIN [dbo].[Grade] AS [G]
ON [St].[StudentId] = [G].[StudentId]
GROUP BY [St].[FirstName], [St].[LastName]
ORDER BY AVG([G].[Mark]) DESC