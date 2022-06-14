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

    IF EXISTS (SELECT Name FROM dbo.Characters WHERE Name = @name)
    BEGIN
        RAISERROR('Character already exists', 11, 1);

        RETURN -6;
    END;

    INSERT INTO dbo.Characters (UserId, Name, ClassId, FirstProfessionId, SecondProfessionId, HasCooking)
    VALUES (@userId, @name, @classId, @firstProfessionId, @secondProfessionId, @hasCooking);
END;
