CREATE PROCEDURE [dbo].[spCharacters_Delete]
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Characters WHERE Id = @id;
END;
