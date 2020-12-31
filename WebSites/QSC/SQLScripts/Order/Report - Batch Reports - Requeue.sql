USE [QSPCanadaOrderManagement]

DECLARE @OrderID INT,
		@ShipmentGroupID INT

SET	@OrderID = 11966707
SET @ShipmentGroupID = null --1: Gift/Prizes, 2: Cookie Dough, 3: Field Supplies, 4: Popcorn

DECLARE	ShipmentGroup CURSOR FOR
SELECT	DISTINCT pl.ShipmentGroupID
FROM	BatchDistributionCenter bdc
JOIN	Batch b ON b.ID = bdc.BatchID AND b.Date = bdc.BatchDate
JOIN	QSPCanadaCommon..QSPProductLine pl ON pl.ID = bdc.QSPProductLine
WHERE	b.OrderID = @OrderID
AND		(pl.ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL)
			
OPEN ShipmentGroup
FETCH NEXT FROM ShipmentGroup INTO @ShipmentGroupID
WHILE @@FETCH_STATUS = 0
begin
	exec pr_cleanprintqueue @OrderID, @ShipmentGroupID
	exec pr_Ins_Report_Parameters_V2 @OrderId, -1, @ShipmentGroupID

	FETCH NEXT FROM ShipmentGroup INTO @ShipmentGroupID

end
CLOSE ShipmentGroup
DEALLOCATE ShipmentGroup

--Mark as Printed if not desired to reprint
--If the order was already printed, ensure the warehouse is advised
