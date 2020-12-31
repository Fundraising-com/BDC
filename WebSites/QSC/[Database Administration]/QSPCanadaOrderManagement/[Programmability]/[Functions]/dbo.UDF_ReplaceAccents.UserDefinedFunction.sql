USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_ReplaceAccents]    Script Date: 06/07/2017 09:21:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_ReplaceAccents] (@zInputString NVARCHAR(4000))  
RETURNS NVARCHAR(4000) AS  
BEGIN 

	DECLARE @zOutputString	NVARCHAR(4000)
	DECLARE @zAccents		NVARCHAR(50)
	DECLARE @zNoAccents	NVARCHAR(50)
	DECLARE @iLength		INT
	DECLARE @i			INT

	SET @zOutputString =	@zInputString
	SET @zAccents =	'áàâäéèêëíìîïóòôöúùûüñÁÀÂÄÉÈÊËÍÌÎÏÓÒÔÖÚÙÛÜÑ'
	SET @zNoAccents =	'aaaaeeeeiiiioooouuuunAAAAEEEEIIIIOOOOUUUUN'
	SET @iLength =		LEN(@zAccents)
	SET @i =		1
	
	WHILE(@i <= @iLength)
	BEGIN
		SET @zOutputString = REPLACE(@zOutputString, SUBSTRING(@zAccents, @i, 1), SUBSTRING(@zNoAccents, @i, 1))

		SET @i = @i + 1
	END

	RETURN @zOutputString
 
END
GO
