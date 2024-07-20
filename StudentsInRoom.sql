CREATE PROCEDURE StudentsInRoom @Room NVARCHAR(255) 
AS
SELECT [S].[FirstName] AS [First Name], [S].[LastName] AS [Last Name]
FROM [dbo].[Student] AS [S]
INNER JOIN [dbo].[StudentSubjectTeacher] AS [SST]
ON [S].[StudentId] = [SST].[StudentId]
INNER JOIN [dbo].[SubjectTeacher] AS [ST]
ON [ST].[SubjectTeacherId] = [SST].[SubjectTeacherId]
WHERE [ST].[Room] = @Room;