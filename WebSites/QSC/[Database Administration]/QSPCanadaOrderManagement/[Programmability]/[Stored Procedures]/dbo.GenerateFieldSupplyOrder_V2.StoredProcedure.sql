USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GenerateFieldSupplyOrder_V2]    Script Date: 06/07/2017 09:19:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
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

CREATE PROCEDURE [dbo].[GenerateFieldSupplyOrder_V2]

@CampaignId int,
@UserId int

AS


--- Add drop temp table code here

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will populate the @Season and @Year variables which will be used to limit the items picked.
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Declare @Season char(1)
Declare @Year int
Declare @Lang varchar(10), @FMId varchar(4)
Declare @ShipToAccountId int, @BillToAccountId int, @ShipToType Int, @TaxRegionId Int, @NumberOfStudents int, @NumberOfStaff int, @NumberOfClasses int

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
	,@BillToAccountId = BillToAccountId
	,@NumberOfStudents = NumberOfParticipants
	,@NumberOfStaff = NumberOfStaff
	,@NumberOfClasses = NumberOfClassroooms
	,@FMId = FMId
	
FROM
	QSPCanadaCommon..Campaign
WHERE
	Id = @CampaignId

PRINT '@Season=' + Convert(varchar, @Season)
PRINT '@Year=' + Convert(varchar, @Year)
PRINT '@Lang=' + Convert(varchar, @Lang)

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will grab all products for the campaign based on the content catalog code.
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
	,G.OracleCode As OracleCode
	,E.MagPrice_Instance As MagPrice_Instance
	
INTO
	#FSOrderItems
FROM
	QSPCanadaCommon..CampaignToContentCatalog A
	INNER JOIN QSPCanadaCommon..Program B ON A.ProgramId = B.Id
	INNER JOIN QSPCanadaCommon..Campaign C ON C.Id = A.CampaignId
	INNER JOIN QSPCanadaProduct..Pricing_Details E ON E.FSContent_Catalog_Code = A.Content_Catalog_Code
	INNER JOIN QSPCanadaProduct..ProgramSection D ON D.Id = E.ProgramSectionId
	INNER JOIN QSPCanadaProduct..Program_Master F ON F.Code LIKE E.FSContent_Catalog_Code
	INNER JOIN QSPCanadaProduct..Product G ON (G.Product_Code = E.Product_Code AND G.Product_Year = E.Pricing_Year AND G.Product_Season = E.Pricing_Season AND G.Lang = E.Language_Code AND G.OracleCode LIKE E.OracleCode)
WHERE
	E.FSProgram_Id = A.ProgramId
	AND A.CampaignId = @CampaignId
	AND E.Pricing_Year = @Year 
	AND E.Pricing_Season = @Season
	--AND F.Status in (30403,30404 )  
	--AND F.FSIsBrochure = 1 
	AND E.Language_Code = @Lang
	

SELECT * FROM #FSOrderItems


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will grab all products for the campaign that are not in a Content_Catalog_Code. 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO #FSOrderItems
	
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
	,G.OracleCode As OracleCode
	,E.MagPrice_Instance As MagPrice_Instance

FROM
	QSPCanadaCommon..CampaignProgram A
	INNER JOIN QSPCanadaCommon..Program B ON A.ProgramId = B.Id
	INNER JOIN QSPCanadaCommon..Campaign C ON C.Id = A.CampaignId
	INNER JOIN QSPCanadaProduct..ProgramSection D ON D.Program_Id = A.ProgramId
	INNER JOIN QSPCanadaProduct..Pricing_Details E ON E.ProgramSectionId = D.Id
	INNER JOIN QSPCanadaProduct..Program_Master F ON D.CatalogCode LIKE F.Code
	INNER JOIN QSPCanadaProduct..Product G ON (G.Product_Code = E.Product_Code AND G.Product_Year = E.Pricing_Year AND G.Product_Season = E.Pricing_Season AND G.Lang = E.Language_Code AND G.OracleCode LIKE E.OracleCode)
WHERE
	A.CampaignId = @CampaignId
	AND E.Pricing_Year = @Year 
	AND E.Pricing_Season = @Season
	--AND F.Status in (30403,30404 )  
	AND E.Language_Code = @Lang
	AND (E.FSContent_Catalog_Code IS NULL OR E.FSContent_Catalog_Code = '')
	AND E.FSProgram_Id = A.ProgramId

SELECT * FROM #FSOrderItems

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the Address To Send To and find TaxRegionId
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Street1 varchar(30), @Street2 varchar(30), @City varchar(30), @StateProvince varchar(30), @PostalCode varchar(10), @FirstName varchar(50), @LastName varchar(50)
DECLARE @Zip4 varchar(4), @Country varchar(20)

