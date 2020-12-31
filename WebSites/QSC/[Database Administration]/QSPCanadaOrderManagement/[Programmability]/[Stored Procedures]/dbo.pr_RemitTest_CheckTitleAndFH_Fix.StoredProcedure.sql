USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_CheckTitleAndFH_Fix]    Script Date: 06/07/2017 09:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_CheckTitleAndFH_Fix]

@iRunID 	int = 0

AS
CREATE TABLE	#switchTitles
(
	RemitCode		varchar(20),
	RemitBatchID	int
)

/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

INSERT INTO	#switchTitles
SELECT		DISTINCT
			codrh.RemitCode,
			rb2.ID AS RemitBatchID
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
				AND	rb.RunID = @iRunID
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
				--AND	p.Product_Year = @ProductYear
				--AND	p.Product_Season = @ProductSeason
JOIN		RemitBatch rb2
				ON	rb2.FulfillmentHouseNbr = p.Fulfill_House_Nbr
				AND	rb2.RunID = @iRunID
WHERE		rb.FulfillmentHouseNbr <> p.Fulfill_House_Nbr

UPDATE		crh
SET			crh.RemitBatchID = st.RemitBatchID
FROM		CustomerRemitHistory crh
JOIN		CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
				AND	rb.RunID = @iRunID
JOIN		#switchTitles st
				ON	st.RemitCode = codrh.RemitCode
				AND	st.RemitBatchID <> rb.ID

/*UPDATE		crha
SET			crha.RemitBatchID = st.RemitBatchID
FROM		CustomerRemitHistoryAudit crha
JOIN		CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerRemitHistoryInstance = crha.Instance
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
				AND	rb.RunID = @iRunID
JOIN		#switchTitles st
				ON	st.RemitCode = codrh.RemitCode
				AND	st.RemitBatchID <> rb.ID
WHERE		crha.AuditDate =
			(SELECT		TOP 1
						crha2.AuditDate
			FROM		CustomerRemitHistoryAudit crha2
			WHERE		crha2.Instance = crha.Instance
			ORDER BY	crha2.AuditDate DESC)*/

UPDATE		codrh
SET			codrh.RemitBatchID = st.RemitBatchID
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
				AND	rb.RunID = @iRunID
JOIN		#switchTitles st
				ON	st.RemitCode = codrh.RemitCode
				AND	st.RemitBatchID <> rb.ID

/*UPDATE		codrha
SET			codrha.RemitBatchID = st.RemitBatchID
FROM		CustomerOrderDetailRemitHistoryAudit codrha
JOIN		RemitBatch rb
				ON	rb.ID = codrha.RemitBatchID
				AND	rb.RunID = @iRunID
JOIN		#switchTitles st
				ON	st.RemitCode = codrha.RemitCode
				AND	st.RemitBatchID <> rb.ID
WHERE		codrha.AuditDate =
			(SELECT		TOP 1
						codrha2.AuditDate
			FROM		CustomerOrderDetailRemitHistoryAudit codrha2
			WHERE		codrha2.CustomerRemitHistoryInstance = codrha.CustomerRemitHistoryInstance
			ORDER BY	codrha2.AuditDate DESC)*/

DROP TABLE	#switchTitles
GO
