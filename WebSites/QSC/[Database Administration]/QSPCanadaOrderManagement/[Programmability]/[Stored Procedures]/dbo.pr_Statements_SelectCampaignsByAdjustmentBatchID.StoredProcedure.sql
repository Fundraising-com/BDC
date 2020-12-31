USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Statements_SelectCampaignsByAdjustmentBatchID]    Script Date: 06/07/2017 09:20:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_Statements_SelectCampaignsByAdjustmentBatchID]

	@iAdjustmentBatchID int = 0

AS

--For last 2 manual statement runs still need this:
SELECT		camp.BillToAccountID AS Account_ID,
			camp.ID AS Campaign_ID,
			camp.FMID
FROM		QSPCanadaCommon..Campaign camp
WHERE		camp.ID IN (SELECT	DISTINCT
								CampaignID
						FROM	QSPCanadaFinance..UDF_Statement_GetDetails(NULL, NULL, NULL) usg
						WHERE	TransactionAmount <> 0.00
						AND		TransactionType = @iAdjustmentBatchID) --4 for Online, 5 for Cust Svc			
ORDER BY	camp.ID

/*
SELECT		adj.Account_ID,
			adj.Campaign_ID,
			c.FMID
FROM		QSPCanadaFinance..Adjustment adj
JOIN		QSPCanadaCommon..Campaign c
				ON	c.ID = adj.Campaign_ID
WHERE		Adjustment_Batch_ID = @iAdjustmentBatchID
ORDER BY	Campaign_ID*/
GO
