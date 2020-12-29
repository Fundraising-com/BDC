USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Return_Ageing_By_Sale]    Script Date: 02/14/2014 13:09:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   FUNCTION [dbo].[fn_Return_Ageing_By_Sale](@SalesID Integer, @DateWanted DATETIME)
RETURNS VARCHAR(20)
AS
BEGIN
	DECLARE @no_of_day Integer;
	DECLARE @ActualShipDate Datetime;
	DECLARE @Ageing VARCHAR(20);
	
	
	SELECT @ActualShipDate = Actual_ship_Date FROM Sale WHERE Sales_ID = @SalesID
	
	SET @no_of_day = Datediff(Day, @ActualShipDate, @DateWanted)
	
	IF (@no_of_day < 29)
		SET @Ageing = '0-29 Days'
	ELSE
		IF @no_of_day <60 
			SET @Ageing  = '30-59 Days'		
		ELSE
			IF @no_of_day <90 
				SET @Ageing = '60-89 Days'
			ELSE
				IF @no_of_day <120 
					SET @Ageing = '90-120 Days'
				ELSE
					SET @Ageing = '120+ Days'
	--SELECT @Ageing
	RETURN(@Ageing)
END
GO
