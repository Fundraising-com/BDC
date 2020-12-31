USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Season_ValidateDelete]    Script Date: 06/07/2017 09:33:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Season_ValidateDelete] 

@iID int

AS

-- get variables necessary for validation
DECLARE @iFiscalYear int, @sSeason char(1), @dtStartDate datetime, @dtEndDate datetime
SELECT 
	@iFiscalYear = [FiscalYear], 
	@sSeason = [Season],
	@dtStartDate = [StartDate],
	@dtEndDate = [EndDate]
FROM 	[Season] 
WHERE 	[ID] = @iID

-- validate
IF EXISTS 
	(SELECT 1 FROM QSPCanadaProduct..Program_Master WHERE Season = @iID)
OR EXISTS
	(SELECT 1 FROM QSPCanadaProduct..Pricing_Details 
	WHERE 	[Pricing_Year] = @iFiscalYear AND [Pricing_Season] = @sSeason)
OR EXISTS
	(SELECT 1 FROM QSPCanadaProduct..Product 
	WHERE 	[Product_Year] = @iFiscalYear AND [Product_Season] = @sSeason)
OR EXISTS
	(SELECT 1 FROM QSPCanadaProduct..Premium
	WHERE [Year] = @iFiscalYear AND [Season] = @sSeason)
OR EXISTS
	(SELECT 1 FROM Campaign
	WHERE 	[StartDate] 	<= @dtEndDate
	AND	[EndDate] 	>= @dtStartDate
	AND status = 37002)

	SELECT 1
ELSE
	SELECT 0
GO
