USE [QSPCanadaOrderManagement]
GO

SELECT  acc.Id AS AccountID, 
	acc.Name AS AccountName, 
	c.ID AS CampaignID, 
	Convert(varchar,c.StartDate,101) CampaignStartDate,
	Convert(varchar,c.EndDate,101) CampaignEndDate, 
	QspCanadaOrderManagement.dbo.Udf_ProgramsByCampaign(C.ID) Programs,
	fm.FirstName FMFirstName,
	fm.LastName FMLastName, 
	b.OrderID,
	d.producttype,
	Convert(varchar,b.Date,101) AS OrderDate, 
	i.INVOICE_ID AS InvoiceID, 
	CD.Description AS CAStatus, 
    CD1.Description AS BatchStatus, 
	CD2.Description AS OrderType, 
	CD3.Description AS OrderQualifier, 

	Case d.producttype
	When 46008 Then 0	
	Else SUM(d.Price) 
	End AS GrossSales,

	Case d.producttype
	When 46008 Then 0	
	Else SUM(d.Price) * .05 
	End AS FivePercentOfGrossSales,

	Case WHEN d.producttype = 46008 AND acc.CAccountCodeClass <> 'FM' THEN SUM(d.Price + ISNULL(d.Tax, 0) + ISNULL(d.Tax2, 0))
	Else 0
	End AS PrizeCost

Into #Orders
FROM		QSPCanadaOrderManagement.dbo.Batch b
JOIN		QSPCanadaOrderManagement.dbo.CustomerOrderHeader h ON b.ID = h.OrderBatchID AND b.[Date] = h.OrderBatchDate
JOIN		QSPCanadaOrderManagement.dbo.CustomerOrderDetail d ON h.Instance = d.CustomerOrderHeaderInstance
LEFT JOIN	QSPCanadaFinance.dbo.INVOICE i ON i.Invoice_ID = d.InvoiceNumber
LEFT JOIN	QSPCanadaCommon.dbo.CodeDetail CD1 ON b.StatusInstance = CD1.Instance
LEFT JOIN	QSPCanadaCommon.dbo.CodeDetail CD2 ON b.OrderTypeCode = CD2.Instance
LEFT JOIN	QSPCanadaCommon.dbo.CodeDetail CD3 ON b.OrderQualifierID = CD3.Instance
JOIN		QSPCanadaCommon.dbo.Campaign c ON b.CampaignID = c.ID
JOIN		QSPCanadaCommon.dbo.CAccount acc ON c.BillToAccountID = acc.Id
LEFT JOIN	QSPCanadaCommon.dbo.CodeDetail CD ON CD.Instance = c.Status
LEFT JOIN	QSPCanadaCommon.dbo.FieldManager fm ON c.FMID = fm.FMID 
WHERE		(b.OrderQualifierID IN (39001, 39002, 39006, 39009, 39015, 39020))
AND			c.StartDate BETWEEN '2013-01-01' AND '2013-06-30'
AND			ISNULL(c.IsStaffOrder, 0) = 0
and			b.OrderTypeCode <> 41012
AND			(d.ProductType NOT IN (46013, 46014, 46015))
AND			b.StatusInstance <> 40005
AND			d.DelFlag = 0
AND			d.StatusInstance NOT IN (500)
GROUP BY acc.Id, acc.Name, b.AccountID, acc.Name, c.ID, c.StartDate, c.EndDate,
		fm.FirstName, fm.LastName, b.OrderID, b.[Date],i.INVOICE_ID, acc.CAccountCodeClass, d.producttype,
		CD.Description, CD1.Description, CD2.Description, CD3.Description

--Details for QA / Audit
SELECT		o.AccountID,
			o.AccountName,
			o.CampaignID,
			o.Programs,
			o.FMFirstname,
			o.FMLastname,
			o.OrderID,
			o.OrderDate OrderDate,
			o.InvoiceID,
			pt.[Description] AS ProductType,
			o.GrossSales AS GrossSales,
			o.FivePercentOfGrossSales AS FivePercentOfGrossSales,
			o.PrizeCost AS PrizeCost
FROM		#Orders o
JOIN		QSPCanadaCommon..CodeDetail pt ON	pt.Instance = o.ProductType
ORDER BY	o.FMLastName, o.CampaignID, o.OrderID, o.ProductType

