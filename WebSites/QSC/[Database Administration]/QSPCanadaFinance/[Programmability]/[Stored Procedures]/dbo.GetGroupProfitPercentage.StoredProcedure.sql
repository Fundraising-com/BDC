USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetGroupProfitPercentage]    Script Date: 06/07/2017 09:17:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetGroupProfitPercentage]
	@OrderID			int,
	@CampaignID 		int,
	@ProductLine		int,
	@ItemCount			int,
	@ProfitPercentage	numeric(10,2) output
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/22/2004 
--   Get Group Profit Percentage For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

IF (@ProductLine = 46018)
BEGIN
	
	DECLARE @OrderQualifierID INT
	SELECT	@OrderQualifierID = b.OrderQualifierID
	FROM	QSPCanadaOrderManagement..Batch b
	WHERE	b.OrderID = @OrderID
	
	--Look at all Cookie Dough orders for the Campaign, as well as Gift Cards as they are included in the Cookie Dough Brochure
	IF @OrderQualifierID IN (39001, 39002, 39009)
	BEGIN
		SELECT	@ItemCount = SUM(cod.Quantity)
		FROM	QSPCanadaOrderManagement..Batch b
		JOIN	QSPCanadaOrderManagement..CustomerOrderHeader coh ON coh.OrderBatchID = b.ID AND coh.OrderBatchDate = b.Date
		JOIN	QSPCanadaOrderManagement..CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = coh.Instance
		WHERE	cod.ProductType IN (46007, 46018)
		AND		b.OrderQualifierID IN (39001, 39002, 39009)
		AND		b.CampaignID = @CampaignID
	END
	
	SELECT  @ProfitPercentage = CASE @OrderQualifierID WHEN 39022 THEN 0.00 
													   ELSE CASE WHEN @ItemCount >= 370 THEN 0.40
																 WHEN @ItemCount BETWEEN 201 AND 369 THEN 0.35
																 WHEN @ItemCount <= 200 THEN 0.30
															END
								END
END
ELSE IF (@ProductLine = 46019) --Popcorn
BEGIN
	SELECT	@ProfitPercentage = convert(numeric(10,2), (convert(numeric(10,2), CP.GroupProfit ) / convert(numeric(10,2), 100 ))  )
	FROM	QSPCanadaCommon..CampaignProgram cp
	WHERE	cp.CampaignID = @CampaignID
	AND		cp.ProgramID IN (61,66) --FFTF Popcorn, PJ Popcorn
END
ELSE IF (@ProductLine = 46024) --Savings Pass
BEGIN
	SELECT	@ProfitPercentage = convert(numeric(10,2), (convert(numeric(10,2), CP.GroupProfit ) / convert(numeric(10,2), 100 ))  )
	FROM	QSPCanadaCommon..CampaignProgram cp
	WHERE	cp.CampaignID = @CampaignID
	AND		cp.ProgramID = 64 --Savings Pass
END
ELSE
BEGIN
	SELECT  @ProfitPercentage = convert(numeric(10,2), (convert(numeric(10,2), CP.GroupProfit ) / convert(numeric(10,2), 100 ))  )
	FROM QSPCanadaOrderManagement..Batch B
		INNER JOIN QSPCanadaCommon..CampaignProgram CP on CP.CampaignID = B.CampaignID
		--INNER JOIN QSPCanadaCommon..Program Prog on CP.ProgramID = Prog.ID
		INNER JOIN QSPCanadaCommon..Program P on P.ID = CP.ProgramID
	WHERE B.OrderID = @OrderID
		AND B.CampaignID = @CampaignID
		AND P.MajorProductLineID = @ProductLine
		AND P.DefaultProfit IS NOT NULL
		AND CP.DeletedTF = 0
END

SET NOCOUNT OFF
GO
