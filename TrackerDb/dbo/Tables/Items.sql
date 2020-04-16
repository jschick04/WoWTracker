CREATE TABLE [dbo].[Items] (
    [Id] INT NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [ProfessionId] INT NOT NULL,
    [EquipmentSlotId] INT NOT NULL,
    CONSTRAINT [FK_Items_Professions] FOREIGN KEY (ProfessionId) REFERENCES dbo.Professions (Id),
    CONSTRAINT [FK_Items_EquipmentSlots] FOREIGN KEY (EquipmentSlotId) REFERENCES dbo.EquipmentSlots (Id)
);
