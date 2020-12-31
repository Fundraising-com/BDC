USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetStatementSectionInfo]    Script Date: 06/07/2017 09:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetStatementSectionInfo]
	@AccountID 	int,
	@StartDate	datetime,
	@EndDate	datetime
AS

CREATE TABLE #Temp
(
	AccountID		int,
	CampaignID		int,
	CAStart			datetime,
	CAEnd			datetime,
	CALang			varchar(10),
	OrderId			int,
	DocumentNumber 	int,
	TransactionDate		datetime,
	DueDate		datetime,
	TransactionType 	varchar(50), 
	Reference		varchar(50), 
	TransactionAmount	numeric(10,2),
	InvoiceAmount		numeric(10,2)
) 

--Invoice
INSERT #Temp
SELECT @AccountID,
	B.CampaignID,
	c.StartDate,
	c.EndDate,
	c.Lang,
	i.Order_Id,
	Invoice_ID, 
	Invoice_Date,
	Invoice_Due_Date,
	CASE c.Lang WHEN 'FR' THEN 'Facture' ELSE 'Invoice' END,
	null,
	Invoice_Amount,
	0 	--InvoiceAmountForPayments
FROM Invoice I
	INNER JOIN QSPCanadaOrderManagement..Batch B  on B.OrderID = I.Order_ID
	INNER JOIN QSPCanadaCommon..CAccount A on A.ID = B.AccountID
	INNER JOIN QSPCanadaCommon..Campaign c on. B.campaignId=c.ID
WHERE  I.Account_Id = @AccountID
AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) BETWEEN @StartDate and @EndDate
AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39020)   OR	----Main Staff, PS (few PS in 2005 were invoiced) or supplementary or Cust Serv invoice earlier than Nov 09 MS Feb 6 2007
    (B.OrderQualifierID   IN (39013,39015  ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006'))
AND B.OrderTypeCode     <> 41009 --Exclude Magnet	


--Adjustment
INSERT #Temp
SELECT @AccountID,
	a.Campaign_ID,
	c.StartDate,
	c.EndDate,
	c.Lang,
	a.Order_Id,
	a.Adjustment_ID, 
	a.Adjustment_Effective_Date, 
	null, 
	CASE c.Lang WHEN 'FR' THEN COALESCE(adjt.French_Name, COALESCE(adjt.Name, '')) ELSE COALESCE(adjt.Name, '') END,
	CASE c.Lang WHEN 'FR' THEN 'Commande #' ELSE 'Order #' END + Convert(varchar, a.Order_Id),
	CASE a.Adjustment_Type_ID
		WHEN 49021 THEN ABS(a.Adjustment_Amount) --Write off debit
		WHEN 49002 THEN ABS(a.Adjustment_Amount) --NSF Check
		WHEN 49009 THEN ABS(a.Adjustment_Amount) --Other Debit
		WHEN 49024 THEN ABS(a.Adjustment_Amount) --Refund Check
		WHEN 49029 THEN
		CASE WHEN coalesce((SELECT adj.Adjustment_Amount FROM Adjustment adj WHERE adj.Campaign_ID = c.ID AND adj.Adjustment_Type_ID = 49016), 0) > a.Adjustment_Amount
			THEN 0
			ELSE a.Adjustment_Amount - coalesce((SELECT adj.Adjustment_Amount FROM Adjustment adj WHERE adj.Campaign_ID = c.ID AND adj.Adjustment_Type_ID = 49016), 0)
			END
		ELSE (-1 * a.Adjustment_Amount)
	END as Adjustment_Amount,
	0 	--InvoiceAmountForPayments
FROM Adjustment a
	INNER JOIN QSPCanadaCommon..Campaign c on. a.campaign_Id=c.ID
	LEFT JOIN Adjustment_Type adjt on adjt.Adjustment_Type_ID = A.Adjustment_Type_ID
WHERE a.Account_Id = @AccountID
AND CONVERT(datetime, CONVERT(varchar, a.Adjustment_Effective_Date,112)) BETWEEN @StartDate and @EndDate
-- Exclude postage costs adjustments for MagNet (Ben, 05-02-04)
AND a.Adjustment_Type_ID <> 49016

--Payment
INSERT #Temp
SELECT @AccountID,
	B.CampaignID,
	c.StartDate,
	c.EndDate,
	c.Lang,
	p.Order_Id,
	Invoice_ID,
	Payment_Effective_Date,
	null,
	CASE c.Lang WHEN 'FR'  THEN 'Paiement' ELSE 'Payment' END ,   --THEN 'Paiement - Merci' ELSE 'Payment - Thank You' END,
	CASE Payment_Method_ID 		WHEN  50002 THEN CASE c.Lang WHEN 'FR' THEN 'Chèque #' ELSE 'Cheque #' END + Cheque_Number --'Check/Cash'
		WHEN  50003 THEN 'Visa'
		WHEN  50004 THEN 'Master Card'
	ELSE 'Other'
	END,
	(-1 * Payment_Amount),
	Invoice_Amount
FROM Payment P
	INNER JOIN QSPCanadaOrderManagement..Batch B  on B.OrderID = P.Order_ID
	INNER JOIN QSPCanadaCommon..Campaign c on. B.campaignId=c.ID
	INNER JOIN QSPCanadaCommon..CAccount A on A.ID = B.AccountID
	LEFT JOIN Invoice I on I.Order_ID = P.Order_ID
WHERE P.Account_Id = @AccountID
AND CONVERT(datetime, CONVERT(varchar,Payment_Effective_Date,112)) BETWEEN @StartDate and @EndDate
AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39020)   OR	----Main Staff or supplementary or Cust Serv invoice earlier than Nov 09 MS Feb 6 2007
    (B.OrderQualifierID    IN (39013,39015 ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006'))
AND B.OrderTypeCode     <> 41009 --Exclude Magnet	

SELECT MAX(AccountID) AccountID,
	   CampaignID CampaignID,
	   CAStart,CAEnd,QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaignLang (CampaignID,CALang) ProgramRunning,
	   SUM(TransactionAmount) CampaignBalance
FROM #Temp 
GROUP BY CampaignID,CAStart,CAEnd,CALang
HAVING SUM(TransactionAmount)  <> 0
ORDER BY CampaignID

DROP TABLE #Temp



SET NOCOUNT OFF
GO
