CREATE TABLE [dbo].[NeededItems] (
    [CharacterId] INT NOT NULL,
    [ItemId] INT NOT NULL,
    [Amount] INT NOT NULL,
    CONSTRAINT [PK_CharacterId_ItemId] PRIMARY KEY (CharacterId, ItemId),
    CONSTRAINT [FK_NeededItems_Characters] FOREIGN KEY (CharacterId) REFERENCES dbo.Characters (Id),
    CONSTRAINT [FK_NeededItems_Items] FOREIGN KEY (ItemId) REFERENCES dbo.Items (Id)
);