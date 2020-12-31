USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[PR_UPDATE_TAX_REG_INFO]    Script Date: 06/07/2017 09:20:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[PR_UPDATE_TAX_REG_INFO]

AS

--written by saqib shah,  jan 2005
-- this proc insert  tax registration numbers for magazines which are partially supplied by user, e.g user defined GST # but no HST and vice vera


declare @TITLE_CODE VARCHAR(10), @TAX_REGISTRATION_NUMBER VARCHAR(50), @IS_EXIST VARCHAR(3)

SET  @IS_EXIST = NULL

    Delete from QSPCanadaCommon..TaxMagRegistration where   TAX_REGISTRATION_NUMBER is NULL;

 Declare  C1 cursor for 
 SELECT DISTINCT TITLE_CODE,TAX_REGISTRATION_NUMBER
 FROM QSPCanadaCommon..TaxMagRegistration mag_tax_reg
 WHERE TAX_ID IN (1,2,4,5)
 and TAX_REGISTRATION_NUMBER is not null   
 ORDER BY 1,2  


BEGIN

 	OPEN C1

	    FETCH NEXT FROM C1    INTO @TITLE_CODE , @TAX_REGISTRATION_NUMBER   

	    WHILE @@FETCH_Status = 0
                  

                BEGIN

      

------START GST----------

	       SELECT @IS_EXIST =  'YES'
	       FROM QSPCanadaCommon..TaxMagRegistration
                     WHERE TAX_ID = 1
	       AND TITLE_CODE  = @TITLE_CODE


	   IF  ISNULL(@IS_EXIST,'NO')  <> 'YES'

                   BEGIN

		INSERT INTO QSPCanadaCommon..TaxMagRegistration  
		(TITLE_CODE,TAX_ID,TAX_REGISTRATION_NUMBER)
		VALUES (@TITLE_CODE,1,@TAX_REGISTRATION_NUMBER)
		

	     END 


------------END GST------------------------------------------------




------START NB----------
	       SELECT @IS_EXIST =  'YES'
	       FROM QSPCanadaCommon..TaxMagRegistration
                     WHERE TAX_ID = 2
	       AND TITLE_CODE  = @TITLE_CODE

--	SELECT @TITLE_CODE, @IS_EXIST

	   IF  ISNULL(@IS_EXIST,'NO')  <> 'YES'

                   BEGIN

		INSERT INTO QSPCanadaCommon..TaxMagRegistration  
		(TITLE_CODE,TAX_ID,TAX_REGISTRATION_NUMBER)
		VALUES (@TITLE_CODE,2,@TAX_REGISTRATION_NUMBER)
		

	     END 


------------END NB------------------------------------------------



---------START NS-----------------------

	   SET  @IS_EXIST = NULL

 
	   SELECT @IS_EXIST =   'YES'
	   FROM QSPCanadaCommon..TaxMagRegistration
                 WHERE TAX_ID = 4
	  AND TITLE_CODE  = @TITLE_CODE



	   IF  ISNULL(@IS_EXIST,'NO')  <>  'YES'

                   BEGIN

		INSERT INTO QSPCanadaCommon..TaxMagRegistration 
		(TITLE_CODE,TAX_ID,TAX_REGISTRATION_NUMBER)
		VALUES (@TITLE_CODE,4,@TAX_REGISTRATION_NUMBER)
		

	     END 

--------END NS---------------------------



---------START NL-----------------------

	   SET  @IS_EXIST = NULL

 
	   SELECT  @IS_EXIST =   'YES'
	   FROM QSPCanadaCommon..TaxMagRegistration
                 WHERE TAX_ID = 5 
	  AND TITLE_CODE  = @TITLE_CODE

	   IF  ISNULL(@IS_EXIST,'NO')  <>  'YES'

                   BEGIN

		INSERT INTO QSPCanadaCommon..TaxMagRegistration 
		(TITLE_CODE,TAX_ID,TAX_REGISTRATION_NUMBER)
		VALUES (@TITLE_CODE,5,@TAX_REGISTRATION_NUMBER)
		

	     END 

	SET  @IS_EXIST = NULL 

--------END NL---------------------------


	                            
	    FETCH NEXT FROM C1    INTO @TITLE_CODE , @TAX_REGISTRATION_NUMBER   

                END

	CLOSE C1
	DEALLOCATE C1

END
GO
