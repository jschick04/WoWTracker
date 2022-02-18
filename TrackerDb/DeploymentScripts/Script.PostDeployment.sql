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
    VALUES (1, N'Warrior'),
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

IF NOT EXISTS (SELECT * FROM dbo.Specs)
BEGIN
    INSERT INTO dbo.Specs (Id, Name, ClassId, HasOffhand)
    VALUES (1, N'Arms', 1, 0),
           (2, N'Fury', 1, 1),
           (3, N'Protection', 1, 0),
           (4, N'Holy', 2, 0),
           (5, N'Protection', 2, 0),
           (6, N'Retribution', 2, 0),
           (7, N'Beast Mastery', 3, 0),
           (8, N'Marksmanship', 3, 0),
           (9, N'Survival', 3, 0),
           (10, N'Assassination', 4, 1),
           (11, N'Outlaw', 4, 1),
           (12, N'Subtlety', 4, 1),
           (13, N'Discipline', 5, 0),
           (14, N'Holy', 5, 0),
           (15, N'Shadow', 5, 0),
           (16, N'Elemental', 6, 0),
           (17, N'Enhancement', 6, 1),
           (18, N'Restoration', 6, 0),
           (19, N'Arcane', 7, 0),
           (20, N'Fire', 7, 0),
           (21, N'Frost', 7, 0),
           (22, N'Affliction', 8, 0),
           (23, N'Demonology', 8, 0),
           (24, N'Destruction', 8, 0),
           (25, N'Brewmaster', 9, 0),
           (26, N'Restoration', 9, 0),
           (27, N'Wind Walker', 9, 1),
           (28, N'Balance', 10, 0),
           (29, N'Feral', 10, 0),
           (30, N'Guardian', 10, 0),
           (31, N'Restoration', 10, 0),
           (32, N'Havoc', 11, 1),
           (33, N'Vengeance', 11, 1),
           (34, N'Blood', 12, 0),
           (35, N'Frost', 12, 1),
           (36, N'Unholy', 12, 0);
END;

IF NOT EXISTS (SELECT * FROM dbo.Professions)
BEGIN
    INSERT INTO dbo.Professions (Id, Name)
    VALUES (1, N'Alchemy'),
           (2, N'Blacksmithing'),
           (3, N'Enchanting'),
           (4, N'Engineering'),
           (5, N'Inscription'),
           (6, N'Jewelcrafting'),
           (7, N'Leatherworking'),
           (8, N'Tailoring');
END;

IF NOT EXISTS (SELECT * FROM dbo.EquipmentSlots)
BEGIN
    INSERT INTO dbo.EquipmentSlots (Id, Name)
    VALUES (1, N'Weapon'),
           (2, N'Gem'),
           (3, N'Ring'),
           (4, N'Gloves'),
           (5, N'Misc');
END;

IF NOT EXISTS (SELECT * FROM dbo.Items)
BEGIN
    INSERT INTO dbo.Items (Id, Name, ProfessionId, EquipmentSlotId)
    VALUES
    /* Alchemy */
    (101, N'Abyssal Healing Potion', 1, 5),
    (102, N'Greater Flask of Endless Fathoms', 1, 5),
    (103, N'Greater Flask of the Currents', 1, 5),
    (104, N'Greater Flask of the Undertow', 1, 5),
    (105, N'Greater Flask of the Vast Horizon', 1, 5),
    /* Glove Enchantments */
    (301, N'Zandalari Crafting', 3, 4),
    (302, N'Zandalari Herbalism', 3, 4),
    (303, N'Zandalari Mining', 3, 4),
    (304, N'Zandalari Skinning', 3, 4),
    (305, N'Zandalari Surveying', 3, 4),
    /* Ring Enchantments */
    (311, N'Accord of Critical Strike', 3, 3),
    (312, N'Accord of Haste', 3, 3),
    (313, N'Accord of Mastery', 3, 3),
    (314, N'Accord of Versatility', 3, 3),
    (315, N'Pact of Critical Strike', 3, 3),
    (316, N'Pact of Haste', 3, 3),
    (317, N'Pact of Mastery', 3, 3),
    (318, N'Pact of Versatility', 3, 3),
    /* Weapon Enchantments */
    (321, N'Force Multiplier', 3, 1),
    (322, N'Machinist''s Brilliance', 3, 1),
    (323, N'Naga Hide', 3, 1),
    (324, N'Oceanic Restoration', 3, 1),
    (325, N'Deadly Navigation', 3, 1),
    (326, N'Masterful Navigation', 3, 1),
    (327, N'Quick Navigation', 3, 1),
    (328, N'Stalwart Navigation', 3, 1),
    (329, N'Versatile Navigation', 3, 1),
    /* Gems */
    (601, N'Deadly Lava Lazuli', 6, 2),
    (602, N'Leviathan''s Eye of Agility', 6, 2),
    (603, N'Leviathan''s Eye of Intellect', 6, 2),
    (604, N'Leviathan''s Eye of Strength', 6, 2),
    (605, N'Masterful Sea Currant', 6, 2),
    (606, N'Quick Sand Spinel', 6, 2),
    (607, N'Straddling Sage Agate', 6, 2),
    (608, N'Versatile Dark Opal', 6, 2);
END;

IF NOT EXISTS (SELECT * FROM dbo.Characters)
BEGIN
    INSERT INTO dbo.Characters (UserId, Name, ClassId, FirstProfessionId, SecondProfessionId, HasCooking)
    VALUES (1, N'Moutagg', 9, 3, 1, 1),
           (1, N'Mout', 1, 6, 2, 0),
           (2, N'Dasaji', 10, 7, NULL, 0);
END;