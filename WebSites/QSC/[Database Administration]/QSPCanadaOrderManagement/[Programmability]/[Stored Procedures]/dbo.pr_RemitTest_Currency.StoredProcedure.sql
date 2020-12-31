USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Currency]    Script Date: 06/07/2017 09:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pr_RemitTest_Currency]

@iRunID		int = 0

AS


/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

IF EXISTS
(
	SELECT		p.Currency,
			codrh.*
	FROM		CustomerOrderDetailRemitHistory codrh,
			RemitBatch rb,
			QSPCanadaProduct..Product p
	WHERE		rb.ID = codrh.RemitBatchID
	AND		p.Product_Code = codrh.TitleCode
	AND		p.Product_Year = @ProductYear
	AND		p.Product_Season = @ProductSeason
	AND		p.Currency <> codrh.CurrencyID
	AND		rb.RunID = @iRunID
)
	SELECT 1
ELSE
	SELECT 0
GO
