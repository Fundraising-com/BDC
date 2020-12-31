USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllInvoicesToPrint]    Script Date: 06/07/2017 09:17:13 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[GetAllInvoicesToPrint]
	@FiscalYear			INT = NULL,
	@AcctName			varchar(50) = '',
	@InvoiceID			int = 0,
	@AccountID			int = 0,
	@CampaignID			int = 0,
	@IsPrinted			char(10) = '',
	@FMID				varchar(4) = '9999', 
	@ShowInOwingOnly	bit = 0,
	@ShowNonPrinted		bit = 0
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  MTC 5/20/2004 
--  Get Invoices ready to print For Canada Finance System
--  MTC 10/5/2004
--  Filter out Internet orders here - they are created but not invoiced
--  MTC 12/10/2004
--  Filtering out cust service and magnet orders
--	Jeff Miles October 2006
--	Allow querying based on FM
--	Juan Martinez January 2010
--	Added filter to show in owing only or all accounts
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON



IF ISNULL(@AcctName, '') = ''
BEGIN
	SET @AcctName = NULL
END

IF ISNULL(@InvoiceID, 0) = 0
BEGIN
	SET @InvoiceID = NULL
END

IF ISNULL(@AccountID, 0) = 0
BEGIN
	SET @AccountID = NULL
END

IF ISNULL(@CampaignID, 0) = 0
BEGIN
	SET @CampaignID = NULL
END

IF ISNULL(@IsPrinted, '') = ''
BEGIN
	SET @IsPrinted = NULL
END

IF ISNULL(@FMID, '9999') = '9999'
BEGIN
	SET @FMID = NULL
END



---------------------------
-- Get dates from fiscal year --
---------------------------

DECLARE @SeasonStartDate	DATETIME,
		@SeasonEndDate		DATETIME

IF ISNULL(@FiscalYear, 0) > 0
BEGIN
	SELECT	@SeasonStartDate = StartDate,
			@SeasonEndDate = DATEADD(dd, 1, EndDate)
	FROM	QSPCanadaCommon..Season
	WHERE	FiscalYear = @FiscalYear
	AND		Season IN ('Y')
END

IF @SeasonStartDate = NULL
	BEGIN
		SET @SeasonStartDate = '1900-01-01'
	END

IF @SeasonEndDate = NULL
	BEGIN
		SET @SeasonEndDate = '2100-01-01'
	END

---------------------------

CREATE TABLE #CampaignBalance
(
	CampaignID		INT,
	CampaignBalance	NUMERIC(12, 2)
)

DECLARE @Date DATETIME
SET @Date = GETDATE()

INSERT		#CampaignBalance
SELECT		sdCamp.CampaignID,
			ISNULL(SUM(sdCamp.TransactionAmount), 0)
FROM		dbo.UDF_Statement_GetDetails_WithBusLogic(@Date) sdCamp
GROUP BY	sdCamp.CampaignID


---------------------------
-- Invoice list --
---------------------------

DECLARE @INVOICES TABLE
(
	Invoice_ID INT, 
	AccountType VARCHAR(64), 
	Group_Name VARCHAR(1000), 
	CampaignID INT NULL, 
	Account_ID INT, 
	Order_ID INT NULL, 
	Invoice_Date DATETIME NULL, 
	Invoice_Due_Date DATETIME NULL, 
	Invoice_Amount NUMERIC(10, 2) NULL, 
	Is_Printed CHAR(10) NULL, 
	DateTime_Approved DATETIME NULL, 
	Lang VARCHAR(10), 
	[Type] VARCHAR(10),
	CampaignBalance NUMERIC(12, 2) NULL
)

