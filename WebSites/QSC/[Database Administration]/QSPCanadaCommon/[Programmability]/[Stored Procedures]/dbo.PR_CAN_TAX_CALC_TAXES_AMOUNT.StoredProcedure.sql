USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[PR_CAN_TAX_CALC_TAXES_AMOUNT]    Script Date: 06/07/2017 09:33:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[PR_CAN_TAX_CALC_TAXES_AMOUNT]

	@p_prov_code	          	VARCHAR(2),
	@p_country_code 	VARCHAR(2),
	@p_date                     	DATETIME ,
     	@p_amount 		NUMERIC(14,6),
	@p_transac		VARCHAR(10),
             @p_section_type	INT,
             @p_return_amount     	NUMERIC(14,6) OUTPUT --sum of all taxes amount
	
AS
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--    WRITTEN BY SAQIB SHAH ON 23 APRIL 2004
-- APPEND OR REDUCE THE APLICABLE TAX AMOUNT FROM THE AMOUNT PASSED AS  PARAMETER
--     CALCULATES CANADIAN TAX AMOUNTS AND RETURN THE  SUM OF ALL APPLICABLE TAXES + PARAMETER AMOUNT (IF ADD)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  DECLARE  @V_TAXES_AMOUNT	NUMERIC(14,6)  
	
 SET @V_TAXES_AMOUNT = 0

---------------------------------------------------ADD---------------------------------------------------------------------------------------------------------------------------


  IF @P_TRANSAC = 'ADD' -- THEN APPEND THE TAX AMOUNT WITH THE PARAMETER AMOUNT
     BEGIN
        EXEC QspCanadaCommon.dbo.PR_CAN_TAX_ADD       @p_prov_code = @p_prov_code,
						    	@p_country_code = @p_country_code,
						    	@p_date = @p_date, 
						    	@p_amount = @p_amount,
						    	@P_TRANSAC =  @P_TRANSAC,
						    	@p_section_type =  @p_section_type,
						    	@p_add_amount =  @V_TAXES_AMOUNT OUTPUT
     
	
      END
      
--------------------------------------- DELETE ------------------------------------------------------------------------------------------------------------------

  ELSE  IF @P_TRANSAC= 'DEL' -- CACULATE THE TAXES AMOUNT FROM THE PARAMETER AMOUNT AND THEN REDUCE  IT FROM THE PARAMETER AMOUNT

         BEGIN
	EXEC QspCanadaCommon.dbo.PR_CAN_TAX_DEL   @p_prov_code = @p_prov_code,
						    	@p_country_code = @p_country_code,
						    	@p_date = @p_date, 
						    	@p_amount = @p_amount,
						    	@P_TRANSAC =  @P_TRANSAC,
						    	@p_section_type =  @p_section_type,
						    	@p_del_amount =  @V_TAXES_AMOUNT OUTPUT
     
      END


-----------RETURN----------------------------------------------------------------------------------------------------------------------------------------------

   SET @P_RETURN_AMOUNT  = @V_TAXES_AMOUNT
--   PRINT @P_RETURN_AMOUNT
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
GO
