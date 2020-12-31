select AP_Cheque_Remit_ID, RemitBatchID, RemitCode, CreationDate, ProductSortName, NetAmount, GSTAmount, HSTAmount, PSTAmount QSTAmount
from AP_Cheque_remit apcr
where CreationDate BETWEEN '2012-03-01' and '2015-03-01'
and PSTAmount > 0.00
order by CreationDate

----

SELECT		apcr.AP_Cheque_Remit_ID,
			acc.BusinessUnitID,
			crh.State,
			ISNULL(SUM((ISNULL(pd.BasePriceSansPostage, 0) * ISNULL(pd.BaseRemitRate, 0)) + (ISNULL(pd.PostageAmount, 0) * ISNULL(pd.PostageRemitRate, 0))), 0) AS NetAmount,
			SUM(ISNULL(codrh.Tax, 0)) AS GSTHST,
			SUM(ISNULL(codrh.Tax2, 0)) AS QST
FROM		QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh
JOIN		QSPCanadaOrderManagement..CustomerRemitHistory crh
				ON	crh.Instance = codrh.CustomerRemitHistoryInstance
JOIN		QSPCanadaOrderManagement..RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
				AND	rb.runId = @RemitBatchID
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product prod
				ON	prod.Product_Instance = pd.Product_Instance
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = coh.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		#AP_Cheque_Remit apcr
				ON	apcr.RemitCode = prod.RemitCode
WHERE		codrh.Status IN (42000, 42001) --42000: Needs to be sent, 42001: Sent
GROUP BY	apcr.AP_Cheque_Remit_ID,
			acc.BusinessUnitID,
			crh.State
