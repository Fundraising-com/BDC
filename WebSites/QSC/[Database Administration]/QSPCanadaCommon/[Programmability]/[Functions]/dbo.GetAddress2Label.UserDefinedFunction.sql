USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAddress2Label]    Script Date: 06/07/2017 09:33:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetAddress2Label] (@lang varchar(2))
RETURNS VARCHAR(100)
AS
BEGIN

DECLARE @valueOut VARCHAR(100)

	if @lang = 'EN'
		SET @valueOut = 'Orangeville, ON   L9W 4Z5 ' 
	else
		SET @valueOut = 'Montreal, QC   H3C 2M7' 

RETURN @valueOut

END
GO
