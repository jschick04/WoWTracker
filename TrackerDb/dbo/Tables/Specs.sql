CREATE TABLE [dbo].[Specs] (
    [Id] INT NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [ClassId] INT NOT NULL,
    [HasOffhand] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Specs_Classes] FOREIGN KEY (ClassId) REFERENCES dbo.Classes (Id)
);
