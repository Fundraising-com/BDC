USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_GetCoast]    Script Date: 02/14/2014 13:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fct_GetCoast] (@time_zone_diff int)  RETURNS Varchar(10) 
BEGIN 
	DECLARE @result varchar(10)

	if @time_zone_diff >= -1
		SET @result = 'East'
	else if @time_zone_diff = -2
		SET @result = 'Center'
	else
		SET @result = 'West'

	RETURN @result
END
GO
