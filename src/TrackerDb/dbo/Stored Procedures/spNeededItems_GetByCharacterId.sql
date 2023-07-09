CREATE PROCEDURE [dbo].[spNeededItems_GetByCharacterId]
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT i.ProfessionId,
           ni.Amount,
           i.Name
    FROM dbo.Characters AS c
        INNER JOIN dbo.NeededItems AS ni ON ni.CharacterId = c.Id AND c.Id = @id
        INNER JOIN dbo.Items AS i ON i.Id = ni.ItemId;
END;