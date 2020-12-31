USE QSPCanadaFinance

/*CREATE TABLE #CurrentCampaigns
(
	CampaignID	INT
)

INSERT INTO #CurrentCampaigns VALUES(	66147 	)
INSERT INTO #CurrentCampaigns VALUES(	66004 	)
INSERT INTO #CurrentCampaigns VALUES(	66018 	)
INSERT INTO #CurrentCampaigns VALUES(	63539 	)

SELECT * FROM #CurrentCampaigns
*/
SELECT		campLastYear.ID AS LastYearCampaignID,
			campCurrentYear.ID AS CurrentYearCampaignID,
			acc.ID AS AccountID,
			ROUND(SUM(invSecLastYear.Total_Tax_Excluded - ISNULL(invSecLastYear.US_Postage_Amount, 0.00)) * 0.05, 2) AS LoyaltyBonus,
			ISNULL(cont.FirstName, '') + ' ' + ISNULL(cont.Lastname, '') AS ContactName,
			acc.[Name] AS AccountName,
			adBill.Street1 AS BillingAddress,
			adBill.Street2 AS BillingAddress2,
			adBill.City AS BillingCity,
			adBill.StateProvince AS BillingProvince,
			adBill.Postal_Code AS BillingZip,
			fm.FMID,
			fm.FirstName + ' ' + fm.LastName AS FMName
INTO		#LoyaltyBonus
FROM		QSPCanadaCommon..Campaign campCurrentYear
JOIN		QSPCanadaCommon..CampaignProgram cpCurrentYear
				ON	cpCurrentYear.CampaignID = campCurrentYear.ID
				AND	cpCurrentYear.DeletedTF = 0
LEFT JOIN	QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = campCurrentYear.FMID
JOIN		QSPCanadaCommon..Season seasCurrentYear
				ON	GETDATE() BETWEEN seasCurrentYear.StartDate AND seasCurrentYear.EndDate
				AND	seasCurrentYear.Season IN ('F')
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = campCurrentYear.BillToAccountID
LEFT JOIN	QSPCanadaCommon..Address adBill   
				ON	adBill.AddressListID = acc.AddressListID
				AND	adBill.Address_Type = 54002
LEFT JOIN	QSPCanadaCommon..Contact cont
				ON	cont.ID = campCurrentYear.BillToCampaignContactID
JOIN		QSPCanadaCommon..Campaign campLastYear
				ON	campLastYear.BillToAccountID = acc.ID
JOIN		QSPCanadaCommon..Season seasLastYear
				ON	DATEADD(YEAR, -1, GETDATE()) BETWEEN seasLastYear.StartDate AND seasLastYear.EndDate
				AND	seasLastYear.Season IN ('F')
JOIN		QSPCanadaOrderManagement..Batch batchLastYear
				ON	batchLastYear.CampaignID = campLastYear.ID
JOIN		Invoice invLastYear
				ON	invLastYear.Order_ID = batchLastYear.OrderID
JOIN		Invoice_Section invSecLastYear
				ON	invSecLastYear.Invoice_ID = invLastYear.Invoice_ID
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
			ISNULL(cont.FirstName, '') + ' ' + ISNULL(cont.Lastname, ''),
			acc.[Name],
			adBill.Street1,
			adBill.Street2,
			adBill.City,
			adBill.StateProvince,
			adBill.Postal_Code,
			fm.FMID,
			fm.FirstName + ' ' + fm.LastName
HAVING		SUM(invSecLastYear.Total_Tax_Included) * 0.05 > 10
ORDER BY	fm.FMID,
			campCurrentYear.ID

--Exceptions for Mentor (include US Postage and Processing Fees)
UPDATE		#LoyaltyBonus
SET			LoyaltyBonus = '61.3700'
WHERE		AccountID IN (31909)

UPDATE		#LoyaltyBonus
SET			LoyaltyBonus = '523.3600'
WHERE		AccountID IN (17442)

UPDATE		#LoyaltyBonus
SET			LoyaltyBonus = '1179.0100'
WHERE		AccountID IN (17443)

UPDATE		#LoyaltyBonus
SET			LoyaltyBonus = '1195.4000'
WHERE		AccountID IN (17444)


SELECT		*
FROM		#LoyaltyBonus
ORDER BY	AccountID


--Ensure ACCOUNT has over 7K (magazine adjusted gross) or they don't qualify (for Fall 2013 and on)
SELECT		lb.AccountID AS AccountID,
			lb.AccountName,
			ROUND(SUM(invSecLastYear.Total_Tax_Excluded - ISNULL(invSecLastYear.US_Postage_Amount, 0.00)), 2) AS AccountMagazineAdjustedGrossSalesLastYear,
			fm.FirstName + ' ' + fm.LastName AS FMName
