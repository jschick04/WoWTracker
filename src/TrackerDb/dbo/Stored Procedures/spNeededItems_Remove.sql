CREATE PROCEDURE [dbo].[spNeededItems_Remove]
    @characterId INT,
    @characterName NVARCHAR(50),
    @professionId INT,
    @name NVARCHAR(50),
    @amount INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @itemId INT;
    DECLARE @oldAmount INT;

    SELECT @itemId = Id FROM dbo.Items WHERE Name = @name AND ProfessionId = @professionId;

    BEGIN TRY
        SELECT @oldAmount = Amount FROM dbo.NeededItems WHERE CharacterId = @characterId AND ItemId = @itemId;

        IF (@oldAmount - @amount <= 0)
            BEGIN
                DELETE FROM dbo.NeededItems WHERE CharacterId = @characterId AND ItemId = @itemId;
            END;
        ELSE
            BEGIN
                UPDATE dbo.NeededItems SET Amount = Amount - @amount WHERE CharacterId = @characterId AND ItemId = @itemId;
            END;
    END TRY
    BEGIN CATCH
        RAISERROR('Failed to remove item', 11, 1);
    END CATCH;
END;
