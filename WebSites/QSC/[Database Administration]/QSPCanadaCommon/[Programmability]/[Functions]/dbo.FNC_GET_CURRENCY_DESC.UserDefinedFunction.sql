USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[FNC_GET_CURRENCY_DESC]    Script Date: 06/07/2017 09:33:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[FNC_GET_CURRENCY_DESC]

           (	@p_currency_code 	int )

RETURNS Varchar(3)  AS  
BEGIN 

  DECLARE  @V_CUR_DESC VARCHAR(3)

          SELECT @V_CUR_DESC = substring(description,1,3) 
	FROM QSPCanadaCommon..CodeDetail
	WHERE CodeHeaderInstance = 800
	AND Instance = @p_currency_code;

  RETURN @V_CUR_DESC
  
END
GO
