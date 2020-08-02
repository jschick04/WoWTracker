CREATE PROCEDURE [dbo].[spCrafting_GetByProfession]
    @profession NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        items.Name AS Item,
        crafting.Quantity,
        c.Name AS Character
    FROM dbo.Crafting AS crafting
        INNER JOIN dbo.Items AS items ON crafting.ItemId = items.Id
        INNER JOIN dbo.Professions AS p ON items.ProfessionId = p.Id
        INNER JOIN dbo.Characters AS c ON crafting.CharacterId = c.Id
    WHERE p.Name = @profession;
END;
