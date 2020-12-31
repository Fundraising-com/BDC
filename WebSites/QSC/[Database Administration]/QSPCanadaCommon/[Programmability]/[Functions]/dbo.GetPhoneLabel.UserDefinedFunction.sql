USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[GetPhoneLabel]    Script Date: 06/07/2017 09:33:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- SELECT [dbo].[GetTaxLabel]('EN','NS')

CREATE FUNCTION [dbo].[GetPhoneLabel] (@lang varchar(2))
RETURNS VARCHAR(20)
AS
BEGIN

DECLARE @valueOut VARCHAR(20)

	if @lang = 'EN'
		SET @valueOut = '1-800-667-2536' 
	else
		SET @valueOut = '1-800-667-2536' 

RETURN @valueOut

END
GO
