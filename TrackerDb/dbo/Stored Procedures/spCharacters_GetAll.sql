CREATE PROCEDURE [dbo].[spCharacters_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Name,
        class.Name AS Class,
        p1.Name AS FirstProfession,
        p2.Name AS SecondProfession,
        c.HasCooking
    FROM dbo.Characters AS c
        INNER JOIN dbo.Classes AS class ON c.ClassId = class.Id
        LEFT JOIN dbo.Professions AS p1 ON c.FirstProfessionId = p1.Id
        LEFT JOIN dbo.Professions AS p2 ON c.SecondProfessionId = p2.Id;
END;
