USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[udfStripBracket]    Script Date: 06/07/2017 09:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[udfStripBracket]  (@str varchar(255))
RETURNS nvarchar(255)
AS
BEGIN
   Declare @udfStripBracket nvarchar(255)

   SELECT @udfStripBracket = Convert(nvarchar(255),REPLACE((SELECT 
REPLACE(@str,'(','')),')',''))

   SELECT @udfStripBracket = Convert(nvarchar(255),REPLACE((SELECT 
REPLACE(@udfStripBracket,' ','')),'/',''))

   SELECT @udfStripBracket = Convert(nvarchar(255),REPLACE((SELECT 
REPLACE(@udfStripBracket,'-','')),'.',''))

   RETURN(@udfStripBracket)
END
GO
