USE [QSPCanadaOrderManagement]
GO

--Ensure each FM has only 1 Commission Account
SELECT		fm.FirstName, fm.LastName, COUNT(*)
FROM		QSPCanadaCommon..Campaign campFM
JOIN		QSPCanadaCommon..CAccount accFM
				ON	accFM.ID = campFM.BillToAccountID
				AND	accFM.CAccountCodeGroup = 'Comm'
JOIN		QSPCanadaCommon..FieldManager fm on fm.fmid = campfm.fmid
WHERE		campFM.[Status] = 37002
GROUP BY	fm.FirstName, fm.LastName
HAVING		COUNT(*) > 1

--Details by School for QA
SELECT		fm.FMID AS FMID,--ISNULL(ccs.FMID, fm.FMID) AS FMID,
			fm.Firstname AS FMFirstName,--ISNULL(fmSplit.Firstname, fm.Firstname) AS FMFirstName,
			fm.Lastname AS FMLastName,--ISNULL(fmSplit.Lastname, fm.Lastname) AS FMLastName,
			accFM.ID AS FMAccountID,
			campFM.ID AS FMCampaignID,
			acc.ID AS SchoolAccountID,
			acc.Name AS SchoolName,
			camp.ID AS SchoolCampaignID,
			pst.[Description] ProductLine,
			100.00 CommissionPercentage,--ISNULL(ccs.CommissionPercentage, 100.00) CommissionPercentage,
			ROUND(SUM((invSec.Total_Tax_Included) * ISNULL(fma.Percentage/100.00, 1)), 2) AS GrossAmount,
			ROUND(SUM((invSec.Total_Tax_Amount) * ISNULL(fma.Percentage/100.00, 1)), 2) AS TaxAmount,
			ROUND(SUM(ISNULL(invSec.US_Postage_Amount, 0.00)), 2) AS USPostage,
			ROUND(SUM((invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00)) * ISNULL(fma.Percentage/100.00, 1)), 2) AS AdjustedGrossAmount,
			ROUND(SUM((invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00)) * ISNULL(fma.Percentage/100.00, 1)) * 0.02 /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/, 2) AS PrizeCost --Adjusted Gross
INTO		#Details
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
				AND	acc.CAccountCodeClass NOT IN ('FM')
JOIN		QSPCanadaCommon..CampaignProgram cp
				ON	cp.CampaignID = camp.ID
				AND	cp.DeletedTF = 0
				AND	cp.ProgramID IN (42, 48)
JOIN		Batch b
				ON	b.CampaignID = camp.ID
JOIN		QSPCanadaFinance..Invoice inv
				ON	inv.Order_ID = b.OrderID
JOIN		QSPCanadaFinance..Invoice_Section invSec
				ON	invSec.Invoice_ID = inv.Invoice_ID
JOIN		QSPCanadaProduct..ProgramSectionType pst
				ON	pst.ID = invSec.Section_Type_ID
JOIN		QSPCanadaCommon..FieldManager fmI
				ON	fmI.FMID = camp.FMID
LEFT JOIN	QSPCanadaCommon..FieldManagerAssociate fma
				ON fma.AssociateFMID = fmI.FMID AND inv.Invoice_Date <= fma.EffectiveToDate
JOIN		QSPCanadaCommon..FieldManager fm
				ON fm.FMID = ISNULL(fma.FMID, fmI.FMID)

/*LEFT JOIN	(QSPCanadaCommon..CampaignCommissionSplit ccs
JOIN		QSPCanadaCommon..FieldManager fmSplit
				ON	fmSplit.FMID = ccs.FMID)

				ON	ccs.CampaignID = camp.ID
				AND	inv.Invoice_Date <= ccs.EffectiveToDate	*/			

LEFT JOIN	(QSPCanadaCommon..Campaign campFM
JOIN		QSPCanadaCommon..CAccount accFM
				ON	accFM.ID = campFM.BillToAccountID
				AND	accFM.CAccountCodeGroup = 'Comm')
				
				ON	campFM.FMID = fm.FMID--ISNULL(fmSplit.FMID, fm.FMID)
				AND	campFM.[Status] = 37002

