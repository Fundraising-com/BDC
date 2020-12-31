USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentReceipt_Reprocess]    Script Date: 06/07/2017 09:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentReceipt_Reprocess]
AS

DECLARE 
	@ShipmentReceiptID	INT,
	@OrderBatchID		INT

SET @OrderBatchID = 10041720


CREATE TABLE #temp
(ShipmentReceiptID int)

INSERT #temp
      SELECT ShipmentReceiptID
      FROM  QSPCanadaOrderManagement..shipmentreceipt
      WHERE BatchOrderID = @OrderBatchID AND statusID = 1
	  ORDER BY ShipmentReceiptID      


DECLARE c cursor for 
      SELECT ShipmentReceiptID
      FROM  #temp
      
Open c
fetch NEXT from c into @ShipmentReceiptID

while @@fetch_status = 0
BEGIN
      --exec QSPCanadaOrderManagement..ShipmentReceipt_UpdateShipmentInfo @ShipmentReceiptID
									 
      fetch NEXT from c into @ShipmentReceiptID
END

DROP TABLE #temp
CLOSE c
DEALLOCATE c
GO
