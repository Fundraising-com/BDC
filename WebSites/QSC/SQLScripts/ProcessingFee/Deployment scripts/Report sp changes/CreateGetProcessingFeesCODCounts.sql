USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [TIME-INC-CORP\crose0033].[GetProcessingFeesCODCounts]    Script Date: 07/28/2011 10:30:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Charles Rose-Lizee
-- Create date: 2011-07-27
-- Description:	Procedure that counts all the processing fees booked as Customer order details between @StartDate
--				and @EndDate
-- =============================================
CREATE PROCEDURE [GetProcessingFeesCODCounts]
	-- Datetime interval for CustomerOrderDetail processing fees added
	@StartDate datetime,
	@EndDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @dStartDate datetime
	DECLARE @dEndDate datetime

	if (ISNULL(@StartDate, '') = '')
		SET @StartDate = '1995-01-01'
	
	if (ISNULL(@EndDate, '') = '')
		SET @EndDate = getdate()

	-- Remove time portion of @StartDate
	SET @dStartDate = DATEADD(dd, DATEDIFF(dd, 0, @StartDate), 0)

	--Remove time portion of @EndDate, adding 1 day to @EndDate to make sure we include all of the last day in the interval
	SET @EndDate = DATEADD(dd, 1, @EndDate)
	SET @dEndDate = DATEADD(dd, DATEDIFF(dd, 0, @EndDate), 0)

	

	DECLARE @LandedOrdersCount int
	DECLARE @ProcessingFeeCount int
	DECLARE @ProcessingFeeGrossAmount numeric(10,2)
	DECLARE @ProcessingFeeNetAmount numeric(10,2)
	DECLARE @ProcessingFeeTax1Amount numeric(10,2)
	DECLARE @ProcessingFeeTax2Amount numeric(10,2)
	DECLARE @ProcessingFeeTotalTax numeric(10,2)
	DECLARE @InvoicedLandedOrdersCount int
	DECLARE @InvoicedProcessingFeeCount int
	DECLARE @InvoicedProcessingFeeGrossAmount numeric(10,2)
	DECLARE @InvoicedProcessingFeeNetAmount numeric(10,2)
	DECLARE @InvoicedProcessingFeeTax1Amount numeric(10,2)
	DECLARE @InvoicedProcessingFeeTax2Amount numeric(10,2)
	DECLARE @InvoicedProcessingFeeTotalTax numeric(10,2)
	
    -- Get LandedOrders count for the desired period. May not be equal to processing fee count, but should definitely not be smaller
	SELECT 
		@LandedOrdersCount = count(COH.Instance)
	FROM 
		CustomerOrderHeader COH
			INNER JOIN Batch as Batch ON
				COH.OrderBatchID = Batch.ID AND
				COH.OrderBatchDate = Batch.Date
	WHERE
		Batch.OrderQualifierID IN (39001,39002) AND --Main or supplement, landed orders
		COH.CreationDate >= @dStartDate AND 
		COH.CreationDate < @dEndDate

	--Get Total processing fee count, amount and taxes, invoiced or not
	SELECT 
		@ProcessingFeeCount = COUNT(TransID),
		@ProcessingFeeGrossAmount = SUM(ISNULL(Gross, 0)),
		@ProcessingFeeNetAmount = SUM(ISNULL(Net, 0)),
		@ProcessingFeeTax1Amount = SUM(ISNULL(Tax, 0)),
		@ProcessingFeeTax2Amount = SUM(ISNULL(Tax2, 0)),
		@ProcessingFeeTotalTax = SUM(ISNULL(Tax, 0)+ISNULL(Tax2, 0))
	FROM
		dbo.CustomerOrderDetail
	WHERE 
		CreationDate >= @dStartDate AND
		CreationDate < @dEndDate AND
		ProductCode = 'PFEE'

	--Get Invoiced landed orders count
	SELECT 
		@InvoicedLandedOrdersCount = count(DISTINCT COH.Instance)
	FROM 
		CustomerOrderHeader COH
			INNER JOIN Batch as Batch ON
				COH.OrderBatchID = Batch.ID AND
				COH.OrderBatchDate = Batch.Date
			INNER JOIN CustomerOrderDetail COD ON
				COD.CustomerOrderHeaderInstance = COH.Instance
	WHERE
		Batch.OrderQualifierID IN (39001,39002) AND --Main or supplement, landed orders
		COH.CreationDate >= @dStartDate AND 
		COH.CreationDate < @dEndDate AND
		ISNULL(COD.InvoiceNumber, 0) <> 0

	--Get Invoice processing fee count, amount and taxes, based on COD
	SELECT 
		@InvoicedProcessingFeeCount = COUNT(TransID),
		@InvoicedProcessingFeeGrossAmount = SUM(ISNULL(Gross, 0)),
		@InvoicedProcessingFeeNetAmount = SUM(ISNULL(Net, 0)),
		@InvoicedProcessingFeeTax1Amount = SUM(ISNULL(Tax, 0)),
		@InvoicedProcessingFeeTax2Amount = SUM(ISNULL(Tax2, 0)),
		@InvoicedProcessingFeeTotalTax = SUM(ISNULL(Tax, 0)+ISNULL(Tax2, 0))
	FROM
		dbo.CustomerOrderDetail
	WHERE 
		CreationDate >= @dStartDate AND
		CreationDate < @dEndDate AND
		ProductCode = 'PFEE' AND
		ISNULL(InvoiceNumber, 0) <> 0

	SELECT  
		@LandedOrdersCount AS LandedOrdersCount,
		@ProcessingFeeCount AS ProcessingFeeCount,
		@InvoicedProcessingFeeCount AS InvoicedProcessingFeeCount,
		@ProcessingFeeGrossAmount AS ProcessingFeeGrossAmount,
		@ProcessingFeeNetAmount AS ProcessingFeeNetAmount,
		@ProcessingFeeTax1Amount AS ProcessingFeeTax1Amount,
		@ProcessingFeeTax2Amount AS ProcessingFeeTax2Amount,
		@ProcessingFeeTotalTax AS ProcessingFeeTotalTax,
		@InvoicedLandedOrdersCount as InvoicedLandedOrdersCount,
		@InvoicedProcessingFeeGrossAmount AS InvoicedProcessingFeeGrossAmount,
		@InvoicedProcessingFeeNetAmount AS InvoicedProcessingFeeNetAmount,
		@InvoicedProcessingFeeTax1Amount AS InvoicedProcessingFeeTax1Amount,
		@InvoicedProcessingFeeTax2Amount AS InvoicedProcessingFeeTax2Amount,
		@InvoicedProcessingFeeTotalTax AS InvoicedProcessingFeeTotalTax
END
