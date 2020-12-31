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
-- Description:	Procedure that counts all the processing fees posted to GL between @StartDate
--				and @EndDate
-- =============================================
CREATE PROCEDURE GetProcessingFeesGLCounts
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

	--Change this id for the actual GL account id for processing fees
	DECLARE @ProcessingFeeGLAccountID int
	SET @ProcessingFeeGLAccountID = 210

	DECLARE @GLEntryCount int
	DECLARE @ProcessingFeeCount int
	DECLARE @ProcessingFeeGrossAmount numeric(10,2)
	DECLARE @ProcessingFeeNetAmount numeric(10,2)
	DECLARE @ProcessingFeeTax1Amount numeric(10,2)
	DECLARE @ProcessingFeeTax2Amount numeric(10,2)
	DECLARE @ProcessingFeeTotalTax numeric(10,2)
	
    -- Get Invoice count for landed orders for the desired period. May not be equal to processing fee count, but should definitely not be smaller
	SELECT 
		@GLEntryCount = count(GL_Entry.GL_ENTRY_ID)
	FROM 
		GL_Entry AS GL_Entry 
			INNER JOIN Invoice AS Invoice
				ON GL_Entry.Invoice_ID = Invoice.Invoice_ID
			INNER JOIN QSPCanadaOrderManagement..Batch AS Batch
				ON Invoice.ORDER_ID = Batch.OrderID
	WHERE 
		GL_Entry.GL_ENTRY_Date >= @StartDate AND 
		GL_Entry.GL_ENTRY_Date < @EndDate AND
		Batch.OrderQualifierID in (39001,39002) --Main, supplement landed orders

	--Get Total processing fee count and  amount. Taxes cannot be extracted from GL as they are now merged with the other taxes from the same invoice
	--We can however extract taxes from Invoice_BY_QSP_PRODUCT, which is used to generate GL transactions
	SELECT 
		@ProcessingFeeCount = COUNT(GL_Transaction.GL_Transaction_ID),
		@ProcessingFeeNetAmount = SUM(GL_Transaction.Amount)
	FROM
		GL_TRANSACTION 
			INNER JOIN GL_ENTRY ON GL_Transaction.GL_Entry_ID = GL_Entry.GL_Entry_ID 
	WHERE 
		GL_TRANSACTION.GLAccountID = @ProcessingFeeGLAccountID AND 
		GL_Entry.GL_ENTRY_Date >= @StartDate AND 
		GL_Entry.GL_ENTRY_Date < @EndDate
		

	--Get detailed invoice taxes from INVOICE_BY_QSP_PRODUCT
	SELECT 
		@ProcessingFeeTax1Amount = SUM(invoiceByProduct.ProductLine_Tax1),
		@ProcessingFeeTax2Amount = SUM(invoiceByProduct.ProductLine_Tax2),
		@ProcessingFeeTotalTax = SUM(invoiceByProduct.ProductLine_Tax1 + invoiceByProduct.ProductLine_Tax2)
	FROM
		INVOICE_BY_QSP_PRODUCT invoiceByProduct
			INNER JOIN INVOICE ON invoiceByProduct.INVOICE_ID = INVOICE.INVOICE_ID
	WHERE
		Invoice.DATETIME_CREATED >= @StartDate AND 
		Invoice.DATETIME_CREATED < @EndDate AND 
		invoiceByProduct.QSP_PRODUCT_LINE_ID = 46017

	
	SET @ProcessingFeeGrossAmount = @ProcessingFeeNetAmount + @ProcessingFeeTotalTax
	SELECT  
		@GLEntryCount AS GLEntryCount,
		@ProcessingFeeCount AS ProcessingFeeCount,
		@ProcessingFeeGrossAmount AS ProcessingFeeGrossAmount,
		@ProcessingFeeNetAmount AS ProcessingFeeNetAmount,
		@ProcessingFeeTax1Amount AS ProcessingFeeTax1Amount,
		@ProcessingFeeTax2Amount AS ProcessingFeeTax2Amount,
		@ProcessingFeeTotalTax AS ProcessingFeeTotalTax
		
END
GO
