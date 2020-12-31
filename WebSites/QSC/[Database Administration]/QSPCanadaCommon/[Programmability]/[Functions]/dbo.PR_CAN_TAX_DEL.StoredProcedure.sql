USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[PR_CAN_TAX_DEL]    Script Date: 06/07/2017 09:33:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[PR_CAN_TAX_DEL]

	@p_prov_code	          	VARCHAR(2),
	@p_country_code 	VARCHAR(2),
	@p_date                     	DATETIME ,
     	@p_amount 		NUMERIC(14,6),
	@p_transac		VARCHAR(10),
             @p_section_type	INT,
             @p_del_amount     	NUMERIC(14,6) OUTPUT
	
AS
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--    WRITTEN BY SAQIB SHAH ON 27 APRIL 2004
--THIS PROCEDURE IS CALLD FROM  DBO.PR_CAN_TAX_CALC_TAXES_AMOUNT
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  DECLARE  @V_TAX_ID 		INT
  DECLARE  @V_TAX_FUNCTION 	VARCHAR (100)
  DECLARE  @V_REC			INT
  DECLARE  @V_REC1_FUNCTION 	VARCHAR (100)
  DECLARE  @V_AMOUNT 		NUMERIC(14,6)
  DECLARE  @V_TAX_AMOUNT	NUMERIC(14,6)  
  DECLARE  @V_TAXES_AMOUNT	NUMERIC(14,6)  
  DECLARE  @V_BACKOUT_AMOUNT	NUMERIC(14,6)  
  DECLARE  @V_TAX_SUM_RATE	NUMERIC(14,6)  
  DECLARE  @V_BACKOUT_FUNCTION   	VARCHAR (100)

  SET @V_TAXES_AMOUNT = 0
  SET @V_BACKOUT_AMOUNT = 0
  SET @V_TAX_SUM_RATE = 0
  SET @V_REC = 0

  SELECT @V_BACKOUT_FUNCTION = (SELECT tax_backout_function
				  	FROM QSPCanadaCommon..PROVINCE
				 	WHERE province_code=@p_prov_code
				  	AND country_code=@p_country_code)


       IF @V_BACKOUT_FUNCTION IS NOT NULL 

           BEGIN

	   SELECT @V_TAX_SUM_RATE =  ( SELECT SUM(tax_rate)
				  	FROM QSPCanadaCommon..TAX tax,QSPCanadaCommon..TaxApplicableTax appl_tax
				    	WHERE tax.tax_id = appl_tax.tax_id
				    	AND appl_tax.section_type_id = @p_section_type
				    	AND appl_tax.country_code    = @p_country_code
				    	AND appl_tax.province_code  = @p_prov_code
				    	AND    tax.tax_effective_date   < @p_date)

	  SET @V_BACKOUT_AMOUNT= @P_AMOUNT-(@P_AMOUNT / (1 + (@V_TAX_SUM_RATE/100)))

	  SET @P_DEL_AMOUNT =    @P_AMOUNT -  @V_BACKOUT_AMOUNT

          END

     ELSE

	  BEGIN  

	  DECLARE Cur_Tax_Desc Cursor  For
	     SELECT app.tax_id,tax.tax_function
	     FROM QSPCanadaCommon..TaxApplicableTax app, QSPCanadaCommon..TAX as TAX
	     WHERE country_code  = @p_country_code
	     AND province_code   = @p_prov_code
	     AND section_type_id = @p_section_type
	     AND APP.TAX_ID = TAX.TAX_ID
	     ORDER BY app.tax_id DESC; --order by descending is  imporatnt to calculate Quebec PST

	   
    OPEN Cur_Tax_Desc
    FETCH NEXT FROM Cur_Tax_Desc INTO @V_TAX_ID,@V_TAX_FUNCTION
    WHILE @@FETCH_Status = 0

              BEGIN


               SET @V_REC = @V_REC + 1
                
               IF  @V_REC = 1 AND @V_TAX_FUNCTION = 'TAX_ON_TAX'
                  BEGIN
	       SET  @V_REC1_FUNCTION = 'TAX_ON_TAX'
	       SET @V_TAX_AMOUNT = @P_AMOUNT
	     END
	ELSE -- if not tax-on-tax (mean not quebec)
	     BEGIN
	       SET @V_TAX_AMOUNT = @P_AMOUNT
	     END
 

               IF  @V_REC = 2 AND @V_REC1_FUNCTION = 'TAX_ON_TAX' -- if the first record function was tax_on_tax then change the amount
                  BEGIN
	       SET  @V_TAX_AMOUNT = @P_AMOUNT -  @V_AMOUNT
	     END
	ELSE
	     BEGIN
	       SET @V_TAX_AMOUNT = @P_AMOUNT
	     END
 


		EXEC QSPCanadaCommon.DBO.PR_CAN_TAX_CALC_RATE @P_TAX_ID= @V_TAX_ID,
						    @P_DATE = @P_DATE,
						    @P_AMOUNT = @V_TAX_AMOUNT, 
						    @P_TRANSAC =  @P_TRANSAC,
						    @P_INVOICE_SECTION_ID =  Null,
						    @P_ERR_MSG  = Null,
						    @P_TAX_AMOUNT =  @V_AMOUNT OUTPUT

	           SET @V_TAXES_AMOUNT =  @V_TAXES_AMOUNT + @V_AMOUNT   

             FETCH NEXT FROM Cur_Tax_Desc INTO @V_TAX_ID,@V_TAX_FUNCTION  

            END

  	CLOSE Cur_Tax_Desc
	DEALLOCATE Cur_Tax_Desc

	 SET @P_DEL_AMOUNT  =  @P_AMOUNT -  @V_TAXES_AMOUNT 

  END
GO
