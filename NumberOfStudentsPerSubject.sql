SELECT [S].[Title] AS [Subject], COUNT([Student].[StudentId]) AS [Number Of Students]
FROM [dbo].[StudentSubjectTeacher] AS [SST]
INNER JOIN [dbo].[SubjectTeacher] AS [ST]
ON [ST].[SubjectTeacherId] = [SST].[SubjectTeacherId]
INNER JOIN [dbo].[Subject] AS [S]
ON [S].[SubjectId] = [ST].[SubjectId]
INNER JOIN [dbo].[Student] AS [Student]
ON [SST].[StudentId] = [Student].[StudentId]
GROUP BY [S].[Title];