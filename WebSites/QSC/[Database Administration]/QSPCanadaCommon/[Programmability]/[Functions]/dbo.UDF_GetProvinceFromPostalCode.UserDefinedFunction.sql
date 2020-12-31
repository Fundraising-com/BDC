USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetProvinceFromPostalCode]    Script Date: 06/07/2017 09:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_GetProvinceFromPostalCode]
(
	@PostalCode	VARCHAR(6)
)

RETURNS VARCHAR(2)

AS

BEGIN
	
DECLARE @PostalCodePrefix varchar(1)
DECLARE @ProvinceCode varchar(2)

	SELECT @PostalCodePrefix = SUBSTRING(@PostalCode,1,1)
	
	RETURN
	(
		CASE  @PostalCodePrefix 
				WHEN 'A' THEN	'NL' 
				WHEN 'B' THEN	'NS' 
				WHEN 'C' THEN	'PE'
				WHEN 'E' THEN	'NB'
				WHEN 'G' THEN	'QC'
				WHEN 'H' THEN	'QC'
				WHEN 'J' THEN	'QC'
				WHEN 'K' THEN	'ON'
				WHEN 'L' THEN	'ON'
				WHEN 'M' THEN	'ON'
				WHEN 'N' THEN	'ON'
				WHEN 'P' THEN	'ON'
				WHEN 'R' THEN	'MB'
				WHEN 'S' THEN	'SK'
				WHEN 'T' THEN	'AB'
				WHEN 'V' THEN	'BC'
				WHEN 'X' THEN	'NT'
				WHEN 'Y' THEN	'YT'
		ELSE		
			'ON'
		END
	)

END
GO
