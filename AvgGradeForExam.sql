SELECT [S].[Title] AS [Subject], [E].[Date] AS [Date], AVG([G].[Mark]) AS [Average Grade]
FROM [dbo].[Subject] AS [S]
INNER JOIN [dbo].[Exam] AS [E]
ON [E].[SubjectId] = [S].[SubjectId]
INNER JOIN [dbo].[Grade] AS [G]
ON [G].[ExamId] = [E].[ExamId]
GROUP BY [S].[Title], [E].[Date];