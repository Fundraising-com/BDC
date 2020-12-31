USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[PR_CAN_TAX_CALC_TAX]    Script Date: 06/07/2017 09:33:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_CAN_TAX_CALC_TAX]

	@p_prov_code	          	VARCHAR(2),
	@p_country_code 	VARCHAR(2),
	@p_date                     	DATETIME ,
     	@p_amount 		NUMERIC(14,6),
             @p_section_type	INT,
             @p_title_code                 VARCHAR(10),
             @P_TAX_1        	NUMERIC(14,6) OUTPUT, --GST
	@P_TAX_2        	NUMERIC(14,6) OUTPUT-- PST/HST
AS
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--    WRITTEN BY SAQIB SHAH ON 23 APRIL 2004
--     CALCULATES CANADIAN TAX AMOUNTS
-- THIS PROCEDURE WOULD BE CALLED FROM DBO.PR_CALC_ORDER_ITEM_AMOUNTS
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  DECLARE  @V_TAX_ID 		INT
  DECLARE  @V_TAX_FUNCTION 	VARCHAR (100)
  DECLARE  @V_LOC 			VARCHAR(100)
  DECLARE  @V_REC 			INT --counter to calc number of records fetched by cursor
  DECLARE  @V_TAX_1 		NUMERIC(14,6) --gst
  DECLARE  @V_TAX_2 		NUMERIC(14,6) --pst/hst
  DECLARE  @V_TAX_ON_TAX		NUMERIC(14,6)

  SET @V_REC = 0
  SET @V_TAX_ON_TAX = 0
 

  Declare Cur_Tax_Func Cursor  For

    SELECT tax.tax_id, tax.tax_function
     FROM QSPCanadaCommon..TAXAPPLICABLETAX tax_applic, QSPCanadaCommon..TAX tax
     WHERE tax_applic.province_code = @p_prov_code
       AND tax_applic.section_type_id = isnull(@p_section_type,2)-- 2 = magazine
       AND tax_applic.country_code = @p_country_code
       AND tax_applic.tax_id = tax.tax_id
       AND EXISTS -- following lines validate if its a magzine then it should have a registration number(only in remit case)
           (SELECT 1 
               FROM QSPCanadaCommon..TaxMagRegistration mag_tax_reg
               WHERE mag_tax_reg.title_code = @p_title_code
                 AND mag_tax_reg.tax_id = tax_applic.tax_id
                 AND mag_tax_reg.tax_registration_number IS NOT NULL
            UNION ALL
            SELECT 1 FROM QSPCanadaCommon..Tax tax2 -- if this procedure is called from Order Entry calculation then title code would be null which means we have to calculate tax whether or not magazine has reg#  OR  title code can be null for non-magazine items i.e gifts,chocolates
               WHERE @p_title_code IS NULL 
           )
order by tax.tax_id;


	OPEN Cur_Tax_Func
	    FETCH NEXT FROM Cur_Tax_Func INTO @V_TAX_ID, @V_TAX_FUNCTION  

	    WHILE @@FETCH_Status = 0
                  

                BEGIN
                        SELECT @V_REC = @V_REC + 1
                                      
                       IF  @V_REC = 1 --GST, then store amount in @V_TAX_1 variable, else  @V_TAX_2 

                          	   BEGIN

		EXEC QSPCanadaCommon.dbo.PR_CAN_TAX_CALC_RATE @P_TAX_ID= @V_TAX_ID,
						    @P_DATE = @P_DATE,
						    @P_AMOUNT = @P_AMOUNT, 
						    @P_TRANSAC =  'ADD',
						    @P_INVOICE_SECTION_ID =  Null,
						    @P_ERR_MSG  = Null,
						    @P_TAX_AMOUNT =  @V_TAX_1 OUTPUT



                              END 

                      ELSE IF @V_REC = 2 -- PST/HST

                       	IF @V_TAX_FUNCTION = 'TAX_ON_BASE'

                              BEGIN
			EXEC QSPCanadaCommon.dbo.PR_CAN_TAX_CALC_RATE @P_TAX_ID= @V_TAX_ID,
						    @P_DATE 	= @P_DATE,
						    @P_AMOUNT = @P_AMOUNT, 
						    @P_TRANSAC =  'ADD',
						    @P_INVOICE_SECTION_ID =  Null,
						    @P_ERR_MSG  = Null,
						    @P_TAX_AMOUNT =  @V_TAX_2 OUTPUT
                              END

                       	ELSE IF @V_TAX_FUNCTION = 'TAX_ON_TAX' --  right now it is in use only for PST Quebec which is calculated on the ( amount +gst )

	                BEGIN

                                 SET  @V_TAX_ON_TAX = @P_AMOUNT + @V_TAX_1
			EXEC QSPCanadaCommon.dbo.PR_CAN_TAX_CALC_RATE @P_TAX_ID= @V_TAX_ID,
						    @P_DATE			= @P_DATE,
						    @P_AMOUNT 		= @V_TAX_ON_TAX, 
						    @P_TRANSAC 		= 'ADD',
						    @P_INVOICE_SECTION_ID 	= Null,
						    @P_ERR_MSG  		= Null,
						    @P_TAX_AMOUNT 		= @V_TAX_2 OUTPUT
                             END 

                                     
                    FETCH NEXT FROM Cur_Tax_Func INTO @V_TAX_ID, @V_TAX_FUNCTION
  
                END

	CLOSE Cur_Tax_Func
	DEALLOCATE Cur_Tax_Func

----------------------RETURN  CALUSES---------------------------------

     SET @P_TAX_1  = @V_TAX_1
     SET @P_TAX_2  = @V_TAX_2

-----------------------------------------------------------------------------------------

--PRINT @P_TAX_1
--PRINT @P_TAX_2
GO
