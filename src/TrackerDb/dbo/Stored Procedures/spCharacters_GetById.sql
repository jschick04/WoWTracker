CREATE PROCEDURE [dbo].[spCharacters_GetById]
    @id INT,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id,
           UserId,
           Name,
           ClassId,
           FirstProfessionId,
           SecondProfessionId,
           HasCooking
    FROM dbo.Characters
    WHERE Id = @id AND UserId = @userId;
END;
