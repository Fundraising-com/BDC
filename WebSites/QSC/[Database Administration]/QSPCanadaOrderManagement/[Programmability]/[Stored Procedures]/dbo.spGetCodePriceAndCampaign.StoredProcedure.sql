USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spGetCodePriceAndCampaign]    Script Date: 06/07/2017 09:20:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[spGetCodePriceAndCampaign]

@Code varchar(20),
@price numeric(10,2),
@CampaignId int,
@ProductType int

AS

Declare @Season char(1)
Declare @Year int
Declare @Lang varchar(10)
Declare @ShipToAccountId int, @ShipToType Int, @TaxRegionId Int

SELECT 

	
	@Lang = Lang
	,@ShipToAccountId = isnull(BillToAccountId,0)
	,@ShipToType = isnull(SuppliesShipToCampaignContactId,2)
	
	
FROM
	QSPCanadaCommon..Campaign
WHERE
	Id = @CampaignId



Declare @now smalldatetime

SET @now = getDate()

SELECT 

	-- Ben - 2006-01-02 : Changed so that it keeps the products for one month after a season change
	@Year = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 7 THEN YEAR(CONVERT(smalldatetime,@now)) + 1
		WHEN MONTH(CONVERT(smalldatetime,@now)) <= 7 THEN YEAR(CONVERT(smalldatetime,@now))
		ELSE
			0
		END
	,@Season = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 7 OR MONTH(CONVERT(smalldatetime,@now)) = 1 THEN 'F'
		WHEN MONTH(CONVERT(smalldatetime,@now)) BETWEEN 2 AND 7 THEN 'S'
		ELSE ''
		END

--PRINT '@Season=' + Convert(varchar, @Season)
--PRINT '@Year=' + Convert(varchar, @Year)
--PRINT '@Lang=' + Convert(varchar, @Lang)
--print @ShipToAccountId
--print @ShipToType
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the Address To Send To and find TaxRegionId
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Street1 varchar(30), @Street2 varchar(30), @City varchar(30), @StateProvince varchar(30), @PostalCode varchar(10), @FirstName varchar(50), @LastName varchar(50)
DECLARE @Zip4 varchar(4), @Country varchar(20)

 IF @ShipToAccountId <> 0 
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

--PRINT '@TaxRegionId: ' + Convert(varchar, IsNull(@TaxRegionId, 0))

/*

select B.Product_Code,
	B.Product_Sort_Name,
	MagPrice_instance,
	PD.nbr_of_issues as Term,
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
where A.CampaignID = @CampaignId
and PD.Pricing_year = @Year
and B.product_year=@Year
and PD.Pricing_season =@Season
and b.product_season=@Season
--	AND D.Status IN ('30403', '30404')
and b.product_code=@code
and productline=@producttype-46000
and qsp_price=@price
	and D.Lang=@Lang
order by b.product_code
*/

select B.Product_Code,
		B.Product_Sort_Name,
		MagPrice_instance,
		PD.nbr_of_issues as Term,
		Pd.qsp_price as Price,
		Pd.programsectionid as ProgramSection,
		pd.pricing_season,
		pd.Language_code,
		C.CatalogCode from 
	QSPCanadaCommon..Campaign A,
	QSPCanadaProduct..ProgramSection C ,
	QSPCanadaProduct..Pricing_Details PD,
	 QSPCanadaProduct..Product B,
	 QSPCanadaProduct..Program_Master PM
	where A.ID = @CampaignId
		and PD.Pricing_year = @Year
		and B.product_Instance=PD.product_instance
		and PD.Pricing_season =@Season	
			--AND D.Status IN ('30403', '30404')
		and pd.product_code=@code
		and productline=@producttype-46000

	and qsp_price=@price
	and PD.TaxRegionID = @TaxRegionId
		--and A.Lang=PM.Lang  MS May 28,07  --Staff recognition program is Only EN
		and PM.Lang = CASE Substring(@code,1,1) WHEN 'S' THEN PM.Lang ELSE @Lang END
		and   C.Id = PD.ProgramSectionId
		and PM.Code=CatalogCode
		order by b.product_code
GO
