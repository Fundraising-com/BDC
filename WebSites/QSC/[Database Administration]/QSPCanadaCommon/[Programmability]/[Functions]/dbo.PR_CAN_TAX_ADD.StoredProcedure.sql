USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[PR_CAN_TAX_ADD]    Script Date: 06/07/2017 09:33:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[PR_CAN_TAX_ADD]

	@p_prov_code	          	VARCHAR(2),
	@p_country_code 	VARCHAR(2),
	@p_date                     	DATETIME ,
     	@p_amount 		NUMERIC(14,6),
	@p_transac		VARCHAR(10),
             @p_section_type	INT,
             @p_add_amount     	NUMERIC(14,6) OUTPUT
	
AS
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--    WRITTEN BY SAQIB SHAH ON 27 APRIL 2004
-- THIS PROECDURE IS CALLED FROM  DBO.PR_CAN_TAX_CALC_TAXES_AMOUNT
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  DECLARE  @V_TAX_ID 		INT
  DECLARE  @V_TAX_FUNCTION 	VARCHAR (100)
  DECLARE  @V_AMOUNT 		NUMERIC(14,6)   
  DECLARE  @V_TAXES_AMOUNT	NUMERIC(14,6)  
  DECLARE  @V_TAX_ON_TAX		NUMERIC(14,6)  

  SET @V_TAXES_AMOUNT = 0


  DECLARE Cur_Tax_ID Cursor  For
     SELECT app.tax_id,tax.tax_function
     FROM QSPCanadaCommon..TaxApplicableTax app, QSPCanadaCommon..TAX as TAX
     WHERE country_code  = @p_country_code
     AND province_code   = @p_prov_code
     AND section_type_id = @p_section_type
     AND APP.TAX_ID = TAX.TAX_ID
     ORDER BY app.tax_id ASC; --order by ascending is very imporatnt to calculate for Quebec PST

	   
	  OPEN Cur_Tax_ID
	    FETCH NEXT FROM Cur_Tax_ID INTO @V_TAX_ID,@V_TAX_FUNCTION
	    WHILE @@FETCH_Status = 0
		

              BEGIN

                      IF @V_TAX_FUNCTION = 'TAX_ON_BASE'
                         BEGIN
		EXEC QSPCanadaCommon.dbo.PR_CAN_TAX_CALC_RATE @P_TAX_ID= @V_TAX_ID,
						    @P_DATE = @P_DATE,
						    @P_AMOUNT = @P_AMOUNT, 
						    @P_TRANSAC =  @P_TRANSAC,
						    @P_INVOICE_SECTION_ID =  Null,
						    @P_ERR_MSG  = Null,
						    @P_TAX_AMOUNT =  @V_AMOUNT OUTPUT

	           SET @V_TAXES_AMOUNT =  @V_TAXES_AMOUNT + @V_AMOUNT   
	
	           END

                     ELSE IF @V_TAX_FUNCTION = 'TAX_ON_TAX'
                         BEGIN
		SET @V_TAX_ON_TAX = @P_AMOUNT + @V_AMOUNT -- parameter amount + gst
		EXEC QSPCanadaCommon.dbo.PR_CAN_TAX_CALC_RATE @P_TAX_ID= @V_TAX_ID,
						    @P_DATE = @P_DATE,
						    @P_AMOUNT = @V_TAX_ON_TAX , 
						    @P_TRANSAC =  @P_TRANSAC,
						    @P_INVOICE_SECTION_ID =  Null,
						    @P_ERR_MSG  = Null,
						    @P_TAX_AMOUNT =  @V_AMOUNT OUTPUT

	           SET @V_TAXES_AMOUNT  = @V_TAXES_AMOUNT + @V_AMOUNT

	           END
  
             FETCH NEXT FROM Cur_Tax_ID INTO @V_TAX_ID,@V_TAX_FUNCTION  

            END

  	CLOSE Cur_Tax_ID
	DEALLOCATE Cur_Tax_ID
 
	SET @P_ADD_AMOUNT =  @P_AMOUNT + @V_TAXES_AMOUNT  -- RETURN CLAUSE
GO
