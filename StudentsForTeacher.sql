CREATE PROCEDURE StudentsForTeacher @FirstName NVARCHAR(255), @LastName NVARCHAR(255)
AS
SELECT 
[Teach].[FirstName] AS [First Name],[Teach].[LastName] AS [Last Name], COUNT([SST].[StudentId]) AS [Number of students]

FROM [dbo].[AspNetUsers] AS [Teach]
INNER JOIN [dbo].[SubjectTeacher] AS [ST]
ON [Teach].[Id] = [ST].[TeacherId]
INNER JOIN [dbo].[StudentSubjectTeacher] AS [SST]
ON [ST].[SubjectTeacherId] = [SST].[SubjectTeacherId]
WHERE [Teach].[FirstName] = @FirstName AND [Teach].[LastName] = @LastName
GROUP BY [Teach].[FirstName], [Teach].[LastName]
