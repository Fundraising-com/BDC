USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_ProductUnremittable]    Script Date: 06/07/2017 09:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_ProductUnremittable]

@iRunID		int = 0

AS

/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

IF EXISTS
(
	SELECT	codrh.*
	FROM	CustomerOrderDetailRemitHistory codrh,
			QSPCanadaProduct..Product p,
			RemitBatch rb
	WHERE	p.Product_Code = codrh.TitleCode
	AND		p.Product_Year = @ProductYear
	AND		p.Product_Season = @ProductSeason
	AND		rb.ID = codrh.RemitBatchID
	AND		p.Status = 30603 --Magazine Unremittable
	AND		rb.RunID = @iRunID
)
	SELECT 1
ELSE
	SELECT 0
GO
