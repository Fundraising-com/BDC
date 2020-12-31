USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Remit_ReRemitSubsByCOD]    Script Date: 06/07/2017 09:20:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Remit_ReRemitSubsByCOD]  
  
 @CustomerOrderHeaderInstance int,  
 @TransID   int  
  
AS  
  
DECLARE @NewRemitBatchID int,  
 @Status   int,  
 @OriginalRemitBatchID int  
  
SELECT @NewRemitBatchID = rb.ID  
FROM RemitBatch rb  
WHERE rb.Status = 42000 --Not sent  
AND rb.FulfillmentHouseNbr =  
  (SELECT p.Fulfill_House_Nbr  
   FROM QSPCanadaProduct..Product p  
   JOIN QSPCanadaProduct..Pricing_Details pd  
    ON  pd.Product_Instance = p.Product_Instance  
   JOIN CustomerOrderDetail cod  
    ON cod.PricingDetailsID = pd.MagPrice_Instance  
    AND cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance  
    AND cod.TransID = @TransID)  
  
--Only modify the most recent action on the sub, e.g. new sub, chadd, cancellation  
SELECT TOP 1 @OriginalRemitBatchID = codrh.RemitBatchID  
FROM CustomerOrderDetailRemitHistory codrh  
WHERE codrh.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance  
AND codrh.TransID = @TransID  
ORDER BY codrh.RemitBatchID DESC  
  
--Move sub's crh records to new RemitBatchID  
UPDATE crh  
SET RemitBatchID = @newRemitBatchID  
FROM CustomerRemitHistory crh  
JOIN CustomerOrderDetailRemitHistory codrh  
  ON codrh.CustomerRemitHistoryInstance = crh.Instance  
  AND codrh.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance  
  AND codrh.TransID = @TransID  
  AND codrh.RemitBatchID = @OriginalRemitBatchID  
  
--Move sub's codrh records to new RemitBatchID and reset status  
UPDATE codrh  
SET codrh.RemitBatchID = @newRemitBatchID,
	--codrh.Status = 42000
	codrh.Status = CASE  
		WHEN codrh.Status in (42000, 42001, 42010) THEN 42000 --Sub  
		WHEN codrh.Status in (42002, 42003) THEN 42002 --Cancellation  
		WHEN codrh.Status in (42006, 42007) THEN 42006 --Chadd  
	END  
FROM CustomerOrderDetailRemitHistory codrh  
WHERE codrh.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance  
AND codrh.TransID = @TransID  
AND codrh.RemitBatchID = @OriginalRemitBatchID  
GO
