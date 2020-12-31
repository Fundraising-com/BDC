SELECT		cod.CustomerOrderHeaderInstance,
			cod.TransID
INTO		#SubsToChange
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
				AND	codrh.Status in (42000,42001)
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
				AND	p.Status = 30600
WHERE		(cod.GiftCD = 'X'
			OR	codrh.GiftOrderType = 'X')
AND			ISNULL(cod.IsGiftCardSent, 0) = 0
AND			cod.CreationDate > '2014-07-01'

BEGIN TRAN

UPDATE		codrh
SET			codrh.GiftOrderType = 'R'
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		#SubsToChange stc
				ON	stc.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	stc.TransID = codrh.TransID

UPDATE		cod
SET			cod.GiftCD = 'R'
FROM		CustomerOrderDetail cod
JOIN		#SubsToChange stc
				ON	stc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	stc.TransID = cod.TransID

COMMIT TRAN