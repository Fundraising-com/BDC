USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCatalogForMarketingMgt]    Script Date: 06/07/2017 09:18:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectCatalogForMarketingMgt]
	@sCatalogCode		varchar(10) = '',
	@sCatalogName	varchar(50) = '',
	@iCatalogYear		int = 0,
	@sCatalogSeason	varchar(1) = '',
	@iCatalogType 		int= 0,
	@sCatalogLang		varchar(10) = '',
	@iCatalogStatus		int = 0,
	@iCampaignID		int = 0,
	@sProductCode		varchar(20) = ''
AS
DECLARE @sqlStatement nvarchar(4000)
set @sqlStatement =
	'SELECT	pm.Program_ID,
			pm.Program_Type,
			pm.SubType AS SubTypeID,
			coalesce(cdt.Description, '''') as SubType,
			s.FiscalYear AS Year,
			coalesce(s.Season, '''') AS Season,
			pm.Alpha,
			pm.Code,
			pm.Status AS StatusID,
			coalesce(cds.Description, '''') as Status,
			pm.Country,
			pm.Lang,
			pm.IsReplacement,
			pm.IsNational,
			pm.DateCreated,
			pm.UserIDCreated,
			pm.DateChanged,
			pm.UserIDChanged
	FROM		Program_Master pm LEFT OUTER JOIN
			QSPCanadaCommon..CodeDetail cdt ON pm.SubType = cdt.Instance LEFT OUTER JOIN
			QSPCanadaCommon..CodeDetail cds ON pm.Status = cds.Instance,
			QSPCanadaCommon..Season s
	WHERE	s.ID = pm.Season '
if(@sCatalogCode <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' AND pm.Code LIKE ''%' + @sCatalogCode + '%'' '
END

if(@sCatalogName <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' AND pm.Program_Type LIKE ''%' + @sCatalogName + '%'' '
END

if(@iCatalogYear <> 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' AND s.FiscalYear = ' + convert(nvarchar, @iCatalogYear)
END

if(@sCatalogSeason <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' AND s.Season = ''' + @sCatalogSeason + ''' '
END

if(@iCatalogType <> 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' AND pm.SubType = ' + convert(nvarchar, @iCatalogType)
END

if(@sCatalogLang <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' AND pm.Lang = ''' + @sCatalogLang + ''' '
END

if(@iCatalogStatus <> 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' AND pm.Status = ' + convert(nvarchar, @iCatalogStatus)
END

if(@iCampaignID <> 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' AND EXISTS
						(SELECT	ca.ID
						FROM		QSPCanadaCommon..Campaign ca,
								QSPCanadaCommon..CampaignToContentCatalog ctcc
						WHERE	ca.ID = ' + convert(nvarchar, @iCampaignID) +
						' AND ca.ID = ctcc.CampaignID
						AND ctcc.Content_Catalog_Code = pm.Code) '
END

if(@sProductCode <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' AND EXISTS
						(SELECT	pd.Product_Code
						FROM		Pricing_Details pd,
								ProgramSection ps
						WHERE	pd.Product_Code LIKE ''%' + @sProductCode + '%'' ' +
						' AND		pd.ProgramSectionID = ps.ID
						AND		ps.Program_ID = pm.Program_ID) '
END

set @sqlStatement = @sqlStatement + ' ORDER BY s.StartDate DESC '

exec(@sqlStatement)
GO