WHERE		invSec.Section_Type_ID IN (1, 2, 9, 10, 11, 13, 14, 15, 16)
AND			acc.BusinessUnitID = 1
and			camp.IsStaffOrder = 0
AND			inv.Invoice_Effective_Date BETWEEN '2018-07-01' AND '2019-06-30'
AND			b.Date <=	(SELECT		MAX(bL.Date)
						FROM		Batch bL
						JOIN		CustomerOrderHeader coh
										ON	coh.OrderBatchID = bL.ID
										AND	coh.OrderBatchDate = bL.Date
						JOIN		CustomerOrderDetail cod
										ON	cod.CustomerOrderHeaderInstance = coh.Instance
						WHERE		cod.ProductType IN (46013, 46014)
						AND			cod.DelFlag = 0
						AND			bL.CampaignID = camp.ID
						AND			(b.OrderQualifierID IN (39001, 39002) OR b.OrderQualifierID = 39009 AND bL.OrderQualifierID = 39001))

/*AND			camp.ID IN		(SELECT		DISTINCT b.CampaignID
							FROM		Batch b
							JOIN		CustomerOrderHeader coh
											ON	coh.OrderBatchID = b.ID
											AND	coh.OrderBatchDate = b.Date
							JOIN		CustomerOrderDetail cod
											ON	cod.CustomerOrderHeaderInstance = coh.Instance
							WHERE		cod.ProductType IN (46013, 46014)
							AND			cod.DelFlag = 0)*/
--and fm.fmid <> '0511'
and fm.fmid NOT IN ('0506','1552') --Carmine, Debbie
--and ISNULL(ccs.FMID, fm.FMID) <> '0074'
GROUP BY	fm.FMID,--ISNULL(ccs.FMID, fm.FMID),
			fm.Firstname,--ISNULL(fmSplit.Firstname, fm.Firstname),
			fm.Lastname,--ISNULL(fmSplit.Lastname, fm.Lastname),
			accFM.ID,
			campFM.ID,
			acc.ID,
			acc.Name,
			camp.ID,
			invSec.Section_Type_ID,
			pst.[Description]--,
			--ccs.CommissionPercentage
ORDER BY	fm.FMID,
			accFM.ID,
			campFM.ID,
			pst.[Description]

SELECT	*
FROM	#Details
ORDER BY FMID, SchoolCampaignID

--Aggregation
SELECT		FMID,
			FMFirstName,
			FMLastName,
			FMAccountID,
			FMCampaignID,
			SUM(PrizeCost) PrizeCost
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
SET	@AdjustmentTypeID = 49058 --Prize Cost

DECLARE @FMAccountID INT,
		@FMCampaignID INT,
		@Amount NUMERIC(16,2),
		@AdjustmentID INT

DECLARE		PrizeCost CURSOR FOR
SELECT		FMAccountID,
			FMCampaignID,
			PrizeCost
FROM		#Adjustments
ORDER BY	FMCampaignID

OPEN PrizeCost
FETCH NEXT FROM PrizeCost INTO @FMAccountID, @FMCampaignID, @Amount


WHILE @@FETCH_STATUS = 0
BEGIN

	EXEC QSPCanadaFinance..AddInvoiceAdjustment
		@AccountID = @FMAccountID,
		@OrderID = NULL,
		@InternalComment = 'Prize Cost - FM',
		@Amount = @Amount,
		@CampaignID = @FMCampaignID,
		@AdjustmentType = @AdjustmentTypeID,
		@ChangedBy = 612,
		@RefundID = null,
		@Value = @AdjustmentID OUTPUT 	

	FETCH NEXT FROM PrizeCost INTO @FMAccountID, @FMCampaignID, @Amount

END
CLOSE PrizeCost
DEALLOCATE PrizeCost

COMMIT

select top 99 *
from QSPCanadaFinance..ADJUSTMENT
order by ADJUSTMENT_ID desc

select top 199 *
from QSPCanadaFinance..GL_TRANSACTION
order by GL_TRANSACTION_ID desc

DROP TABLE #Details
DROP TABLE #Adjustments