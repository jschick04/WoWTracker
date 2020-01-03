/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT * FROM dbo.Classes)
BEGIN
    INSERT INTO dbo.Classes (Id, Name)
    VALUES
    (1, N'Warrior'),
    (2, N'Paladin'),
    (3, N'Hunter'),
    (4, N'Rogue'),
    (5, N'Priest'),
    (6, N'Shaman'),
    (7, N'Mage'),
    (8, N'Warlock'),
    (9, N'Monk'),
    (10, N'Druid'),
    (11, N'Demon Hunter'),
    (12, N'Death Knight');
END;

IF NOT EXISTS (SELECT * FROM dbo.Characters)
BEGIN
    INSERT INTO dbo.Characters (Name, ClassId)
    VALUES
    (N'Moutagg', 9),
    (N'Mout', 1);
END;

IF NOT EXISTS (SELECT * FROM dbo.Specs)
BEGIN
    INSERT INTO dbo.Specs (Id, Name, ClassId)
    VALUES
    (1, N'Arms', 1),
    (2, N'Fury', 1),
    (3, N'Protection', 1),
    (4, N'Holy', 2),
    (5, N'Protection', 2),
    (6, N'Retribution', 2),
    (7, N'Beast Mastery', 3),
    (8, N'Marksmanship', 3),
    (9, N'Survival', 3),
    (10, N'Assassination', 4),
    (11, N'Outlaw', 4),
    (12, N'Subtlety', 4),
    (13, N'Discipline', 5),
    (14, N'Holy', 5),
    (15, N'Shadow', 5),
    (16, N'Elemental', 6),
    (17, N'Enhancement', 6),
    (18, N'Restoration', 6),
    (19, N'Arcane', 7),
    (20, N'Fire', 7),
    (21, N'Frost', 7),
    (22, N'Affliction', 8),
    (23, N'Demonology', 8),
    (24, N'Destruction', 8),
    (25, N'Holy', 9),
    (26, N'Protection', 9),
    (27, N'Retribution', 9),
    (28, N'Balance', 10),
    (29, N'Feral', 10),
    (30, N'Guardian', 10),
    (31, N'Restoration', 10),
    (32, N'Havoc', 11),
    (33, N'Vengeance', 11),
    (34, N'Blood', 12),
    (35, N'Frost', 12),
    (36, N'Unholy', 12);
END;

IF NOT EXISTS (SELECT * FROM dbo.Professions)
BEGIN
    INSERT INTO dbo.Professions (Id, Name)
    VALUES
    (1, N'Alchemy'),
    (2, N'Blacksmithing'),
    (3, N'Enchanting'),
    (4, N'Engineering'),
    (5, N'Inscription'),
    (6, N'Jewelcrafting'),
    (7, N'Leatherworking'),
    (8, N'Tailoring'),
    (9, N'Cooking');
END;

IF NOT EXISTS (SELECT * FROM dbo.EquipmentSlots)
BEGIN
    INSERT INTO dbo.EquipmentSlots (Id, Name)
    VALUES
    (1, N'Main Hand'),
    (2, N'Off Hand'),
    (3, N'Gem'),
    (4, N'Ring'),
    (5, N'Gloves'),
    (6, N'Misc');
END