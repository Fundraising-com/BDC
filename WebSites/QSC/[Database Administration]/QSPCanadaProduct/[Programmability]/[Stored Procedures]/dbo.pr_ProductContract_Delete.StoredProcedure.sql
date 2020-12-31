USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProductContract_Delete]    Script Date: 06/07/2017 09:17:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ProductContract_Delete]

	@zUMC		varchar(4),
	@zSeason		varchar(1),
	@iYear			int,
	@iNumberOfIssues	int

AS

UPDATE	Pricing_Details
SET		Status = 30601
WHERE	Product_Code = @zUMC
AND		Pricing_Season = @zSeason
AND		Pricing_Year = @iYear
AND		Nbr_of_Issues = @iNumberOfIssues
GO
