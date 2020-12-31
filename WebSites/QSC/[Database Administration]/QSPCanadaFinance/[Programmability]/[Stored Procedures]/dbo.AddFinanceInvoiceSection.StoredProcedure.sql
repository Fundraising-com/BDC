USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddFinanceInvoiceSection]    Script Date: 06/07/2017 09:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddFinanceInvoiceSection]
	@InvoiceID		int,
	@SectionTypeID		int,
	@TotalTaxIncluded	numeric(10,2),
	@TotalTaxExcluded	numeric(10,2),
	@GroupProfitRate	numeric(10,6),
	@GroupProfitAmount	numeric(10,2),
	@ThirdPartyProfitRate	numeric(10,6),
	@ThirdPartyProfitAmount	numeric(10,2),
	@Tax			numeric(10,2),
	@USPostageAmount numeric(10,2),
	@ItemCount		int,
	@UserID		int,
	@ProgramType int,
	@InvoiceSectionID	int output,
	@TotalTaxableAmount	numeric(10,2) output,
	@DueAmount		numeric(10,2) output
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/3/2004 
--   Insert an Invoice Section Record with Tax, Profit and Total Amounts For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

DECLARE @Amount numeric(10,2)

IF @SectionTypeID = 1 --Gift
	BEGIN
		SET @Amount = @TotalTaxIncluded-@GroupProfitAmount-@ThirdPartyProfitAmount-@Tax
	END
ELSE IF @SectionTypeID = 2 --mag
	BEGIN
		SET @Amount = @TotalTaxIncluded-@Tax
	END	
ELSE IF @SectionTypeID = 20 --Jewelry
	BEGIN
		SET @Amount = @TotalTaxIncluded-@GroupProfitAmount-@ThirdPartyProfitAmount-@Tax
	END	
ELSE --default
	BEGIN
		SET @Amount = @TotalTaxExcluded
	END

INSERT QSPCanadaFinance..Invoice_Section
			(INVOICE_ID,
			SECTION_TYPE_ID,
			TOTAL_TAX_INCLUDED,
			TOTAL_TAX_EXCLUDED,
			GROUP_PROFIT_RATE,
			GROUP_PROFIT_AMOUNT,
			TOTAL_TAXABLE_AMOUNT,
			NET_BEFORE_TAX,
			TOTAL_TAX_AMOUNT,
			DUE_AMOUNT,
			ITEM_COUNT,
			DateCreated,
			CreatedBy,
			US_Postage_Amount,
			ProgramType,
			THIRDPARTY_PROFIT_RATE,
			THIRDPARTY_PROFIT_AMOUNT
			)
VALUES(@InvoiceID,
	@SectionTypeID,
	@TotalTaxIncluded,
	@TotalTaxExcluded,
	@GroupProfitRate*100, 	--Group Profit Rate
	@GroupProfitAmount,			--Group Profit Amount
	@Amount	,			--Total Taxable Amount
	@TotalTaxExcluded-@GroupProfitAmount-@ThirdPartyProfitAmount, --Net Before Tax
	@Tax,					--Tax
	@TotalTaxIncluded-@GroupProfitAmount, --Amount Due and now includes any USD postage
	@ItemCount,
	GETDATE(),
	@UserID	,
	@USPostageAmount,
	@ProgramType,
	@ThirdPartyProfitRate*100,
	@ThirdPartyProfitAmount
	)

SELECT @InvoiceSectionID = scope_identity(), 
	@TotalTaxableAmount = @Amount, 
	@DueAmount = (@TotalTaxIncluded-@GroupProfitAmount)

SET NOCOUNT OFF
GO