INSERT INTO @INVOICES
	SELECT	Invoice_ID,
			cd.Description AS AccountType,
			[Name] AS Group_Name,
			b.CampaignID,
			acc.ID AS Account_ID,
			inv.Order_ID,
			inv.Invoice_Date,
			inv.Invoice_Due_Date,
			inv.Invoice_Amount,
			inv.Is_Printed,
			inv.DateTime_Approved,
			camp.Lang,
			CASE WHEN ( SELECT	COUNT(*)
						FROM	QSPCanadaOrderManagement..Batch b
								JOIN	QSPCanadaCommon..CampaignProgram cp ON	cp.CampaignID = b.CampaignID
								JOIN	QSPCanadaCommon..Program prog ON	prog.ID = cp.ProgramID
						WHERE	b.OrderID = inv.Order_ID 
								AND		prog.ID = 1) > 0 THEN 'Mag Only'
				WHEN (	SELECT	COUNT(*)
						FROM	QSPCanadaOrderManagement..Batch b
								JOIN	QSPCanadaCommon..CampaignProgram cp ON	cp.CampaignID = b.CampaignID
								JOIN	QSPCanadaCommon..Program prog ON	prog.ID = cp.ProgramID
						WHERE	b.OrderID = inv.Order_ID 
								AND		prog.ID = 2) > 0 THEN 'Mag Only'
				WHEN (	SELECT	COUNT(*)
						FROM	QSPCanadaOrderManagement..Batch b
								JOIN	QSPCanadaCommon..CampaignProgram cp ON	cp.CampaignID = b.CampaignID
								JOIN	QSPCanadaCommon..Program prog ON	prog.ID = cp.ProgramID
						WHERE	b.OrderID = inv.Order_ID 
								AND		prog.ID = 4) > 0 THEN 'Combo' 
				WHEN (	SELECT COUNT(*)
						FROM	QSPCanadaOrderManagement..Batch b
								JOIN	QSPCanadaCommon..CampaignProgram cp ON	cp.CampaignID = b.CampaignID
								JOIN	QSPCanadaCommon..Program prog ON	prog.ID = cp.ProgramID
						WHERE	b.OrderID = inv.Order_ID 
								AND		prog.ID = 20) > 0 THEN 'Combo' 
				WHEN (	SELECT COUNT(*)
						FROM	QSPCanadaOrderManagement..Batch b
								JOIN	QSPCanadaCommon..CampaignProgram cp ON	cp.CampaignID = b.CampaignID
								JOIN	QSPCanadaCommon..Program prog ON	prog.ID = cp.ProgramID
						WHERE	b.OrderID = inv.Order_ID 
								AND	prog.ID = 21) > 0 THEN 'Combo' 
					ELSE ''	
					END AS [Type],
				cb.CampaignBalance
	FROM		Invoice inv
				LEFT JOIN	QSPCanadaCommon..CAccount acc
					ON	acc.ID = inv.Account_ID
				LEFT JOIN	QSPCanadaOrderManagement..Batch b
					ON	b.OrderID = inv.Order_ID
				LEFT JOIN	QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
				LEFT JOIN	QSPCanadaCommon..CodeDetail cd
					ON	cd.Instance = inv.Account_Type_ID
				LEFT JOIN	#CampaignBalance cb
					ON	cb.CampaignID = camp.ID
	WHERE		inv.Invoice_Date BETWEEN @SeasonStartDate AND @SeasonEndDate
				AND		(@ShowNonPrinted = 1 OR b.OrderQualifierID NOT IN (39009, 39013, 39015)) --39009: Internet 39013: Credit Card Reprocess 39015: CC Reprocessed to invoice
				AND		OrderTypeCode <> 41009 --Exclude Magnet
				AND		OrderQualifierID <> 39008
				AND		acc.Name = ISNULL(@AcctName, acc.Name)
				AND		inv.Invoice_ID = ISNULL(@InvoiceID, inv.Invoice_ID)
				AND		inv.Account_ID = ISNULL(@AccountID, inv.Account_ID)
				AND		b.CampaignID = ISNULL(@CampaignID, b.CampaignID)
				AND		inv.Is_Printed = ISNULL(@IsPrinted, inv.Is_Printed)
				AND		camp.FMID = ISNULL(@FMID, camp.FMID)
	--ORDER BY	inv.Invoice_Date

---------------------------



---------------------------
-- Do final join if needed--
---------------------------

IF @ShowInOwingOnly = 1
	BEGIN
		---------------------------
		-- Invoice and payments -- 
		---------------------------
		CREATE TABLE #PAYMENTS
		(
			InvoiceId INT, 
			PaymentAmount NUMERIC (10, 2)
		)
		
		create index ix_invoiceid on #PAYMENTS (InvoiceId)

		INSERT INTO #PAYMENTS
		SELECT		InvoiceId, 
					SUM(PaymentAmount) AS PaymentAmount
		FROM		UDIF_GetInvoicePaymentTotals()
		WHERE		CampaignStartDate >= @SeasonStartDate
					AND CampaignEndDate <= @SeasonEndDate
		GROUP BY	InvoiceId
		---------------------------
		
		SELECT	i.Invoice_ID, 
				i.AccountType, 
				i.Group_Name, 
				i.CampaignID, 
				i.Account_ID, 
				i.Order_ID, 
				i.Invoice_Date, 
				i.Invoice_Due_Date, 
				i.Invoice_Amount, 
				i.Is_Printed, 
				i.DateTime_Approved, 
				i.Lang, 
				i.Type,
				i.CampaignBalance,
				p.PaymentAmount
		FROM	@INVOICES i
				LEFT JOIN #PAYMENTS p ON i.Invoice_ID = p.InvoiceId
		WHERE	(i.Invoice_Amount - ISNULL(p.PaymentAmount, 0)) > 5
		ORDER BY	i.CampaignBalance
	END
ELSE
	BEGIN
		SELECT		*
		FROM		@INVOICES
		ORDER BY	CampaignBalance
	END

SET NOCOUNT OFF
GO
