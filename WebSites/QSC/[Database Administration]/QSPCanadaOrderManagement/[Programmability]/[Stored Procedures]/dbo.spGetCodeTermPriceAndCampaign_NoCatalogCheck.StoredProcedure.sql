USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spGetCodeTermPriceAndCampaign_NoCatalogCheck]    Script Date: 06/07/2017 09:20:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE[dbo].[spGetCodeTermPriceAndCampaign_NoCatalogCheck]

@Code varchar(20),
@term int,
@price numeric(10,2),
@CampaignId int,
@ProductType int

AS
set nocount on
Declare @Season char(1)
Declare @Year int
Declare @Lang varchar(10)
Declare @ShipToAccountId int, @ShipToType Int, @TaxRegionId Int
Declare @SeasonID int

SELECT 

	-- Ben - 2006-01-31 : Changed so that it keeps the products for one month after a season change
	@Year = CASE
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) > 7 THEN YEAR(CONVERT(smalldatetime,StartDate)) + 1
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) <= 7 THEN YEAR(CONVERT(smalldatetime,StartDate))
		ELSE
			0
		END
	,@Season = CASE
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) > 7 OR MONTH(CONVERT(smalldatetime,StartDate)) = 1 THEN 'F'
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) BETWEEN 2 AND 7 THEN 'S'
		ELSE ''
		END
	,@Lang = Lang
	,@ShipToAccountId = ShipToAccountId
	,@ShipToType = isnull(SuppliesShipToCampaignContactId,2)
	
	
FROM
	QSPCanadaCommon..Campaign
WHERE
	Id = @CampaignId

Select @SeasonID = ID from QSPCanadaCommon..Season where FiscalYear=@Year and Season=@Season

/*
--print @ShipToAccountId
--print @ShipToType
PRINT '@Season=' + Convert(varchar, @Season)
PRINT '@Year=' + Convert(varchar, @Year)
PRINT '@Lang=' + Convert(varchar, @Lang)
*/
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the Address To Send To and find TaxRegionId
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Street1 varchar(30), @Street2 varchar(30), @City varchar(30), @StateProvince varchar(30), @PostalCode varchar(10), @FirstName varchar(50), @LastName varchar(50)
DECLARE @Zip4 varchar(4), @Country varchar(20)

IF @ShipToType = 1
	BEGIN
		--- Populate Variables with FM data when that table becomes available
		SELECT @Street1 = ''
	END
ELSE IF @ShipToType = 2
	BEGIN
		SELECT
			@FirstName =  '',
			@LastName = [Name]
		FROM
			QSPCanadaCommon..CAccount
		WHERE
			Id = @CampaignId

		SELECT
			@Street1 = C.Street1,
			@Street2 = C.Street2,
			@City = C.City,
			@StateProvince = C.StateProvince,
			@PostalCode = C.Postal_Code,
			@Zip4 = C.Zip4,
			@Country = C.Country
			
		FROM
			QSPCanadaCommon..CAccount A
			INNER JOIN QSPCanadaCommon..AddressList B ON A.AddressListId = B.Id
			INNER JOIN QSPCanadaCommon..Address C ON C.AddressListId = B.Id
		WHERE
			C.Address_Type = 54001 		--- SHIP TO Address Type
			AND A.Id = @ShipToAccountId
	END


--PRINT '@Street1=' + @Street1 + ', @City = ' + @City + ', @StateProvince = ' + @StateProvince + ', @Country = ' + @Country

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the TaxRegionId 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT @TaxRegionId = TaxRegionId FROM QSPCanadaCommon..TaxRegionProvince WHERE Province LIKE @StateProvince
/*
PRINT '@TaxRegionId: ' + Convert(varchar, IsNull(@TaxRegionId, 0))
Print @Lang
*/
if @ProductType = 46001
begin
	select B.Product_Code,
		B.Product_Sort_Name,
		MagPrice_instance,
		PD.nbr_of_issues as Term,
		Pd.qsp_price as Price,
		Pd.programsectionid as ProgramSection,
		pd.pricing_season,
		pd.Language_code,
		C.CatalogCode from 
	
	 QSPCanadaProduct..Program_Master D
	INNER JOIN QSPCanadaProduct..ProgramSection C On C.CatalogCode= D.Code
	inner Join QSPCanadaProduct..Pricing_Details PD on C.Id = PD.ProgramSectionId
	INNER JOIN QSPCanadaProduct..Product B ON PD.Product_Code = B.Product_Code
	where 
	PD.Pricing_year = @Year
	and B.product_year=@Year
	and PD.Pricing_season =@Season
	and b.product_season=@Season
		--AND D.Status IN ('30403', '30404')
	and b.product_code=@code
	and productline=@producttype-46000
	and pd.nbr_of_issues=@term
	and qsp_price=@price
	--and D.Lang=@Lang MS May 28, 2007 Staff recognition is in 'EN' only
	and D.Lang=   CASE Substring(@code,1,1) WHEN 'S' THEN D.Lang ELSE @Lang END
	and D.Season=@SeasonID
	and PD.TaxRegionID = @TaxRegionID
	order by b.product_code
end
else
begin
	-- no need to check term for non mag stuff
	select  distinct B.Product_Code,
		B.Product_Sort_Name,
		MagPrice_instance,
		@term as Term,
		Pd.qsp_price as Price,
		Pd.programsectionid as ProgramSection,
		pd.pricing_season,
		pd.Language_code,
		C.CatalogCode from 
	QSPCanadaCommon..CampaignToContentCatalog A
	INNER JOIN QSPCanadaProduct..Program_Master D ON D.Code LIKE A.Content_Catalog_Code
	INNER JOIN QSPCanadaProduct..ProgramSection C On C.CatalogCode= D.Code
	inner Join QSPCanadaProduct..Pricing_Details PD on C.Id = PD.ProgramSectionId
		INNER JOIN QSPCanadaProduct..Product B ON PD.Product_Code = B.Product_Code
	where 
A.CampaignID = @CampaignId
and 	 PD.Pricing_year = @Year
	and B.product_year=@Year
	and PD.Pricing_season =@Season
	and b.product_season=@Season
--		AND D.Status IN ('30403', '30404')
	and b.product_code=@code
	and productline=@producttype-46000
	and qsp_price=@price
	order by b.product_code
end
GO
