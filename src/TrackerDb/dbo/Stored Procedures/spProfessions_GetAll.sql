CREATE PROCEDURE [dbo].[spProfessions_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Name FROM dbo.Professions;
END;
