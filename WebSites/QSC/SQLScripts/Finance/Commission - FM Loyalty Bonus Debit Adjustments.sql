USE QSPCanadaFinance
GO
--Should check if 2nd year didn't run; if so, then should reverse FM debit charge from year 1.
--Look at split commissions
--Remove Under7KInFirstYearAccounts (7K minimum magazine adjusted gross sale last year)

--Groups who just finished Year 1
SELECT		'Year1' YearInProgram,
			campLastYear.ID AS LastYearCampaignID,
			campCurrentYear.ID AS CurrentYearCampaignID,
			acc.ID AS AccountID,
			acc.[Name] AS AccountName,
			fm.FMID,
			fm.FirstName + ' ' + fm.LastName AS FMName,
			accFM.ID AS FMAccountID,
			campFM.ID AS FMCampaignID,
			ROUND(SUM(invSecLastYear.Net_Before_Tax - ISNULL(invSecLastYear.US_Postage_Amount, 0.00)) * 0.03, 2) FMLoyaltyDebit
			--ROUND(SUM(invSecLastYear.Total_Tax_Excluded - ISNULL(invSecLastYear.US_Postage_Amount, 0.00)), 2) BonusMustBeOver7K
INTO		#FMLoyaltyDebit
FROM		QSPCanadaCommon..Campaign campCurrentYear
JOIN		QSPCanadaCommon..CampaignProgram cpCurrentYear
				ON	cpCurrentYear.CampaignID = campCurrentYear.ID
				AND	cpCurrentYear.DeletedTF = 0
LEFT JOIN	QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = campCurrentYear.FMID
JOIN		QSPCanadaCommon..Season seasCurrentYear
				ON	GETDATE() BETWEEN seasCurrentYear.StartDate AND seasCurrentYear.EndDate
				AND	seasCurrentYear.Season IN ('Y')
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = campCurrentYear.BillToAccountID
JOIN		QSPCanadaCommon..Campaign campLastYear
				ON	campLastYear.BillToAccountID = acc.ID
JOIN		QSPCanadaCommon..Season seasLastYear
				ON	DATEADD(YEAR, -1, GETDATE()) BETWEEN seasLastYear.StartDate AND seasLastYear.EndDate
				AND	seasLastYear.Season IN ('Y')
JOIN		QSPCanadaOrderManagement..Batch batchLastYear
				ON	batchLastYear.CampaignID = campLastYear.ID
JOIN		Invoice invLastYear
				ON	invLastYear.Order_ID = batchLastYear.OrderID
JOIN		Invoice_Section invSecLastYear
				ON	invSecLastYear.Invoice_ID = invLastYear.Invoice_ID

LEFT JOIN	(QSPCanadaCommon..Campaign campFM
JOIN		QSPCanadaCommon..CAccount accFM
				ON	accFM.ID = campFM.BillToAccountID
				AND	accFM.CAccountCodeGroup = 'Comm')
				
				ON	campFM.FMID = campCurrentYear.FMID
				AND	campFM.[Status] = 37002

