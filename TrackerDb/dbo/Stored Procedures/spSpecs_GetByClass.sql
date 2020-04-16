CREATE PROCEDURE [dbo].[spSpecs_GetByClass]
    @name NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT spec.Name
    FROM dbo.Specs AS spec
        INNER JOIN dbo.Classes AS c ON c.Id = spec.ClassId AND c.Name = @name;
END;
