CREATE PROCEDURE [dbo].[spItems_GetBySlot]
    @name NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT item.Name
    FROM dbo.Items AS item
        INNER JOIN dbo.EquipmentSlots AS slot ON slot.Id = item.EquipmentSlotId AND slot.Name = @name;
END;
