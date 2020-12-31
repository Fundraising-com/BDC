USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllProducts]    Script Date: 06/07/2017 09:18:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllProducts]

	@zProductCode		varchar(20) = '',
	@zRemitCode		varchar(20) = '',
	@zProductName	varchar(55) = '',
	@iYear			int = 0,
	@zSeason		varchar(1) = '',
	@iProductType		int = 0,
	@iProductStatus	int = 0,
	@iPublisherID		int = 0,
	@iFulfillmentHouseID	int = 0



AS
    declare @mkezProductCode		varchar(20),
	@mkezRemitCode		varchar(20),
	@mkezProductName	varchar(55),
	@mkeiYear			int ,
	@mkezSeason		varchar(1),
	@mkeiProductType		int ,
	@mkeiProductStatus	int ,
	@mkeiPublisherID		int ,
	@mkeiFulfillmentHouseID	int 
DECLARE	@sqlStatement	nvarchar(4000)
    set @mkezProductCode = @zProductCode
    set @mkezRemitCode = @zRemitCode
    set @mkezProductName = @zProductName
    set @mkeiYear = @iYear
    set @mkezSeason = @zSeason
    set @mkeiProductType = @iProductType
    set @mkeiProductStatus = @iProductStatus
    set @mkeiPublisherID = @iPublisherID
    set @mkeiFulfillmentHouseID = @iFulfillmentHouseID

	

	set @sqlStatement = 'SELECT	p.Product_Instance,
			p.Product_Code,
			p.Product_Year AS Year,
			p.Product_Season AS Season,
			p.Product_Sort_Name,
			p.Lang as Language,
			p.Type AS ProductType,
			cdProductType.Description AS ProductTypeDescription,
			p.Status AS ProductStatus,
			cdProductStatus.Description AS ProductStatusDescription,
			p.Pub_Nbr AS PublisherID,
			p.Fulfill_House_Nbr AS FulfillmentHouseID,
			COALESCE(p.OracleCode, '''') AS OracleCode,
			COALESCE(p.Prize_Level, '''') AS Prize_Level,
			COALESCE(p.Prize_Level_Qty_Required, 0) AS Prize_Level_Qty_Required,
			COALESCE(p.RemitCode, '''') AS RemitCode,
			COALESCE(p.IsQSPExclusive, 0) AS IsQSPExclusive

	FROM		Product p,
			QSPCanadaCommon..CodeDetail cdProductType,
			QSPCanadaCommon..CodeDetail cdProductStatus

	WHERE	cdProductType.Instance = p.Type
	AND		cdProductStatus.Instance = p.Status
	AND		p.Type NOT IN (46009, 46010, 46011, 46015) '

	IF(COALESCE(@mkezProductCode, '') <> '')
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND p.Product_Code LIKE ''%' + @mkezProductCode + '%'' '
	END

	IF(COALESCE(@mkezRemitCode, '') <> '')
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND p.RemitCode LIKE ''%' + @mkezRemitCode + '%'' '
	END

	IF(COALESCE(@mkezProductName, '') <> '')
	BEGIN
--QSPCanadaOrderManagement.dbo.UDF_ReplaceAccents(p.Product_Sort_Name) LIKE ''%' + QSPCanadaOrderManagement.dbo.UDF_ReplaceAccents(@mkezProductName) + '%'' '
		SET @sqlStatement = @sqlStatement + ' AND p.Product_Sort_Name collate SQL_Latin1_General_CP1_CI_AI LIKE ''%' + @mkezProductName + '%'' '
	END

	IF(COALESCE(@mkeiYear, 0) <> 0)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND p.Product_Year = ' + CONVERT(nvarchar, @mkeiYear)
	END

	IF(COALESCE(@mkezSeason, '') <> '')
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND p.Product_Season = ''' + @mkezSeason + ''' '
	END

	IF(COALESCE(@mkeiProductType, 0) <> 0)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND p.Type = ' + CONVERT(nvarchar, @mkeiProductType)
	END

	IF(COALESCE(@mkeiProductStatus, 0) <> 0)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND p.Status = ' + CONVERT(nvarchar, @mkeiProductStatus)
	END

	IF(COALESCE(@mkeiPublisherID, 0) <> 0)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND p.Pub_Nbr = ' + CONVERT(nvarchar, @mkeiPublisherID)
	END

	IF(COALESCE(@mkeiFulfillmentHouseID, 0) <> 0)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND p.Fulfill_House_Nbr = ' + CONVERT(nvarchar, @mkeiFulfillmentHouseID)
	END

	SET @sqlStatement = @sqlStatement + ' ORDER BY	p.Product_Year DESC,
			p.Product_Season DESC,
			p.Product_Sort_Name '

	EXEC(@sqlStatement)
GO
