USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GenerateFieldSupplyOrder]    Script Date: 06/07/2017 09:19:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
--	GenerateFieldSupplyOrder
--	Charles J Scally III  4/28/2004
--
--	This proc will generate a Field Supply Order for a given campaign.
--
--	Details:
--		-  Catalog Sections will be identified by the Brochure Table
--		-  Items are stored in the Pricing_Details table
--		-  A Brochure item is defined as an item that is Printed and Stored in a QSP Warehouse.
--		-  
--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

CREATE PROCEDURE [dbo].[GenerateFieldSupplyOrder]

@CampaignId int

AS


--- Add drop temp table code here

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will populate the @Season and @Year variables which will be used to limit the items picked.
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Declare @Season char(1)
Declare @Year int
Declare @Lang varchar(10)
Declare @ShipToAccountId int, @ShipToType Int, @TaxRegionId Int, @NumberOfStudents int, @NumberOfStaff int, @NumberOfClasses int

SELECT 

	@Year = CASE
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) > 6 THEN YEAR(CONVERT(smalldatetime,StartDate))+ 1
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) <= 6 THEN YEAR(CONVERT(smalldatetime,StartDate))
		ELSE
			0
		END
	,@Season = CASE
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) > 6 THEN 'F'
		WHEN MONTH(CONVERT(smalldatetime,StartDate)) <= 6 THEN 'S'
		ELSE ''
		END
	,@Lang = Lang
	,@ShipToAccountId = ShipToAccountId
	,@ShipToType = SuppliesShipToCampaignContactId
	,@NumberOfStudents = NumberOfParticipants
	,@NumberOfStaff = NumberOfStaff
	,@NumberOfClasses = NumberOfClassroooms
	
FROM
	QSPCanadaCommon..Campaign
WHERE
	Id = @CampaignId

PRINT '@Season=' + Convert(varchar, @Season)
PRINT '@Year=' + Convert(varchar, @Year)
PRINT '@Lang=' + Convert(varchar, @Lang)

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will grab all products that are in the Brochure table for the campaign.
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT
	 E.Product_Code As Product_Code
	,E.ProgramSectionId As ProgramSectionId
	,D.CatalogCode As Catalog_Code
	,@CampaignId As CampaignId
	,B.Id As ProgramId
	,E.FSIsBrochure As FSIsBrochure
	,E.FSDistributionLevelId As FSDistributionLevelId
	,FSApplicabilityId = CASE
		WHEN E.FSIsBrochure = 1 THEN 43100
		ELSE E.FSApplicabilityId
		END
	,QSP_Price As Price
	,FSExtra_Limit_Rate As FSExtra_Limit_Rate
	,F.Program_Id AS Program_Master
	,E.TaxRegionId As TaxRegionId
	,0 As IsValid -- Is Valid To be in order
	,0 As Quantity
	,G.Product_Name As ProductName
	,0 As IsValidApplicability
	
INTO
	#FSOrderItems
FROM
	QSPCanadaCommon..Brochure A
	INNER JOIN QSPCanadaCommon..Program B ON A.ProgramId = B.Id
	INNER JOIN QSPCanadaCommon..Campaign C ON C.Id = A.CampaignId
	INNER JOIN QSPCanadaProduct..ProgramSection D ON D.Id = A.ProgramSectionId
	INNER JOIN QSPCanadaProduct..Pricing_Details E ON E.ProgramSectionId = D.Id
	INNER JOIN QSPCanadaProduct..Program_Master F ON D.CatalogCode LIKE F.Code
	INNER JOIN QSPCanadaProduct..Product G ON (G.Product_Code = E.Product_Code AND G.Product_Year = E.Pricing_Year AND G.Product_Season = E.Pricing_Season AND G.Lang = E.Language_Code)
WHERE
	A.CampaignId = @CampaignId
	AND E.Pricing_Year = @Year 
	AND E.Pricing_Season = @Season
	AND A.ProgramMasterCode =  E.FSCatalog_Product_Code
	--AND F.Status in (30403,30404 )  
	--AND F.FSIsBrochure = 1 
	AND E.Language_Code = @Lang

