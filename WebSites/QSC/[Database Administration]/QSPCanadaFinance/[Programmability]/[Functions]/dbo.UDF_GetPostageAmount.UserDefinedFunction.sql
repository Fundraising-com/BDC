USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetPostageAmount]    Script Date: 06/07/2017 09:17:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_GetPostageAmount] 
(
	@MagPriceInstance int
)
RETURNS numeric(13,2)
AS
BEGIN
	DECLARE @ReturnValue numeric(13,2)
	
	SELECT	@ReturnValue = (isnull(pd.PostageAmount,0) *  isnull(pd.PostageRemitRate,0) * isnull(pd.ConversionRate,0))
	FROM	QSPCanadaProduct..Pricing_Details pd
	WHERE	pd.MagPrice_Instance = @MagPriceInstance

	RETURN @ReturnValue
END
GO
