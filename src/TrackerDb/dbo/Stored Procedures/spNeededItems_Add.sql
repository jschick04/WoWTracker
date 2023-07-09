﻿CREATE PROCEDURE [dbo].[spNeededItems_Add]
    @id INT,
    @characterName NVARCHAR(50),
    @professionId INT,
    @name NVARCHAR(50),
    @amount INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @itemId INT;

    SELECT @itemId = Id FROM dbo.Items WHERE Name = @name AND ProfessionId = @professionId;

    BEGIN TRY
        IF EXISTS (SELECT * FROM dbo.NeededItems WHERE CharacterId = @id AND ItemId = @itemId)
            BEGIN
                UPDATE dbo.NeededItems SET Amount = Amount + @amount WHERE CharacterId = @id AND ItemId = @itemId;
            END;
        ELSE
            BEGIN
                INSERT INTO dbo.NeededItems (CharacterId, ItemId, Amount) VALUES (@id, @itemId, @amount);
            END;
    END TRY
    BEGIN CATCH
        RAISERROR('Failed to add item', 11, 1);
    END CATCH;
END;