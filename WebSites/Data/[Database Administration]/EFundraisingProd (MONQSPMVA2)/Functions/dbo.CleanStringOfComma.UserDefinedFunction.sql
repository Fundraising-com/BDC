USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[CleanStringOfComma]    Script Date: 02/14/2014 13:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  FUNCTION [dbo].[CleanStringOfComma] ( @OriginalString varchar(255)) RETURNS varchar(255)
AS 
BEGIN
	DECLARE @Lenght as int
	DECLARE @Cpt as int
	DECLARE @CleanString as Varchar(255)
	
	SET @Cpt = 1
	SET @CleanString = ''
	SET @Lenght = LEN(@OriginalString)
	WHILE @cpt<@Lenght +1
	BEGIN
			IF SUBSTRING(@OriginalString, @Cpt, 1) <> ','
				SET @CleanString = @CleanString + SUBSTRING(@OriginalString, @Cpt, 1)
			ELSE
				SET @CleanString = @CleanString + ' '
			
		SET @Cpt = @Cpt + 1
	END
	Return @CleanString
END
GO