WHERE		cpCurrentYear.ProgramID = 36 --36: Loyalty Bonus Program
AND			campLastYear.StartDate BETWEEN seasLastYear.StartDate AND seasLastYear.EndDate
AND			campCurrentYear.StartDate BETWEEN seasCurrentYear.StartDate AND seasCurrentYear.EndDate
AND			campLastYear.IsStaffOrder = 0
AND			campCurrentYear.Status IN (37002)
AND			invSecLastYear.Section_Type_ID IN (2) --2: Magazine and BHE
--AND			campCurrentYear.ID IN (SELECT CampaignID FROM #CurrentCampaigns)
GROUP BY	campLastYear.ID,
			campCurrentYear.ID,
			acc.ID,
			acc.[Name],
			fm.FMID,
			fm.FirstName + ' ' + fm.LastName,
			accFM.ID,
			campFM.ID
HAVING		SUM(invSecLastYear.Total_Tax_Included) * 0.05 > 10
ORDER BY	fm.FMID,
			acc.Id,
			campCurrentYear.ID

--Groups who just finished Year 2
INSERT INTO	#FMLoyaltyDebit
SELECT		'Year2' YearInProgram,
			campLastYear.ID AS LastYearCampaignID,
			0 CurrentYearCampaignID,
			acc.ID AS AccountID,
			acc.[Name] AS AccountName,
			fm.FMID,
			fm.FirstName + ' ' + fm.LastName AS FMName,
			accFM.ID AS FMAccountID,
			campFM.ID AS FMCampaignID,
			ROUND(SUM(invSecLastYear.Net_Before_Tax - ISNULL(invSecLastYear.US_Postage_Amount, 0.00)) * 0.03, 2) FMLoyaltyDebit
			--ROUND(SUM(invSecLastYear.Total_Tax_Excluded - ISNULL(invSecLastYear.US_Postage_Amount, 0.00)), 2) BonusMustBeOver7K
FROM		QSPCanadaCommon..Campaign campLastYear
JOIN		QSPCanadaCommon..CampaignProgram cpLastYear
				ON	cpLastYear.CampaignID = campLastYear.ID
				AND	cpLastYear.DeletedTF = 0
LEFT JOIN	QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = campLastYear.FMID
JOIN		QSPCanadaCommon..Season seasLastYear
				ON	DATEADD(YEAR, -1, GETDATE()) BETWEEN seasLastYear.StartDate AND seasLastYear.EndDate
				AND	seasLastYear.Season IN ('Y')
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = campLastYear.BillToAccountID
JOIN		QSPCanadaOrderManagement..Batch batchLastYear
				ON	batchLastYear.CampaignID = campLastYear.ID
JOIN		Invoice invLastYear
				ON	invLastYear.Order_ID = batchLastYear.OrderID
JOIN		Invoice_Section invSecLastYear
				ON	invSecLastYear.Invoice_ID = invLastYear.Invoice_ID

LEFT JOIN	(QSPCanadaCommon..Campaign campFM
JOIN		QSPCanadaCommon..CAccount accFM
				ON	accFM.ID = campFM.BillToAccountID
				AND	accFM.CAccountCodeGroup = 'Comm')
				
				ON	campFM.FMID = campLastYear.FMID
				AND	campFM.[Status] = 37002

WHERE		cpLastYear.ProgramID = 36 --36: Loyalty Bonus Program
AND			campLastYear.StartDate BETWEEN seasLastYear.StartDate AND seasLastYear.EndDate
AND			campLastYear.IsStaffOrder = 0
AND			campLastYear.Status IN (37002)
AND			invSecLastYear.Section_Type_ID IN (2) --2: Magazine and BHE
--AND			campLastYear.ID IN (96009,96211,96212,96216,96218,96220) --Temp for FY2015 since only a few groups signed after the new rules
AND			campLastYear.ID NOT IN (99824)
GROUP BY	campLastYear.ID,
			acc.ID,
			acc.[Name],
			fm.FMID,
			fm.FirstName + ' ' + fm.LastName,
			accFM.ID,
			campFM.ID
HAVING		SUM(invSecLastYear.Total_Tax_Included) * 0.05 > 10
ORDER BY	fm.FMID,
			acc.Id,
			campLastYear.ID
			
SELECT		*
FROM		#FMLoyaltyDebit
ORDER BY	FMID, AccountID, CurrentYearCampaignID

--Check if there are any Account transfers (Notre Dame College should be excluded due to this)
SELECT		fmAssigned.FirstName + ' ' + fmAssigned.LastName FMAssignedToCampaign, fmAssigned.FMID FMIDAssignedToCampaign,
			fmNotAssigned.FirstName + ' ' + fmNotAssigned.LastName FMNotAssignedToCampaign, fmNotAssigned.FMID FMIDNotAssignedToCampaign,
			acc.Id AccountID, acc.Name AccountName, c.ID CampaignID, c.IsStaffOrder IsStaffCampaign
FROM		QSPCanadaCommon..Campaign campOld
JOIN		QSPCanadaCommon..Campaign campNew ON campNew.BillToAccountID = campOld.BillToAccountID AND campNew.IsStaffOrder = campold.IsStaffOrder
JOIN		QSPCanadaCommon..FieldManager fmAssigned ON fmAssigned.FMID = campNew.FMID
JOIN		QSPCanadaCommon..FieldManager fmNotAssigned ON fmNotAssigned.FMID = campOld.FMID
JOIN		QSPCanadaCommon..Campaign c ON c.ID = campNew.ID
JOIN		QSPCanadaCommon..CAccount acc ON acc.Id = c.BillToAccountID
JOIN		#FMLoyaltyDebit ld ON ld.AccountID = acc.Id
WHERE		campOld.FMID <> campNew.FMID
AND			campOld.StartDate BETWEEN '2015-07-01' AND '2016-06-30'
AND			campNew.StartDate BETWEEN '2016-07-01' AND '2017-06-30'
AND			acc.BusinessUnitID <> 2
ORDER BY	campOld.FMID, campNew.FMID, campNew.ID

--Check if there are any Commission Splits
SELECT		*
FROM		#FMLoyaltyDebit ld
JOIN		QSPCanadaCommon..CampaignCommissionSplit ccsL ON ccsL.CampaignID = ld.LastYearCampaignID
JOIN		QSPCanadaCommon..CampaignCommissionSplit ccsC ON ccsC.CampaignID = ld.CurrentYearCampaignID
ORDER BY	ld.FMID, AccountID, CurrentYearCampaignID

---------FM Debit Adjustment

DECLARE @FMRunDate			DATETIME,
		@FMRefundID			INT,
		@FMAdjustmentID		INT,
		@FMAccountID		INT,
		@FMAdjustmentTypeID	INT

SET @FMRunDate = GETDATE()
SET	@FMAdjustmentTypeID = 49065 --Loyalty Bonus Debit - FM (*need to create adjustmenttype_glentry mappings)

DECLARE	@FMGLDescriptionText	VARCHAR(50),
		@FMInternalComment		VARCHAR(100),
		@FMGLEntryID			INT,
		@FMCampaignID			INT,
		@FMAmount				DECIMAL(8,2)

BEGIN TRANSACTION

DECLARE		FMDebit CURSOR FOR
SELECT		FMCampaignID,
			FMAccountID,
			SUM(FMLoyaltyDebit) FMAmount
FROM		#FMLoyaltyDebit
GROUP BY	FMCampaignID,
			FMAccountID
ORDER BY	FMCampaignID

OPEN FMDebit
FETCH NEXT FROM FMDebit INTO @FMCampaignID, @FMAccountID, @FMAmount


WHILE @@FETCH_STATUS = 0
BEGIN

	SET @FMInternalComment = 'Loyalty Bonus Debit - FM'
	
	EXEC AddInvoiceAdjustment
		@AccountID = @FMAccountID,
		@OrderID = NULL,
		@InternalComment = @FMInternalComment,
		@Amount = @FMAmount,
		@CampaignID = @FMCampaignID,
		@AdjustmentType = @FMAdjustmentTypeID,
		@ChangedBy = 612,
		@RefundID = null,
		@Value = @FMAdjustmentID OUTPUT 

	FETCH NEXT FROM FMDebit INTO @FMCampaignID, @FMAccountID, @FMAmount

END
CLOSE FMDebit
DEALLOCATE FMDebit

COMMIT

DROP TABLE #FMLoyaltyDebit

select top 99 *
from ADJUSTMENT
order by ADJUSTMENT_ID desc

select top 99 *
from GL_TRANSACTION
order by GL_TRANSACTION_ID desc