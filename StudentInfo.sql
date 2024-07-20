CREATE PROCEDURE StudentInfo @FirstName NVARCHAR(255), @LastName NVARCHAR(255)
AS
Select * FROM [dbo].[Student] as [S]
WHERE [S].[FirstName] = @FirstName AND [S].[LastName] = @LastName;