FROM		#LoyaltyBonus lb
JOIN		QSPCanadaCommon..Campaign campLastYear
				ON	campLastYear.BillToAccountID = lb.AccountID
LEFT JOIN	QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = campLastYear.FMID
JOIN		QSPCanadaCommon..Season seasLastYear
				ON	DATEADD(YEAR, -1, GETDATE()) BETWEEN seasLastYear.StartDate AND seasLastYear.EndDate
				AND	seasLastYear.Season IN ('F')
JOIN		QSPCanadaOrderManagement..Batch batchLastYear
				ON	batchLastYear.CampaignID = campLastYear.ID
JOIN		Invoice invLastYear
				ON	invLastYear.Order_ID = batchLastYear.OrderID
JOIN		Invoice_Section invSecLastYear
				ON	invSecLastYear.Invoice_ID = invLastYear.Invoice_ID
WHERE		campLastYear.StartDate BETWEEN seasLastYear.StartDate AND seasLastYear.EndDate
AND			campLastYear.IsStaffOrder = 0
AND			invSecLastYear.Section_Type_ID IN (2) --2: Magazine and BHE
GROUP BY	lb.AccountID,
			lb.AccountName,
			fm.FirstName + ' ' + fm.LastName
HAVING		ROUND(SUM(invSecLastYear.Total_Tax_Excluded - ISNULL(invSecLastYear.US_Postage_Amount, 0.00)), 2) < 7000
ORDER BY	fm.FirstName + ' ' + fm.LastName,
			lb.AccountID
			
---------School Credit Adjustment

DECLARE @RunDate			DATETIME,
		@RefundType			INT,
		@RefundID			INT,
		@DoAsAdjustment		BIT,
		@AdjustmentID		INT,
		@AccountID			INT,
		@AdjustmentTypeID	INT

SET @RunDate = GETDATE()
SET	@RefundType = 2
--SET	@AdjustmentTypeID = 49035 --2 Year Loyalty Bonus - Standalone
SET	@AdjustmentTypeID = 49046 --2 Year Loyalty Bonus Credit

DECLARE	@GLDescriptionText		VARCHAR(50),
		@InternalComment		VARCHAR(100),
		@GLEntryID				INT,
		@CurrentYearCampaignID	INT,
		@Amount					DECIMAL(8,2),
		@ContactName			VARCHAR(100),
		@BillingAddress			VARCHAR(200),
		@AccountName			VARCHAR(200),
		@BillingAddress2		VARCHAR(200),
		@BillingCity			VARCHAR(200),
		@BillingProvince		VARCHAR(10),
		@BillingZip				VARCHAR(100)

BEGIN TRANSACTION

DECLARE		Bonus CURSOR FOR
SELECT		CurrentYearCampaignID,
			AccountID,
			SUM(LoyaltyBonus),
			ContactName,
			AccountName,
			BillingAddress,
			BillingAddress2,
			BillingCity,
			BillingProvince,
			BillingZip
FROM		#LoyaltyBonus
GROUP BY	CurrentYearCampaignID,
			AccountID,
			ContactName,
			AccountName,
			BillingAddress,
			BillingAddress2,
			BillingCity,
			BillingProvince,
			BillingZip
ORDER BY	CurrentYearCampaignID

OPEN Bonus
FETCH NEXT FROM Bonus INTO @CurrentYearCampaignID, @AccountID, @Amount, @ContactName, @AccountName, @BillingAddress,
						@BillingAddress2, @BillingCity, @BillingProvince, @BillingZip


WHILE @@FETCH_STATUS = 0
BEGIN

	SET @InternalComment = 'Loyalty Bonus Credit - CampaignID ' + CONVERT(VARCHAR(10), @CurrentYearCampaignID)
	
	EXEC AddInvoiceAdjustment
		@AccountID = @AccountID,
		@OrderID = NULL,
		@InternalComment = @InternalComment,
		@Amount = @Amount,
		@CampaignID = @CurrentYearCampaignID,
		@AdjustmentType = @AdjustmentTypeID,
		@ChangedBy = 612,
		@RefundID = null,
		@Value = @AdjustmentID OUTPUT 

	FETCH NEXT FROM Bonus INTO @CurrentYearCampaignID, @AccountID, @Amount, @ContactName, @AccountName, @BillingAddress,
							@BillingAddress2, @BillingCity, @BillingProvince, @BillingZip

END
CLOSE Bonus
DEALLOCATE Bonus

COMMIT


select top 999 *
from ADJUSTMENT
order by ADJUSTMENT_ID desc

select top 99 *
from GL_TRANSACTION
order by GL_TRANSACTION_ID desc