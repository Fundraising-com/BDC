USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Remit_ReRemitSubsByProductandRemitBatchID]    Script Date: 06/07/2017 09:20:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Remit_ReRemitSubsByProductandRemitBatchID]

	@Product_Code varchar(20),
	@RunIDFrom int,
	@RunIDTo int,
	@AlreadyRemittedToPublisher bit = true,
	@ReRemitSubs bit = true,
	@RemitInactiveMagSubs bit = true,
	@ReRemitCancels bit = false,
	@ReRemitChadds bit = false

AS

DECLARE	@PricingDetailsID	int

DECLARE PricingDetailsIDs CURSOR FOR
SELECT	DISTINCT pd.MagPrice_Instance
FROM	CustomerRemitHistory crh
JOIN	CustomerOrderDetailRemitHistory codrh
			ON	codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN	RemitBatch rb
			ON	rb.ID = codrh.RemitBatchID
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
			AND	cod.TransID = codrh.TransID
JOIN	QSPCanadaProduct..Pricing_Details pd
			ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN	QSPCanadaProduct..Product p
			ON	p.Product_Instance = pd.Product_Instance
WHERE	p.Product_Code = @Product_Code
AND		rb.RunID BETWEEN @RunIDFrom AND @RunIDTo

OPEN PricingDetailsIDs
FETCH NEXT FROM PricingDetailsIDs INTO  @PricingDetailsID
							
WHILE(@@fetch_status = 0)
BEGIN
	exec pr_Remit_ReRemitSubsByPricingDetailsIDandRemitBatchID
			@PricingDetailsID,@RunIDFrom, @RunIDTo,
			@AlreadyRemittedToPublisher, @ReRemitSubs,
			@RemitInactiveMagSubs, @ReRemitCancels, @ReRemitChadds

FETCH NEXT FROM PricingDetailsIDs INTO @PricingDetailsID
							
END
CLOSE PricingDetailsIDs
DEALLOCATE PricingDetailsIDs
GO
