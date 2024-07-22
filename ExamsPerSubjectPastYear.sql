SELECT [S].[Title] AS [Subject], COUNT([E].[ExamId]) AS [Number of Exams]
FROM [dbo].[Subject] AS [S]
INNER JOIN [dbo].[Exam] AS [E]
ON [E].[SubjectId] = [S].[SubjectId]
WHERE [E].[Date] >= DATEADD(YEAR, -1, GETDATE()) AND [E].[Date] <= GETDATE()
GROUP BY [S].[Title];