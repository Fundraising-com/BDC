USE QSPCanadaOrderManagement
GO

DECLARE @OrderStageReceiptID INT

CREATE TABLE #temp
(OrderStageReceiptID int)

INSERT #temp
      SELECT OrderStageReceiptID
      FROM  QSPCanadaOrderManagement..orderstagereceipt
      WHERE orderstagereceiptbatchid in (3327)
      --AND statusID = 1
      ORDER BY OrderStageReceiptID      

DECLARE c cursor for 
      SELECT OrderStageReceiptID
      FROM  #temp
      
Open c
fetch NEXT from c into @OrderStageReceiptID;

while @@fetch_status = 0
BEGIN
      exec QSPCanadaOrderManagement..OrderStageReceipt_UpdateOrderStageInfo @OrderStageReceiptID

      fetch NEXT from c into @OrderStageReceiptID
END

DROP TABLE #temp
CLOSE c
DEALLOCATE c
