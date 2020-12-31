USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetCodeDetail]    Script Date: 06/07/2017 09:21:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_GetCodeDetail]

           (	@p_CodeDetailInstance 	int )

RETURNS Varchar(100)  AS  
BEGIN 

  DECLARE  @V_DESC VARCHAR(100)

          SELECT @V_DESC = Description
	FROM QSPCanadaCommon..CodeDetail
	WHERE Instance = @p_CodeDetailInstance;


  RETURN @V_DESC
  
END
GO
