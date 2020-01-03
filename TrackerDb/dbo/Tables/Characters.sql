CREATE TABLE [dbo].[Characters] (
    [Id] INT NOT NULL IDENTITY PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL UNIQUE,
    [ClassId] INT NOT NULL,
    [ProfessionId] INT NULL DEFAULT NULL,
    CONSTRAINT [FK_Characters_Classes] FOREIGN KEY (ClassId) REFERENCES dbo.Classes (Id),
    CONSTRAINT [FK_Characters_Professions] FOREIGN KEY (ProfessionId) REFERENCES dbo.Professions (Id)
);
