USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllInvoicesByDate]    Script Date: 06/07/2017 09:17:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllInvoicesByDate] 
	@FromDate 		datetime,
	@ToDate 		datetime,
	@AccountName	varchar(50) = '',
	@AccountID		int = 0,
	@OrderID		int = 0,
	@InvoiceID		int = 0,
	@CampaignID		int = 0
	
AS

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/6/2004 
--   Get Invoice List For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

IF @AccountName = '' SET @AccountName = NULL
IF @AccountID = 0 SET @AccountID = NULL
IF @OrderID = 0 SET @OrderID = NULL
IF @InvoiceID = 0 SET @InvoiceID = NULL
IF @CampaignID = 0 SET @CampaignID = NULL

SELECT Invoice_ID, CampaignId,
	CD.Description AS AccountType,		
	A.ID as Account_ID, OrderId as Order_ID, Invoice_Date, Invoice_Due_Date, Invoice_Amount, Is_Printed, DateTime_Approved, Name as Group_Name,
	convert(numeric(10,2),0.0) as Adjustments, 
	convert(numeric(10,2),0.0) as Payments,
	convert(numeric(10,2),0.0) as Balance
INTO #Temp
FROM QSPCanadaOrderManagement..Batch B
	LEFT JOIN INVOICE I on I.Order_ID = B.OrderId
	LEFT JOIN QSPCanadaCommon..CAccount A on B.AccountID = A.ID
	LEFT JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = I.Account_Type_ID	
WHERE 
(CONVERT(datetime, CONVERT(varchar,B.[Date],112)) BETWEEN @FromDate AND @ToDate OR (@FromDate IS NULL OR @ToDate IS NULL)) 
AND	A.Name LIKE '%' + ISNULL(@AccountName, A.Name) + '%'
AND A.Id = ISNULL(@AccountID, A.Id)
AND	B.OrderID = ISNULL(@OrderID, B.OrderID)
AND	(I.INVOICE_ID = ISNULL(@InvoiceID, I.Invoice_ID) OR (I.INVOICE_ID IS NULL AND @InvoiceID IS NULL))
AND	B.CampaignID = ISNULL(@CampaignID, B.CampaignID)
AND b.orderQualifierId <> 39008  -- Added Apr 27 2005 MS

-- Create Index
CREATE INDEX OrderIDIndex_1 on #Temp (Order_ID)

UPDATE #Temp 
SET Adjustments = 
(SELECT ISNULL(SUM(adjustment_amount),0) FROM adjustment WHERE order_id = #Temp.Order_ID)

UPDATE #Temp 
SET Payments =  
(SELECT ISNULL(SUM(payment_amount),0) FROM payment WHERE order_id = #Temp.Order_ID)

UPDATE #Temp 
SET Balance =  
(SELECT Invoice_Amount - (Adjustments +Payments))

SELECT * FROM #Temp ORDER BY Group_Name, Invoice_Date
DROP TABLE #Temp

SET NOCOUNT OFF
GO
