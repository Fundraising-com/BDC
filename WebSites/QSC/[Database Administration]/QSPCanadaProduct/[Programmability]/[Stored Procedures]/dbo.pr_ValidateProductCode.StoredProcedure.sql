USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ValidateProductCode]    Script Date: 06/07/2017 09:18:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ValidateProductCode]

	@zProductCode	varchar(20),
	@zSeason	varchar(1),
	@iYear		int

AS

	DECLARE @iCount	int
	DECLARE @iIsValid	int

	SELECT	@iCount = COUNT(p.Product_Code)
	FROM		Product p
	WHERE	p.Product_Code = @zProductCode
	AND		p.Product_Season = @zSeason
	AND		p.Product_Year = @iYear

	IF(@iCount = 0)
		SET @iIsValid = 1
	ELSE
		SET @iIsValid = 0

SELECT @iIsValid
GO
