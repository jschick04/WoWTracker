CREATE TABLE [dbo].[Characters] (
    [Id] INT NOT NULL IDENTITY PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL UNIQUE,
    [ClassId] INT NOT NULL,
    [FirstProfessionId] INT NULL DEFAULT NULL,
    [SecondProfessionId] INT NULL DEFAULT NULL,
    [HasCooking] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Characters_Classes] FOREIGN KEY (ClassId) REFERENCES dbo.Classes (Id),
    CONSTRAINT [FK_Characters_FirstProfessions] FOREIGN KEY (FirstProfessionId) REFERENCES dbo.Professions (Id),
    CONSTRAINT [FK_Characters_SecondProfessions] FOREIGN KEY (SecondProfessionId) REFERENCES dbo.Professions (Id)
);
