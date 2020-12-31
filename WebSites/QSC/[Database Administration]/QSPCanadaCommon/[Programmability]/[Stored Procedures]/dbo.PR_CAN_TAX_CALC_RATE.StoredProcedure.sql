USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[PR_CAN_TAX_CALC_RATE]    Script Date: 06/07/2017 09:33:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_CAN_TAX_CALC_RATE]

	@P_TAX_ID          	INT,
	@P_DATE   		DATETIME,
	@P_AMOUNT 		NUMERIC(14,6) ,
     	@P_TRANSAC 		VARCHAR(10),
             @P_INVOICE_SECTION_ID 	INT,
             @P_ERR_MSG		VARCHAR(10)  OUTPUT,
             @P_TAX_AMOUNT        NUMERIC(14,6) OUTPUT
	
AS
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--    WRITTEN BY SAQIB SHAH ON 22 APRIL 2004
--     CALCULATES CANADIAN TAX RATE AND AMOUNT FOR ONLY ONE TAX (GST O PST)  AT ONE TIME BASED ON THE TAX_ID PARAMETER
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  DECLARE  @V_RATE NUMERIC(10,4)
  DECLARE  @V_TAX_AMOUNT  NUMERIC(14,6)


  SELECT @V_RATE = (SELECT TAX_RATE  FROM QSPCanadaCommon.dbo.TAX WHERE TAX_ID=@P_TAX_ID )

       

        IF ISNULL(@P_TRANSAC,'NULL') = 'ADD' 

       	BEGIN

       	  SELECT @V_TAX_AMOUNT = @P_AMOUNT * (@V_RATE/100)

       	END

     ELSE

         BEGIN
              SELECT @V_TAX_AMOUNT = @P_AMOUNT - (@P_AMOUNT /(1 + (@V_RATE/100)))
         END


 SET @P_TAX_AMOUNT  =  ISNULL(@V_TAX_AMOUNT,0)
GO
