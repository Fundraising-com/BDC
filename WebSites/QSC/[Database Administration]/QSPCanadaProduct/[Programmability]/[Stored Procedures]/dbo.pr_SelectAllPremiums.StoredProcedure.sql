USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllPremiums]    Script Date: 06/07/2017 09:18:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllPremiums]

	@iYear		int = 0,
	@zSeason	varchar(1) = ''

AS

DECLARE @sqlStatement nvarchar(4000)

set @sqlStatement =
	'SELECT	p.ID,
			p.Year,
			p.Season,
			p.Code AS PremiumCode,
			p.Valid,
			pde.Description AS EnglishDescription,
			pdf.Description AS FrenchDescription
			
	FROM		Premium p LEFT OUTER JOIN
			PremiumDescription pde ON p.ID = pde.PremiumID AND pde.Lang = ''EN'' LEFT OUTER JOIN
			PremiumDescription pdf ON p.ID = pdf.PremiumID AND pdf.Lang = ''FR'' '

if(@iYear <> 0 AND @zSeason <> '')
begin
	set @sqlStatement = @sqlStatement + ' WHERE p.Year = ' + convert(nvarchar, @iYear) + ' AND (p.Season = ''' + @zSeason + ''' OR p.Season = ''Y'') '
end

set @sqlStatement = @sqlStatement + ' ORDER BY p.Code '

exec(@sqlStatement)
GO
