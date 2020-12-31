USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_GetCurrentSeason]    Script Date: 06/07/2017 09:20:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_RemitTest_GetCurrentSeason]

@ProductSeason 	char(1) output,
@ProductYear	int	output

AS


/*********************** get current season ******************************/
IF (MONTH(GETDATE()) >= 2 AND MONTH(GETDATE()) < 8)
	SET @ProductSeason = 'F'--'S'--Magazine brochure spans full year, but the offers are still set to Fall which isn't really accurate
ELSE
	SET @ProductSeason = 'F'

IF (MONTH(GETDATE()) >= 8)
	SET @ProductYear = YEAR(GETDATE()) + 1 
ELSE
	SET @ProductYear = YEAR(GETDATE())
/*************************************************************************/
GO
