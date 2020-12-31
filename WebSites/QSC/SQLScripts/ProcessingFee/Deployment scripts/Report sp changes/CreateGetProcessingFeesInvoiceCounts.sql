USE [QSPCanadaFinance]
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Charles Rose-Lizee
-- Create date: 2011-07-27
-- Description:	Procedure that counts all the processing fees booked invoiced between @StartDate
--				and @EndDate
-- =============================================
CREATE PROCEDURE GetProcessingFeesInvoiceCounts
	-- Datetime interval for Invoice processing fees added
	@StartDate datetime,
	@EndDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	if (@StartDate is null or @StartDate = '')
		SET @StartDate = '1995-01-01'
	
	if (@EndDate is null or @EndDate = '')
		SET @EndDate = getdate()

	-- Remove time portion of @StartDate
	SET @StartDate = DATEADD(dd, DATEDIFF(dd, 0, @StartDate), 0)

	--Remove time portion of @EndDate, adding 1 day to @EndDate to make sure we include all of the last day in the interval
	SET @EndDate = DATEADD(dd, 1, @EndDate)
	SET @EndDate = DATEADD(dd, DATEDIFF(dd, 0, @EndDate), 0)

	DECLARE @InvoiceCount int
	DECLARE @ProcessingFeeCount int
	DECLARE @ProcessingFeeGrossAmount numeric(10,2)
	DECLARE @ProcessingFeeNetAmount numeric(10,2)
	DECLARE @ProcessingFeeTax1Amount numeric(10,2)
	DECLARE @ProcessingFeeTax2Amount numeric(10,2)
	DECLARE @ProcessingFeeTotalTax numeric(10,2)
	
    -- Get Invoice count for landed orders for the desired period. May not be equal to processing fee count, but should definitely not be smaller
	SELECT 
		@InvoiceCount = count(Invoice.INVOICE_ID)
	FROM 
		INVOICE AS Invoice 
			LEFT JOIN QSPCanadaOrderManagement..Batch AS Batch
				ON Invoice.ORDER_ID = Batch.OrderID
	WHERE 
		Invoice.DATETIME_CREATED >= @StartDate AND 
		Invoice.DATETIME_CREATED < @EndDate AND
		Batch.OrderQualifierID in (39001,39002) --Main, supplement landed orders

	--Get Total processing fee count, amount and total taxes from invoice_Section
	SELECT 
		@ProcessingFeeCount = COUNT(invoice_section.INVOICE_SECTION_ID),
		@ProcessingFeeGrossAmount = SUM(ISNULL(invoice_section.TOTAL_TAX_INCLUDED, 0)),
		@ProcessingFeeNetAmount = SUM(ISNULL(invoice_section.TOTAL_TAX_EXCLUDED, 0)),
		@ProcessingFeeTotalTax = SUM(invoice_section.TOTAL_TAX_AMOUNT)
	FROM
		INVOICE_SECTION invoice_section
	WHERE 
		invoice_section.DateCreated >= @StartDate AND
		invoice_section.DateCreated < @EndDate AND
		invoice_section.Section_Type_ID = 8

	--Get detailed invoice taxes
	SELECT 
		@ProcessingFeeTax1Amount = SUM((CASE WHEN Tax.TAX_ID IN (1,2,4,5,6,10) then Tax.TAX_AMOUNT ELSE 0 END)),
		@ProcessingFeeTax2Amount = SUM((CASE WHEN Tax.TAX_ID IN (3,7,8,9) then Tax.TAX_AMOUNT ELSE 0 END))
	FROM
		INVOICE_SECTION as Invoice_section 
			INNER JOIN INVOICE_SECTION_TAX AS Tax ON 
				Tax.Invoice_Section_ID = Invoice_section.Invoice_section_ID 
	WHERE
		Tax.DateCreated >= @StartDate AND
		Tax.DateCreated < @EndDate  AND
		Invoice_Section.Section_Type_ID = 8

	SELECT  
		@InvoiceCount AS InvoiceCount,
		@ProcessingFeeCount AS ProcessingFeeCount,
		@ProcessingFeeGrossAmount AS ProcessingFeeGrossAmount,
		@ProcessingFeeNetAmount AS ProcessingFeeNetAmount,
		@ProcessingFeeTax1Amount AS ProcessingFeeTax1Amount,
		@ProcessingFeeTax2Amount AS ProcessingFeeTax2Amount,
		@ProcessingFeeTotalTax AS ProcessingFeeTotalTax
		
END
GO
