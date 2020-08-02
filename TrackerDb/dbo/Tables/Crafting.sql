CREATE TABLE [dbo].[Crafting] (
    [Id] INT NOT NULL IDENTITY PRIMARY KEY,
    [ItemId] INT NOT NULL,
    [Quantity] INT NOT NULL DEFAULT 1,
    [CharacterId] INT NOT NULL,
    CONSTRAINT [FK_Crafting_Items] FOREIGN KEY (ItemId) REFERENCES dbo.Items (Id),
    CONSTRAINT [FK_Crafting_Characters] FOREIGN KEY (CharacterId) REFERENCES dbo.Characters (Id)
);
