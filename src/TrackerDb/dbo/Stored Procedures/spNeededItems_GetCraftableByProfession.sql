CREATE PROCEDURE [dbo].[spNeededItems_GetCraftableByProfession]
    @userId INT,
    @professionId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT c.Id AS CharacterId,
           c.Name As CharacterName,
           i.ProfessionId,
           i.Name,
           ni.Amount
    FROM dbo.Characters AS c
        INNER JOIN dbo.NeededItems AS ni ON ni.CharacterId = c.Id AND c.UserId = @userId
        INNER JOIN dbo.Items AS i ON i.Id = ni.ItemId AND i.ProfessionId = @professionId;
END;
