SELECT 
[S].[Title] as [Subject],
[E].[Date] AS [Date]
FROM [dbo].[Exam] as [E]
INNER JOIN [dbo].[Subject] as [S]
ON [E].[SubjectId] = [S].[SubjectId]
WHERE [E].[Date] <= DateAdd(month, +1, GetDate()) AND [E].[Date] >= GetDate()