CREATE PROCEDURE enterGrade @studentId INT ,@examId INT ,@grade INT
AS
INSERT INTO [dbo].[Grade] ([StudentId], [ExamId], [Mark])
VALUES (@studentId, @examId, @grade)