CREATE PROCEDURE [dbo].[spCharacters_Insert]
    @id INT,
    @userId INT,
    @name NVARCHAR(50),
    @classId INT,
    @firstProfessionId INT,
    @secondProfessionId INT,
    @hasCooking BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Characters (UserId, Name, ClassId, FirstProfessionId, SecondProfessionId, HasCooking)
    VALUES (@userId, @name, @classId, @firstProfessionId, @secondProfessionId, @hasCooking);
END;
