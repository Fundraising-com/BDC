USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Remit_ReRemitSubsByPricingDetailsIDandRemitBatchID]    Script Date: 06/07/2017 09:20:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Remit_ReRemitSubsByPricingDetailsIDandRemitBatchID]

	@PricingDetailsID int,
	@RunIDFrom int,
	@RunIDTo int,
	@AlreadyRemittedToPublisher bit = true,
	@ReRemitSubs bit = true,
	@RemitInactiveMagSubs bit = true,
	@ReRemitCancels bit = false,
	@ReRemitChadds bit = false

AS

DECLARE	@newRemitBatchID	int

SELECT	@newRemitBatchID = rb.ID
FROM	RemitBatch rb
WHERE	rb.Status = 42000 --Not sent
AND		rb.FulfillmentHouseNbr =
			(SELECT	p.Fulfill_House_Nbr
			 FROM	QSPCanadaProduct..Pricing_Details pd
			 JOIN	QSPCanadaProduct..Product p
						ON	p.Product_Instance = pd.Product_Instance
			 WHERE	pd.MagPrice_Instance = @PricingDetailsID)


IF @ReRemitSubs = 1
BEGIN
	--Move subs' crh records to new RemitBatchID
	UPDATE	crh
	SET		RemitBatchID = @newRemitBatchID
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
	WHERE	rb.RunID BETWEEN @RunIDFrom AND @RunIDTo
	AND		pd.MagPrice_Instance = @PricingDetailsID
	AND		codrh.Status = 42001

	--Move subs' codrh records to new RemitBatchID and reset status
	UPDATE	codrh
	SET		codrh.RemitBatchID = @newRemitBatchID,
			codrh.Status = 42000
	FROM	CustomerOrderDetailRemitHistory codrh
	JOIN	RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
	JOIN	QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
	WHERE	rb.RunID BETWEEN @RunIDFrom AND @RunIDTo
	AND		pd.MagPrice_Instance = @PricingDetailsID
	AND		codrh.Status = 42001
END


IF @RemitInactiveMagSubs = 1
BEGIN
	--Move subs' crh records to new RemitBatchID
	UPDATE	crh
	SET		RemitBatchID = @newRemitBatchID
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
	WHERE	rb.RunID BETWEEN @RunIDFrom AND @RunIDTo
	AND		pd.MagPrice_Instance = @PricingDetailsID
	AND		codrh.Status = 42010

	--Move subs' codrh records to new RemitBatchID and reset status
	UPDATE	codrh
	SET		codrh.RemitBatchID = @newRemitBatchID,
			codrh.Status = 42000
	FROM	CustomerOrderDetailRemitHistory codrh
	JOIN	RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
	JOIN	QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
	WHERE	rb.RunID BETWEEN @RunIDFrom AND @RunIDTo
	AND		pd.MagPrice_Instance = @PricingDetailsID
	AND		codrh.Status = 42010
END


IF @ReRemitCancels = 1
BEGIN
	--Move subs' crh records to new RemitBatchID
	UPDATE	crh
	SET		RemitBatchID = @newRemitBatchID
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
	WHERE	rb.RunID BETWEEN @RunIDFrom AND @RunIDTo
	AND		pd.MagPrice_Instance = @PricingDetailsID
	AND		codrh.Status = 42003

	--Move subs' codrh records to new RemitBatchID and reset status
	UPDATE	codrh
	SET		codrh.RemitBatchID = @newRemitBatchID,
			codrh.Status = 42002
	FROM	CustomerOrderDetailRemitHistory codrh
	JOIN	RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
	JOIN	QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
	WHERE	rb.RunID BETWEEN @RunIDFrom AND @RunIDTo
	AND		pd.MagPrice_Instance = @PricingDetailsID
	AND		codrh.Status = 42003
END

IF @ReRemitChadds = 1
BEGIN
	--Move subs' crh records to new RemitBatchID
	UPDATE	crh
	SET		RemitBatchID = @newRemitBatchID,
			crh.StatusInstance = 42006
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
	WHERE	rb.RunID BETWEEN @RunIDFrom AND @RunIDTo
	AND		pd.MagPrice_Instance = @PricingDetailsID
	AND		codrh.Status = 42007

	--Move subs' codrh records to new RemitBatchID and reset status
	UPDATE	codrh
	SET		codrh.RemitBatchID = @newRemitBatchID,
			codrh.Status = 42006
	FROM	CustomerOrderDetailRemitHistory codrh
	JOIN	RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
	JOIN	QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
	WHERE	rb.RunID BETWEEN @RunIDFrom AND @RunIDTo
	AND		pd.MagPrice_Instance = @PricingDetailsID
	AND		codrh.Status = 42007
END

IF @AlreadyRemittedToPublisher = 0
BEGIN
	DECLARE @Count int
	SET @Count = @RunIDFROM
	WHILE @Count <= @RunIDTO
	BEGIN
		--Rerun Original Remit Files to remove subs from there
		DECLARE @RemitBatchID int
		
		SELECT	@RemitBatchID = codrh.RemitBatchID
		FROM	CustomerOrderDetailRemitHistory codrh
		JOIN	RemitBatch rb
					ON	rb.ID = codrh.RemitBatchID
		WHERE	rb.RunID = @Count
		AND		rb.FulfillmentHouseNbr =
					(SELECT	p.Fulfill_House_Nbr
					 FROM	QSPCanadaProduct..Pricing_Details pd
					 JOIN	QSPCanadaProduct..Product p
								ON	p.Product_Instance = pd.Product_Instance
					 WHERE	pd.MagPrice_Instance = @PricingDetailsID)

		EXEC ReprocessRemitBatchByRemitBatch @RemitBatchID
		Print 'Reprocessed Remit Batch ' + CONVERT(varchar,@RemitBatchID)
		SET	@Count = @Count + 1
	END

	PRINT 'Rerun AP if not already processed'
END
GO
