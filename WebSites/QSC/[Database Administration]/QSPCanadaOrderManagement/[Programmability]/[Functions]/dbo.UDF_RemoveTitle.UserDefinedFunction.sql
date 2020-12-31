USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_RemoveTitle]    Script Date: 06/07/2017 09:21:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_RemoveTitle] (@InputString NVARCHAR(4000))  

RETURNS NVARCHAR(4000) AS  

BEGIN 

	--For efficiency exit when first character is not an M as all replacements begin with M
	IF LEFT(@InputString, 1) <> 'M' RETURN RTRIM(@InputString)

	RETURN (REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(RTRIM(LTRIM(@InputString)), 'Mr ', ''),
			'Mr. ', ''), 'Mister ', ''), 'Mrs ', ''), 'Mrs. ', ''), 'Ms ', ''), 'Ms. ', ''), 'Miss ', ''), 'Misses ', ''), 'M. ', ''),
			'Mdme ', ''), 'Mdme. ', ''), 'Mlle ', ''), 'Mlle. ', ''), 'Mme ', ''), 'Mme. ', ''), 'Madame ', ''))
	 
END
GO
