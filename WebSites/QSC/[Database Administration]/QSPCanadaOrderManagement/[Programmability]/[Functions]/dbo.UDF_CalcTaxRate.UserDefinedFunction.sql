USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_CalcTaxRate]    Script Date: 06/07/2017 09:21:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
*	Created by:	Benoit Nadon
*	Date:		01/30/2006
*	
*			This function is used to determine a consolidated tax rate from
*			two different rates, eg. GST + PST.
*	Parameters:	@fTaxGST - GST tax rate
*			@fTaxOther - HST or PST tax rate
*			@fTaxFunction -	TAX_ON_BASE if the rate is applied to the base price
*					TAX_ON_TAX if the rate is applied to the GST rate
*	Returns:	Consolidated tax rate
*/

CREATE FUNCTION [dbo].[UDF_CalcTaxRate]
(
	@fTaxGST	numeric(10, 2),
	@fTaxOther	numeric(10, 2),
	@zTaxFunction	varchar(20)
)  
RETURNS numeric(10, 5) AS  
BEGIN 
DECLARE	@fTaxRate	numeric(10, 5)

SET		@fTaxRate =	CASE @fTaxGST
					WHEN 0.00 THEN 1 + @fTaxOther / 100
					ELSE
						CASE @zTaxFunction
							WHEN 'TAX_ON_TAX' THEN (1 + @fTaxGST / 100) * (1 + @fTaxOther / 100)
							ELSE 1 + (@fTaxGST + @fTaxOther) / 100
						END
				END

RETURN	@fTaxRate
END
GO
