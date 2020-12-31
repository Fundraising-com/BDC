USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAddress1Label]    Script Date: 06/07/2017 09:33:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetAddress1Label] (@lang varchar(2))
RETURNS VARCHAR(100)
AS
BEGIN

DECLARE @valueOut VARCHAR(100)

	if @lang = 'EN'
		SET @valueOut = '695 Riddell Road' 
	else
		SET @valueOut = '33 Prince Street Suite 200' 

RETURN @valueOut

END
GO
