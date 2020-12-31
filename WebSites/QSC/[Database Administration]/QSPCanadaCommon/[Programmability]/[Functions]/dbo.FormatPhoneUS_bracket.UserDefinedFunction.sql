USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[FormatPhoneUS_bracket]    Script Date: 06/07/2017 09:33:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[FormatPhoneUS_bracket](@Phone varchar(20))
RETURNS VARCHAR(20)
AS
BEGIN

  DECLARE @newPhone varchar(20)
  SELECT @newPhone = REPLACE(REPLACE(REPLACE(REPLACE(@Phone, '(', ''), ')', ''), ' ', ''), '-', '')
  IF (len(@newPhone) <> 10)
  BEGIN
    RETURN @Phone
  END

  RETURN '(' + substring( @newPhone, 1, 3) + ') ' + substring( @newPhone, 4, 3) + '-' + substring( @newPhone, 7, 4) 
END
GO
