SELECT [D].[Title] AS [Department],
CAST(AVG(CAST([Student].[DateOfBirth] AS FLOAT)) AS DATETIME) AS [Average Age]
FROM [dbo].[StudentSubjectTeacher] AS [SST]
INNER JOIN [dbo].[SubjectTeacher] AS [ST]
ON [ST].[SubjectTeacherId] = [SST].[SubjectTeacherId]
INNER JOIN [dbo].[Subject] AS [S]
ON [S].[SubjectId] = [ST].[SubjectId]
INNER JOIN [dbo].[Department] AS [D]
ON [S].[DepartmentId] = [D].[DepartmentId]
INNER JOIN [dbo].[Student] AS [Student]
ON [SST].[StudentId] = [Student].[StudentId]
GROUP BY [D].[Title];