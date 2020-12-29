USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[udf_evaluate_lead]    Script Date: 02/14/2014 13:09:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[udf_evaluate_lead] ( 
	@intLeadActivityTypeID TINYINT
	, @intDateDifference NUMERIC(9,2) 
)  
RETURNS VARCHAR(25)
AS  
BEGIN 
DECLARE @strMessage VARCHAR(25)

IF @intDateDifference >= 7 
	SET @strMessage = '4 - Unreachable'
ELSE
BEGIN
	IF @intLeadActivityTypeID = 2 
	BEGIN
		IF 1 <= @intDateDifference AND @intDateDifference >= 0 
			SET @strMessage = '1 - New'
		ELSE
			SET @strMessage = '3 - Not contacted'
	END
	ELSE
	            SET @strMessage = '2 - Scheduled call back'
END    

RETURN( @strMessage )

END
GO
