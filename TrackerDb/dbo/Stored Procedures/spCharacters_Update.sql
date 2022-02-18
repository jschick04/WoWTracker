CREATE PROCEDURE [dbo].[spCharacters_Update]
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

    UPDATE dbo.Characters
    SET Name = @name,
        ClassId = @classId,
        FirstProfessionId = @firstProfessionId,
        SecondProfessionId = @secondProfessionId,
        HasCooking = @hasCooking
    WHERE Id = @id;
END;
