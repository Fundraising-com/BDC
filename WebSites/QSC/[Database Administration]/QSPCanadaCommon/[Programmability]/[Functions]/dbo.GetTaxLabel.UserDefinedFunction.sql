USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[GetTaxLabel]    Script Date: 06/07/2017 09:33:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetTaxLabel] (@lang varchar(2), @province varchar(2))
RETURNS VARCHAR(7)
AS
BEGIN
	DECLARE @valueOut VARCHAR(7)

SET @valueOut = 'GST'

if @lang = 'EN'
	
	BEGIN
		if (@province in ('NB','NS','NL','PE','ON'))
			SET @valueOut =  'HST'
		else
			if @province = 'QC'
				SET @valueOut =  'GST/QST'
			else
				SET @valueOut =  'GST'
	END

else
	BEGIN
		if (@province in ('NB','NS','NL','PE','ON'))
			SET @valueOut =  'TVH'
		else
			if @province = 'QC'
				SET @valueOut =  'TPS/TVQ'
			else
				SET @valueOut =  'TPS'
	END

RETURN @valueOut

END
GO
