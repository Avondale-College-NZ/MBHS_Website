SELECT [S].[FirstName] AS [First Name], 
[S].[LastName] AS [Last Name],
COUNT([SST].[StudentSubjectTeacherId])
FROM [dbo].[StudentSubjectTeacher] AS [SST]
INNER JOIN [dbo].[Student] AS [S]
ON [S].[StudentId] = [SST].[StudentId]
GROUP BY [S].[FirstName], [S].[LastName]