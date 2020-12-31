USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllStatementsToPrint]    Script Date: 06/07/2017 09:17:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[GetAllStatementsToPrint] 	@FromDate 	datetime,
							@ToDate 	datetime

--MS Jan 30th 2008 Chnage for New Statement (Summary by CA)
AS
SET NOCOUNT ON

SET @ToDate = CONVERT(DATETIME, CONVERT(VARCHAR, @ToDate, 112))

DECLARE	@dCampaignStartDate	datetime,
		@dCampaignEndDate	datetime


SELECT	@dCampaignStartDate = StartDate
FROM		QSPCanadaCommon..Season
WHERE	Season IN ('F', 'S')
AND		@FromDate BETWEEN StartDate AND EndDate


SELECT	@dCampaignEndDate = EndDate
FROM		QSPCanadaCommon..Season
WHERE	Season IN ('F', 'S')
AND		@ToDate BETWEEN StartDate AND EndDate


SELECT	DISTINCT a.ID AS AccountID, 
		a.Name,
		fm.FMID, 
		fm.LastName,
		fm.FirstName,
		c.Lang
FROM		QSPCanadaCommon..CAccount a
INNER JOIN	QSPCanadaCommon..Campaign c
			ON	c.BillToAccountID = a.ID
			AND	c.StartDate <= @dCampaignEndDate
			AND	c.EndDate >= @dCampaignStartDate
INNER JOIN	QSPCanadaCommon..FieldManager fm
			ON	fm.FMID = c.FMID
ORDER BY	a.Name






/******************************************************************************************************************************************
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 7/6/2004 
--   Get Statements ready to print For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

--Invoices First
SELECT B.AccountID as AccountID,
	B.OrderID as OrderID,
	Acct.Name as Name, 
	SUM(ISNULL(Invoice_Amount, 0.0)) as InvoiceAmount,
	Convert(numeric(10,2),0.0) as PaymentAmount,
	Convert(numeric(10,2),0.0) as AdjustmentAmount
INTO #Temp
FROM QSPCanadaOrderManagement..Batch B
LEFT JOIN QSPCanadaCommon..CAccount Acct on Acct.ID = B.AccountID
LEFT JOIN Invoice I on I.Order_Id = B.OrderId
--WHERE CONVERT(datetime, CONVERT(varchar, DateBatchCompleted,112)) Between CONVERT(datetime, CONVERT(varchar,@FromDate,112)) AND CONVERT(datetime, CONVERT(varchar,@ToDate,112))
--MS June 29, 2005 Null batch date '01/01/1995' issue
Where (Case Cast(Convert(Varchar(10),DateBatchCompleted,101)as datetime)
      	When Cast('01/01/1995' as datetime) Then I.Invoice_Date
     	 Else  DateBatchCompleted
     	 End ) Between CONVERT(datetime, CONVERT(varchar,@FromDate,101)) AND CONVERT(datetime, CONVERT(varchar,@ToDate,101))
GROUP BY B.AccountID, B.OrderID, Acct.Name


--Get any Payments with matching order id's.  
INSERT #TEMP
SELECT P.Account_ID, 
	P.Order_ID, --OrderID
	T.Name, 
	0.0, --InvoiceAmount
	ISNULL(Payment_Amount, 0.0),
	0.0 --AdjustmentAmount
FROM Payment P
	INNER JOIN #Temp T on T.AccountId = P.Account_ID 
		AND T.OrderID = P.Order_ID 
WHERE CONVERT(datetime, CONVERT(varchar, Payment_Effective_Date,112)) Between CONVERT(datetime, CONVERT(varchar,@FromDate,112)) AND CONVERT(datetime, CONVERT(varchar,@ToDate,112))

--Get any Payments without matching order id's
INSERT #TEMP
SELECT P.Account_ID, 
	ISNULL(P.Order_ID,0), --OrderID
	T.Name, 
	0.0,--InvoiceAmount
	ISNULL(Payment_Amount, 0.0),
	0.0 --AdjustmentAmount
FROM Payment P
INNER JOIN #Temp T on T.AccountID = Account_ID
Where CONVERT(datetime, CONVERT(varchar, Payment_Effective_Date,112)) Between CONVERT(datetime, CONVERT(varchar,@FromDate,112)) AND CONVERT(datetime, CONVERT(varchar,@ToDate,112))
AND ORDER_ID Not in (SELECT OrderID FROM #Temp)

--Get any Adjustments with matching order id's
INSERT #TEMP
SELECT DISTINCT A.Account_ID, 
	T.OrderID, --OrderID
	T.Name, 
	0.0, --InvoiceAmount
	0.0, --PaymentAmount
	ISNULL(Adjustment_Amount, 0.0)
FROM Adjustment A
	INNER JOIN QSPCanadaOrderManagement..Batch B on B.OrderID = A.Order_ID
	INNER JOIN #Temp T on T.AccountId = Account_ID 
		AND T.OrderId = A.Order_ID 
WHERE CONVERT(datetime, CONVERT(varchar, Adjustment_Effective_Date,112)) Between CONVERT(datetime, CONVERT(varchar,@FromDate,112)) AND CONVERT(datetime, CONVERT(varchar,@ToDate,112))


--Get any Adjustments without matching order id's
INSERT #TEMP
SELECT DISTINCT A.Account_ID, 
	ISNULL(A.Order_ID,0), --OrderID
	T.Name, 
	0.0,--InvoiceAmount
	0.0, --PaymentAmount
	Adjustment_Amount
FROM Adjustment A
	RIGHT JOIN #Temp T on T.AccountId = Account_ID 
		AND ( (A.Order_ID IS NULL) OR (A.Order_ID not in (SELECT OrderID FROM #Temp ))  )
WHERE CONVERT(datetime, CONVERT(varchar, Adjustment_Effective_Date,112)) Between CONVERT(datetime, CONVERT(varchar,@FromDate,112)) AND CONVERT(datetime, CONVERT(varchar,@ToDate,112))


SELECT  T.AccountID, 
	T.Name,
	MAX(C.FMID) AS FMID, 
	MAX(FM.LastName) as LastName,
	MAX(FM.FirstName) as FirstName,
	MAX(C.Lang) as Lang
FROM QSPCanadaCommon..CAccount A 
	INNER JOIN QSPCanadaCommon..Campaign C on C.BillToAccountID = A.ID 
		AND StartDate BETWEEN  @FromDate AND @ToDate
	INNER JOIN QSPCanadaCommon..FieldManager FM on FM.FMID = C.FMID
	INNER JOIN #Temp T on T.AccountID = A.ID
	INNER JOIN QSPCanadaOrderManagement..Batch B on B.OrderID = T.OrderID
--WHERE B.OrderQualifierID         <> 39009 --Exclude Internet
GROUP BY T.AccountID, T.Name
HAVING SUM (InvoiceAmount-PaymentAmount-AdjustmentAmount) <> 0
ORDER BY T.Name

DROP TABLE #Temp
***********************************************************************************************************************************************************************************/

SET NOCOUNT OFF
GO