--Aggregation
SELECT		o.AccountID,
			o.AccountName,
			(SELECT MIN(o2.CampaignID) FROM #Orders o2 WHERE o2.PrizeCost > 0.00 AND o2.AccountID = o.AccountID) AS PrizeCampaignID,
			o.FMFirstname,
			o.FMLastname,
			SUM(o.GrossSales) AS GrossSales,
			SUM(o.FivePercentOfGrossSales) AS FivePercentOfGrossSales,
			SUM(o.PrizeCost) AS PrizeCost,
			SUM(o.PrizeCost) - SUM(o.FivePercentOfGrossSales) AS AmountOwing
INTO		#Aggregation
FROM		#Orders o
GROUP BY	o.AccountID,
			o.AccountName,
			o.FMFirstName,
			o.FMLastName
HAVING		SUM(o.PrizeCost) - SUM(o.FivePercentOfGrossSales) >= 100.00

--Report for FM's
SELECT		*
FROM		#Aggregation
ORDER BY	FMLastname, AccountID, PrizeCampaignID

--Update based on FM feedback
SELECT		agg.AccountID,
			agg.AccountName,
			agg.PrizeCampaignID,
			agg.FMFirstname,
			agg.FMLastname,
			agg.GrossSales,
			agg.FivePercentOfGrossSales,
			agg.PrizeCost,
			agg.AmountOwing,
			NULL AS FMCampaign,
			NULL AS FMAccount,
			'aaaaaaa' AS SchoolOrFMOrSplitOrNobodyToPay
INTO		#Adjustments
FROM		#Aggregation agg
ORDER BY	FMLastname, AccountID, PrizeCampaignID

--FM pays
UPDATE #Adjustments
SET FMCampaign = NULL, SchoolOrFMOrSplitOrNobodyToPay = 'FM'
WHERE AccountID IN (165, 2023, 2543, 7088, 7607)--(7610,16844,30581,9354,30157,9196,2657,2237,17452,16688,374,1830,369,382,302,1832,358,619,373,1446,18761,318,410,1268,322,624,1367,360,1933,1442,400,1874,749,751,16829,771,786,895,579,1056,  17393,15765,9028)

--School pays
UPDATE #Adjustments
SET FMCampaign = NULL, SchoolOrFMOrSplitOrNobodyToPay = 'SCHOOL'
WHERE AccountID IN --(8319,7729,3589,33082,3627,31892,5253,5007,5302,5321,5264,5019,5226,3953,18493,4144,16690,9763,9396,8639,9138,8589,18519,15827,15391,15615,16078,16119,16098,15960,4588,4612,4767,15796,16200,16233,11792,15925,15891,15815,15741,1088,15759,9259)

--Nobody pays
UPDATE #Adjustments
SET SchoolOrFMOrSplitOrNobodyToPay = 'Nobody'
WHERE AccountID IN (16414)--(17444,6149,18625)

--Split
UPDATE #Adjustments
SET FMCampaign = NULL, SchoolOrFMOrSplitOrNobodyToPay = 'Split'
WHERE AccountID IN --(0)

--Special deals
UPDATE #Adjustments
SET AmountOwing = 0.00
WHERE AccountID IN (0)

--FM Commission Accounts
UPDATE		adj
SET			FMCampaign = campFM.ID,
			FMAccount = campFM.BillToAccountID
FROM		#Adjustments adj
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = adj.PrizeCampaignID
LEFT JOIN	(QSPCanadaCommon..Campaign campFM
JOIN		QSPCanadaCommon..CAccount accFM
				ON	accFM.ID = campFM.BillToAccountID
				AND	accFM.CAccountCodeGroup = 'Comm')
				
				ON	camp.FMID = campFM.FMID
				AND	campFM.[Status] = 37002
WHERE		adj.SchoolOrFMOrSplitOrNobodyToPay = 'FM'

select * from #Adjustments

--Apply debit adjustments

BEGIN TRANSACTION

DECLARE @SchoolAdjustmentTypeID INT
SET	@SchoolAdjustmentTypeID = 49032 --Prize Overage
DECLARE @FMAdjustmentTypeID INT
SET	@FMAdjustmentTypeID = 49058 --Prize Overage

DECLARE @AccountID INT,
		@PrizeCampaignID INT,
		@AmountOwing NUMERIC(16,2),
		@FMCampaign INT,
		@FMAccount INT,
		@ToPay VARCHAR(50),
		@AdjustmentID INT

DECLARE		PrizeOverage CURSOR FOR
SELECT		AccountID,
			PrizeCampaignID,
			AmountOwing,
			FMCampaign,
			FMAccount,
			SchoolOrFMOrSplitOrNobodyToPay
FROM		#Adjustments
ORDER BY	PrizeCampaignID

OPEN PrizeOverage
FETCH NEXT FROM PrizeOverage INTO @AccountID, @PrizeCampaignID, @AmountOwing, @FMCampaign, @FMAccount, @ToPay


WHILE @@FETCH_STATUS = 0
BEGIN

	IF @ToPay = 'SCHOOL'
	BEGIN
		EXEC QSPCanadaFinance..AddInvoiceAdjustment
		@AccountID = @AccountID,
		@OrderID = NULL,
		@InternalComment = 'Prize Overage - School to pay',
		@Amount = @AmountOwing,
		@CampaignID = @PrizeCampaignID,
		@AdjustmentType = @SchoolAdjustmentTypeID,
		@ChangedBy = 612,
		@RefundID = null,
		@Value = @AdjustmentID OUTPUT 	
	END
	ELSE IF (@ToPay = 'FM' OR @ToPay = 'aaaaaaa')
	BEGIN
		EXEC QSPCanadaFinance..AddInvoiceAdjustment
		@AccountID = @FMAccount,
		@OrderID = NULL,
		@InternalComment = 'Prize Overage - FM to pay',
		@Amount = @AmountOwing,
		@CampaignID = @FMCampaign,
		@AdjustmentType = @FMAdjustmentTypeID,
		@ChangedBy = 612,
		@RefundID = null,
		@Value = @AdjustmentID OUTPUT 	
	END

	FETCH NEXT FROM PrizeOverage INTO @AccountID, @PrizeCampaignID, @AmountOwing, @FMCampaign, @FMAccount, @ToPay

END
CLOSE PrizeOverage
DEALLOCATE PrizeOverage

COMMIT

select top 99 *
from QSPCanadaFinance..ADJUSTMENT
order by ADJUSTMENT_ID desc

select top 199 *
from QSPCanadaFinance..GL_TRANSACTION
order by GL_TRANSACTION_ID desc

drop table #Orders
drop table #Aggregation
drop table #Adjustments