USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_IsMagReg]    Script Date: 06/07/2017 09:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_IsMagReg]

           (	    @p_title_code                 VARCHAR(10)  )

RETURNS Varchar(1)  AS  
BEGIN 

 Declare @IS_MAG_REG varchar(1)

  SET @IS_MAG_REG = 'N'


          SELECT @IS_MAG_REG = 'Y'
	FROM QSPCanadaCommon..TaxMagRegistration mag_tax_reg
	WHERE mag_tax_reg.title_code = @p_title_code
	AND mag_tax_reg.tax_id = 1
	AND mag_tax_reg.tax_registration_number IS NOT NULL

  RETURN @IS_MAG_REG
  
END
GO
