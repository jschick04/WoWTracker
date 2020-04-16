CREATE PROCEDURE [dbo].[spItems_GetByProfession]
    @name NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT items.Name
    FROM dbo.Items AS items
        INNER JOIN dbo.Professions AS p ON p.Id = items.ProfessionId AND p.Name = @name;
END;
