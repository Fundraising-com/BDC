USE [QSPCanadaOrderManagement]
GO

--Details by School for QA
SELECT		fm.FMID,
			fm.Firstname AS FMFirstName,
			fm.Lastname AS FMLastName,
			accFM.ID AS FMAccountID,
			campFM.ID AS FMCampaignID,
			acc.ID AS SchoolAccountID,
			acc.Name AS SchoolName,
			camp.ID AS SchoolCampaignID,
			pst.[Description] AS ProductLine,
			SUM(invSec.Item_Count) UnitsSold,
			CASE invSec.Section_Type_ID
				WHEN 2 THEN 0.80
				WHEN 1 THEN 0.30
				WHEN 11 THEN 0.30
				WHEN 9 THEN 0.30
			 END AS ProfitPerUnit,
			SUM(invSec.Item_Count) * CASE invSec.Section_Type_ID
										WHEN 2 THEN 0.80
										WHEN 1 THEN 0.30
										WHEN 11 THEN 0.30
										WHEN 9 THEN 0.30
									 END AS Profit
INTO		#Details
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
				AND	acc.CAccountCodeClass NOT IN ('FM')
JOIN		QSPCanadaCommon..CampaignProgram cp
				ON	cp.CampaignID = camp.ID
				AND	cp.DeletedTF = 0
				AND	cp.ProgramID = 46 --Prize C
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
JOIN		Batch b
				ON	b.CampaignID = camp.ID
JOIN		QSPCanadaFinance..Invoice inv
				ON	inv.Order_ID = b.OrderID
JOIN		QSPCanadaFinance..Invoice_Section invSec
				ON	invSec.Invoice_ID = inv.Invoice_ID
JOIN		QSPCanadaProduct..ProgramSectionType pst
				ON	pst.ID = invSec.Section_Type_ID

LEFT JOIN	(QSPCanadaCommon..Campaign campFM
JOIN		QSPCanadaCommon..CAccount accFM
				ON	accFM.ID = campFM.BillToAccountID
				AND	accFM.CAccountCodeGroup = 'Comm')
				
				ON	campFM.FMID = fm.FMID
				AND	campFM.[Status] = 37002

WHERE		invSec.Section_Type_ID IN (2, 1, 11, 9)
AND			inv.Invoice_Effective_Date BETWEEN '2013-01-01' AND '2013-06-30'
GROUP BY	fm.FMID,
			fm.Firstname,
			fm.Lastname,
			accFM.ID,
			campFM.ID,
			acc.ID,
			acc.Name,
			camp.ID,
			invSec.Section_Type_ID,
			pst.[Description]
ORDER BY	fm.FMID,
			accFM.ID,
			campFM.ID,
			pst.[Description]

SELECT	*
FROM	#Details
ORDER BY SchoolCampaignID

--Aggregation
SELECT		FMID,
			FMFirstName,
			FMLastName,
			FMAccountID,
			FMCampaignID,
			SUM(Profit) Profit
INTO		#Adjustments
FROM		#Details
GROUP BY	FMID,
			FMFirstName,
			FMLastName,
			FMAccountID,
			FMCampaignID
ORDER BY	FMID

SELECT	*
FROM	#Adjustments

--Apply credit adjustments

BEGIN TRANSACTION

DECLARE @AdjustmentTypeID INT
SET	@AdjustmentTypeID = 49059 --Prize C

DECLARE @FMAccountID INT,
		@FMCampaignID INT,
		@Amount NUMERIC(16,2),
		@AdjustmentID INT

DECLARE		PrizeC CURSOR FOR
SELECT		FMAccountID,
			FMCampaignID,
			Profit
FROM		#Adjustments
ORDER BY	FMCampaignID

OPEN PrizeC
FETCH NEXT FROM PrizeC INTO @FMAccountID, @FMCampaignID, @Amount


WHILE @@FETCH_STATUS = 0
BEGIN

	EXEC QSPCanadaFinance..AddInvoiceAdjustment
		@AccountID = @FMAccountID,
		@OrderID = NULL,
		@InternalComment = 'Prize C',
		@Amount = @Amount,
		@CampaignID = @FMCampaignID,
		@AdjustmentType = @AdjustmentTypeID,
		@ChangedBy = 612,
		@RefundID = null,
		@Value = @AdjustmentID OUTPUT 	

	FETCH NEXT FROM PrizeC INTO @FMAccountID, @FMCampaignID, @Amount

END
CLOSE PrizeC
DEALLOCATE PrizeC

COMMIT

select top 99 *
from QSPCanadaFinance..ADJUSTMENT
order by ADJUSTMENT_ID desc

select top 199 *
from QSPCanadaFinance..GL_TRANSACTION
order by GL_TRANSACTION_ID desc

DROP TABLE #Details
DROP TABLE #Adjustments