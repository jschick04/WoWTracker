CREATE PROCEDURE [dbo].[spCharacters_GetAll]
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        UserId,
        Name,
        ClassId,
        FirstProfessionId,
        SecondProfessionId,
        HasCooking
    FROM dbo.Characters
    WHERE UserId = @userId;
END;