SELECT * FROM #FSOrderItems

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the Address To Send To and find TaxRegionId
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Street1 varchar(30), @Street2 varchar(30), @City varchar(30), @StateProvince varchar(30), @PostalCode varchar(10)
DECLARE @Zip4 varchar(4), @Country varchar(20)

IF @ShipToType = 1
	BEGIN
		--- Populate Variables with FM data when that table becomes available
		SELECT @Street1 = ''
	END
ELSE IF @ShipToType = 2
	BEGIN
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
			C.Address_Type = 1 		--- SHIP TO Address Type
	END
ELSE
	BEGIN
		SELECT 1
	END

PRINT '@Street1=' + @Street1 + ', @StateProvince = ' + @StateProvince

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the TaxRegionId And Update the temp table to weed out products that don't apply
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT @TaxRegionId = TaxRegionId FROM QSPCanadaCommon..TaxRegionProvince WHERE Province LIKE @StateProvince

UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 1 WHERE #FSOrderItems.TaxRegionId = @TaxRegionId  OR #FSOrderItems.TaxRegionId = 0


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine applicability at the campaign level
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @IsCombo int

EXEC QSPCanadaCommon..Campaign_IsCombo_Check @CampaignId, @IsCombo OUTPUT



------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will calculate the quantity of each item
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--- First update the extra_limit_rate for those items who have a null extra_limit_rate based on the distribution level
UPDATE #FSOrderItems
SET #FSOrderItems.FSExtra_Limit_Rate = Convert(int, B.ADPCODE)
FROM
	#FSOrderItems
	INNER JOIN QSPCanadaProduct..CodeDetail B ON B.ADPCODE = #FSOrderItems.FSApplicabilityId
WHERE
	#FSOrderItems.FSExtra_Limit_Rate IS NULL

--- Next update extra_limit_rate for those items who are at 0 to 100
UPDATE #FSOrderItems
SET #FSOrderItems.FSExtra_Limit_Rate = 100
WHERE
	#FSOrderItems.FSExtra_Limit_Rate = 0
	AND #FSOrderItems.IsValid = 1

--- First update the extra_limit_rate for those items who have a null extra_limit_rate based on the distribution level
UPDATE #FSOrderItems
SET #FSOrderItems.Quantity =
	Case
		WHEN FSDistributionLevelId = 44030 THEN 	-- Campaign Level
			Round(1 * (FSExtra_Limit_Rate/100), 0)
		WHEN FSDistributionLevelId = 44031 THEN 	-- Class Level
			Round(@NumberOfClasses * (FSExtra_Limit_Rate/100), 0)
		WHEN FSDistributionLevelId = 44032 THEN 	-- Student Level
			Round((@NumberOfStudents + @NumberOfStaff) * (FSExtra_Limit_Rate/100), 0)
	END
FROM
	#FSOrderItems
	INNER JOIN QSPCanadaProduct..CodeDetail B ON B.Instance = #FSOrderItems.FSApplicabilityId
WHERE
	#FSOrderItems.FSExtra_Limit_Rate > 0
	AND IsValid = 1


---- Apply the applicability rules
If @IsCombo = 1 
	BEGIN
		UPDATE
			#FSOrderItems
		SET
			IsValidApplicability = 1
		WHERE
			FSIsBrochure = 0
			AND FSApplicabilityId IN (43100, 43200)
			AND IsValid = 1
	END
Else
	BEGIN
		UPDATE
			#FSOrderItems
		SET
			IsValidApplicability = 1
		WHERE
			FSIsBrochure = 0
			AND FSApplicabilityId IN (43100, 43300)
			AND IsValid = 1
	END

--- IsBrochure Items autmatically added
UPDATE
	#FSOrderItems
SET
	IsValidApplicability = 1
WHERE
	FSIsBrochure = 1
	AND IsValid = 1


SELECT * FROM #FSOrderItems WHERE IsValid = 1 AND IsValidApplicability = 1

DROP TABLE #FSOrderItems
GO
