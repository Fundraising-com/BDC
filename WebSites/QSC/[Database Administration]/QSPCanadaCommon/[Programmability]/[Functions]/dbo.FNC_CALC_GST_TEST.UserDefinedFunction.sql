USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[FNC_CALC_GST_TEST]    Script Date: 06/07/2017 09:33:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
create  FUNCTION [dbo].[FNC_CALC_GST_TEST]

           (	@p_amount 		NUMERIC(14,6),
             @p_title_code                 VARCHAR(10)  )

RETURNS Numeric(14,6)  AS  
BEGIN 

  DECLARE  @V_RATE 			NUMERIC(14,6)
  DECLARE  @V_TAX_1 		NUMERIC(14,6) --gst
  DECLARE  @V_EXIST			INT
  SET @V_EXIST = 0


          SELECT @V_EXIST = 1 
	FROM QSPCanadaCommon..TaxMagRegistration mag_tax_reg
	WHERE mag_tax_reg.title_code = @p_title_code
	AND mag_tax_reg.tax_id = 1
	AND mag_tax_reg.tax_registration_number IS NOT NULL

         IF @V_EXIST = 1

              BEGIN

	  SELECT @V_RATE=TAX_RATE  FROM TAX WHERE TAX_ID=1 

          	  SELECT @V_TAX_1 = @P_AMOUNT * (@V_RATE/100)

                 END 
         ELSE --  else if magazine has no tax registration number then no tax should be charged
            BEGIN
               SET @V_TAX_1 = 0
	END


  RETURN @V_TAX_1
  
END
GO