IF @ShipToType = 1
	BEGIN
		--- Populate Variables with FM data when that table becomes available
		SELECT
			@FirstName =  FirstName,
			@LastName = LastName
		FROM
			QSPCanadaCommon..FieldManager
		WHERE
			FMId = @FMId

		SELECT
			@Street1 = C.Street1,
			@Street2 = C.Street2,
			@City = C.City,
			@StateProvince = C.StateProvince,
			@PostalCode = C.Postal_Code,
			@Zip4 = C.Zip4,
			@Country = C.Country
			
		FROM
			QSPCanadaCommon..FieldManager A
			INNER JOIN QSPCanadaCommon..AddressList B ON A.AddressListId = B.Id
			INNER JOIN QSPCanadaCommon..Address C ON C.AddressListId = B.Id
		WHERE
			C.Address_Type = 1 		--- SHIP TO Address Type
			AND A.FMId = @FMId
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
			C.Address_Type = 1 		--- SHIP TO Address Type
			AND A.Id = @ShipToAccountId
	END
ELSE
	BEGIN
		SELECT 1
	END

PRINT '@Street1=' + @Street1 + ', @City = ' + @City + ', @StateProvince = ' + @StateProvince + ', @Country = ' + @Country

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the TaxRegionId And Update the temp table to weed out products that don't apply
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT @TaxRegionId = TaxRegionId FROM QSPCanadaCommon..TaxRegionProvince WHERE Province LIKE @StateProvince

PRINT '@TaxRegionId: ' + Convert(varchar, IsNull(@TaxRegionId, 0))

UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 1 WHERE #FSOrderItems.TaxRegionId = @TaxRegionId  OR #FSOrderItems.TaxRegionId = 0

--SELECT * FROM #FSOrderItems

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
	AND B.ADPCODE IS NOT NULL
	AND B.ADPCode <> ''

--- Next update extra_limit_rate for those items who are at 0 to 100
--UPDATE #FSOrderItems
--SET #FSOrderItems.FSExtra_Limit_Rate = 100
---WHERE
	--#FSOrderItems.FSExtra_Limit_Rate = 0
	--AND #FSOrderItems.IsValid = 1

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

------- SHIP TO INFORMATION
--- Insert Customer Instance
DECLARE @CustomerInstanceId int

IF @ShipToType = 1
	BEGIN
		--- Populate Variables with FM data when that table becomes available
		SELECT @Street1 = ''
	END
ELSE IF @ShipToType = 2
	BEGIN
		
		EXEC QSPCanadaOrderManagement..CreateCustomerAccount @ShipToAccountId, @UserId, @CustomerInstanceId OUTPUT

	END
ELSE
	BEGIN
		SELECT 1
	END

---- Create Order Header And Batch
DECLARE @OrderId int
DECLARE @CustomerOrderHeader int
DECLARE @OrderQualifierId varchar(10)
DECLARE @Now datetime
DECLARE @BatchDate varchar(10)

SET @Now = getdate()
SET @OrderQualifierId = '39001'
SET @BatchDate =  Convert(varchar,getdate(), 101)

EXEC QSPCanadaOrderManagement..CreateBatchAndOrderHeader @BatchDate, @BillToAccountId,  @ShipToAccountId,  @CampaignId, '40002', '41002', @OrderQualifierId, @CustomerInstanceId, @OrderId OUTPUT, @CustomerOrderHeader OUTPUT, @UserId

--- INSERT EACH PRODUCT
	
DECLARE @ProductCode varchar(50)
DECLARE @Quantity int
DECLARE @Price money
DECLARE @ProgramSectionId int
DECLARE @MagPrice_Instance int
DECLARE @TotalPrice money


DECLARE Detail CURSOR FOR
	SELECT
		OracleCode,
		Quantity,
		Price,
		ProgramSectionId,
		MagPrice_Instance
	FROM
		#FSOrderItems
	WHERE
		Quantity > 0
		AND IsValid = 1 
		AND IsValidApplicability = 1

OPEN Detail
	FETCH NEXT FROM Detail INTO
		@ProductCode,
		@Quantity,
		@Price,
		@ProgramSectionId,
		@MagPrice_Instance

WHILE @@Fetch_Status = 0
	BEGIN


		SELECT @TotalPrice = @Quantity * @Price


		EXEC CreateDetailItem 
			@BatchDate,
			@CustomerOrderHeader,
			@ProductCode,
			@Quantity,
			@TotalPrice,				-- Added Multiplication of Quantity per Karen T.  8/23/2004
			@ProgramSectionId,
			@Price,
			@Quantity,  -- Quantity Reserved
			'46004',
			@MagPrice_Instance,
			'500'	-- Status


		FETCH NEXT FROM Detail INTO
			@ProductCode,
			@Quantity,
			@Price,
			@ProgramSectionId,
			@MagPrice_Instance
	END

CLOSE Detail
DEALLOCATE Detail



SELECT *, @CustomerInstanceId As 'CustomerInstanceId', @OrderId As OrderId,  @CustomerOrderHeader As CustomerOrderHeader FROM #FSOrderItems WHERE IsValid = 1 AND IsValidApplicability = 1

DROP TABLE #FSOrderItems
GO
