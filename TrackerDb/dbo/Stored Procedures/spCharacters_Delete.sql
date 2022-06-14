CREATE PROCEDURE [dbo].[spCharacters_Delete]
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE FROM dbo.NeededItems WHERE CharacterId = @id;

        DELETE FROM dbo.Characters WHERE Id = @id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF (@@TRANCOUNT > 0)
            ROLLBACK TRANSACTION;

        RAISERROR('Failed to delete character', 11, 1);
    END CATCH;
END;
