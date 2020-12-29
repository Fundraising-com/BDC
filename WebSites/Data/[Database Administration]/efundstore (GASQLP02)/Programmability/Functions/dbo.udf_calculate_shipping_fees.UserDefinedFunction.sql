USE [eFundstore]
GO
/****** Object:  UserDefinedFunction [dbo].[udf_calculate_shipping_fees]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[udf_calculate_shipping_fees] (
	@intAccountClassID TINYINT
	, @intDollarAmount NUMERIC(10,2)
) RETURNS TINYINT 
AS  
BEGIN 
	DECLARE @intShippingFees TINYINT
	SELECT
		@intShippingFees = shipping_fee 
	FROM 	accounting_class_shipping_fees
	WHERE	
		accounting_class_id = @intAccountClassID
	 AND	@intDollarAmount BETWEEN min_amount AND max_amount 
	RETURN( @intShippingFees )
END
GO
