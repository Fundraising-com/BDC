USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GenerateFieldSupplyOrder_V6]    Script Date: 06/07/2017 09:19:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--set nocount on
--drop table #FSOrderItems
CREATE         Procedure [dbo].[GenerateFieldSupplyOrder_V6] @CampaignID	Int,
							 @UserId 	Varchar(4),
							 @DebugMode 	Int,
							 @GenerateOrder Int
As
Declare	@OnHandQty int,
	@OracleCode varchar(50)


Declare @FSProvince varchar(20)
Declare @FSMagProvinceCount int
Declare @Season char(1)
Declare @Year int
Declare @Lang varchar(10), @FMId varchar(4)
Declare @ShipToAccountId int, @BillToAccountId int, @ShipToType Int, @TaxRegionId Int, @NumberOfStudents int, @NumberOfStaff int, @NumberOfClasses int
Declare @ShipToAccountProvince varchar(10)
Declare @IsStaff int
Declare @RequestedDeliveryDate datetime

Declare @ContactFirst varchar(50)
Declare @ContactLast varchar(50)
Declare @ContactPhone varchar(50)

Declare @Extra1Ups Int
Declare @ExtraGiftForm Int
Declare @ExtraMagBrochure Int
Declare @CoolCardsBoxes Int
Declare @OnlineOnly Int

select  @ContactFirst =''
select  @ContactLast = ''
select @IsStaff = 0

if(@DebugMode = 0)
begin
set nocount on
end
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
	,@IsStaff = IsStaffOrder
	,@Extra1Ups=Extra_1Ups
	,@ExtraGiftForm=Extra_GiftForm
	,@ExtraMagBrochure=Extra_MagBrochure
	,@CoolCardsBoxes=CoolCardsBoxes
	,@OnlineOnly= ISNULL(OnlineOnlyPrograms,0)
	,@RequestedDeliveryDate = SuppliesDeliveryDate
FROM
	QSPCanadaCommon..Campaign
WHERE
	Id = @CampaignID

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the Address To Send To and find TaxRegionId
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Street1 varchar(30), @Street2 varchar(30), @City varchar(30), @StateProvince varchar(30), @PostalCode varchar(10), @FirstName varchar(50), @LastName varchar(50)
DECLARE @Zip4 varchar(4), @Country varchar(20)

IF @ShipToType = 63001
	BEGIN
		if(@DebugMode=1)
		begin
			print 'Shipping to FM'
		end
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
			C.Address_Type = 54004 		--- SHIP TO Address Type
			AND A.FMId = @FMId
		
		select  @ContactFirst =@FirstName
		select  @ContactLast = @LastName
--		Declare @ContactPhone varchar(50)

		SELECT	@ContactPhone = ph.PhoneNumber
		FROM	QSPCanadaCommon..FieldManager fm
		JOIN	QSPCanadaCommon..Phone ph ON ph.PhoneListID = fm.PhoneListID AND ph.Type = 30501
		WHERE	fm.FMID = @FMID

		if(@DebugMode=1)
		begin
			SELECT
				*
				
			FROM
				QSPCanadaCommon..FieldManager A
				INNER JOIN QSPCanadaCommon..AddressList B ON A.AddressListId = B.Id
				INNER JOIN QSPCanadaCommon..Address C ON C.AddressListId = B.Id
			WHERE
				C.Address_Type = 54004 		--- SHIP TO Address Type
				AND A.FMId = @FMId
		End

	END
ELSE IF @ShipToType = 63002
	BEGIN
		SELECT
			@FirstName =  '',
			@LastName = [Name]
		FROM
			QSPCanadaCommon..CAccount
		WHERE
			Id = @ShipToAccountId

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


		-- Grab contact info
		select @ContactFirst=firstname,@ContactLast= lastname
				 from  qspcanadacommon..campaign campaign, qspcanadacommon..contact c
				where 						
				 c.ID = campaign.ShipToCampaignContactID 
					and campaign.ID = @CampaignID

		-- and Phone
		select @ContactPhone=phonenumber
			from 
			qspcanadacommon..phone p, qspcanadacommon..caccount a, qspcanadacommon..campaign c 			
			where a.PhoneListID = p.PhoneListID and c.ShipToAccountID = a.ID 
				and c.id = @CampaignID
	END
ELSE
	BEGIN
		select 
			@FirstName = FirstName,
			@LastName = LastName,
			@Street1 = A.Street1,
			@Street2 = A.Street2,
			@City = A.City,
			@StateProvince =A.StateProvince,
			@PostalCode = A.Postal_Code,
			@Zip4 = A.Zip4,
			@Country = A.Country,
			@ContactFirst = C.FirstName,
			@ContactLast = C.LastName
			 from QSPCanadaCommon..Contact C,QSPCanadaCommon..Campaign as Campaign,				
				QSPCanadaCommon..Address  A where A.address_id = AddressID
				and SuppliesCampaignContactID=C.ID and Campaign.Id = @CampaignID
	END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the TaxRegionId 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT @TaxRegionId = TaxRegionId FROM QSPCanadaCommon..TaxRegionProvince,
			QSPCanadaCommon..CAccount A
			INNER JOIN QSPCanadaCommon..AddressList B ON A.AddressListId = B.Id
			INNER JOIN QSPCanadaCommon..Address C ON C.AddressListId = B.Id
		WHERE
			C.Address_Type = 54001 		--- SHIP TO Address Type
	 		And Province LIKE C.StateProvince
			and A.ID = @ShipToAccountId


Select  @ShipToAccountProvince=C.StateProvince from
	QSPCanadaCommon..CAccount A
			INNER JOIN QSPCanadaCommon..AddressList B ON A.AddressListId = B.Id
			INNER JOIN QSPCanadaCommon..Address C ON C.AddressListId = B.Id
		WHERE
			C.Address_Type = 54001 		--- SHIP TO Address Type
			and A.ID = @ShipToAccountId

if(@DebugMode = 1)
begin
	PRINT '@Street1=' + @Street1 + ', @City = ' + @City + ', @StateProvince = ' + @StateProvince + ', @Country = ' + @Country
	PRINT '@TaxRegionId: ' + Convert(varchar, IsNull(@TaxRegionId, 0))
	PRINT '@ShipToType: ' + Convert(varchar, IsNull(@ShipToType, 0))
		print 'Contacts:' + @ContactFirst + ' ' + @ContactLast

	--Select @ShipToType=2
	PRINT '@Season=' + Convert(varchar, @Season)
	PRINT '@Year=' + Convert(varchar, @Year)
	PRINT '@Lang=' + Convert(varchar, @Lang)
	print '@ShipToType=' + Convert(varchar, @ShipToType)
	print '@NumberOfStudents=' + Convert(varchar, @NumberOfStudents)
	print '@NumberOfStaff=' + Convert(varchar, @NumberOfStaff)

	print '@FMID=' + Convert(varchar, @FMID)
End

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
	,E.FSApplicabilityId/*FSApplicabilityId = CASE
		WHEN E.FSIsBrochure = 1 THEN 43100
		ELSE E.FSApplicabilityId
		END*/
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
	,IsNull(E.FSProvinceCode, ' ') as FSProvinceCode
	,F.Program_Type AS CatalogContentName
Into #FSOrderItems
FROM
	QSPCanadaCommon..CampaignToContentCatalog A
	INNER JOIN QSPCanadaCommon..Program B ON A.ProgramId = B.Id
	INNER JOIN QSPCanadaCommon..Campaign C ON C.Id = A.CampaignId
	INNER JOIN QSPCanadaCommon..CampaignProgram CP on CP.Campaignid = A.CampaignId
	INNER JOIN QSPCanadaProduct..Pricing_Details E ON E.FSContent_Catalog_Code = A.Content_Catalog_Code
	INNER JOIN QSPCanadaProduct..ProgramSection D ON D.Id = E.ProgramSectionId
	INNER JOIN QSPCanadaProduct..Program_Master F ON F.Code LIKE E.FSContent_Catalog_Code
	INNER JOIN QSPCanadaProduct..Product G ON (G.Product_instance = E.Product_instance)
	INNER JOIN QSPCanadaProduct..Program_Master H ON H.Program_ID = D.Program_ID
	INNER JOIN QSPCanadaCommon..Season S ON S.ID = H.Season
WHERE
	E.FSProgram_Id = A.ProgramId
	AND A.CampaignId = @CampaignId
	AND E.Pricing_Year = @Year 
	AND (E.Pricing_Season = @Season	OR S.Season = 'Y')
	AND E.Language_Code like '%'+ @Lang+'%'
--	AND F.Lang=@Lang
	and E.FSIsBrochure = 1
	and A.ProgramID=CP.ProgramiD
	and (E.TaxRegionId = @TaxRegionId OR E.TaxRegionID = 0)
	And cp.DeletedTF = 0
	AND cp.OnlineOnly = 0
	And D.type <> 7 	--Program section for FM kanata Invoicing, MS Oct 26, 2005
	AND E.Status IN (30600)
	AND	G.Status IN (30600)
	AND D.CatalogCode NOT LIKE 'FSFM%'

if(@DebugMode=1)
begin
	SELECT * FROM #FSOrderItems
end

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will grab all products for the campaign that are not in a Content_Catalog_Code. 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO #FSOrderItems
SELECT
	 E.Product_Code As Product_Code
	,E.ProgramSectionId As ProgramSectionId
	,D.CatalogCode As Catalog_Code
	, CampaignId
	,B.Id As ProgramId
	,E.FSIsBrochure As FSIsBrochure
	,E.FSDistributionLevelId As FSDistributionLevelId
	,E.FSApplicabilityId/*FSApplicabilityId = CASE
		WHEN E.FSIsBrochure = 1 THEN 43100
		ELSE E.FSApplicabilityId
		END*/
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
	, ' ' -- as placeholder
	,'' AS CatalogContentName

FROM
	QSPCanadaCommon..CampaignProgram A,
	QSPCanadaCommon..Program B,
	QSPCanadaCommon..Campaign C ,
	QSPCanadaProduct..ProgramSection D ,
 	QSPCanadaProduct..Pricing_Details E ,
	QSPCanadaProduct..Program_Master F ,
	QSPCanadaProduct..Product G,
	QSPCanadaProduct..ProgFSSectionMap H,
	QSPCanadaProduct..Program_Master I,
	QSPCanadaCommon..Season S
WHERE
	A.CampaignId = @CampaignId
	AND E.Pricing_Year = @Year 
	AND (E.Pricing_Season = @Season	OR S.Season = 'Y')
	AND F.Lang=@Lang
	AND E.Language_Code  like '%'+ @Lang+'%'
	AND (E.FSContent_Catalog_Code IS NULL OR E.FSContent_Catalog_Code = '')
	AND E.FSProgram_Id = A.ProgramId
	and A.ProgramId = B.Id
	and  C.Id = A.CampaignId
	and  H.Program_ID = E.FSProgram_Id
	and  D.Id = H.Catalog_section_id
	and  E.ProgramSectionId = D.Id
	and  D.CatalogCode LIKE F.Code
	and E.Product_instance=G.Product_Instance
	and I.Program_ID = D.Program_ID
	and S.ID = I.Season
	And DeletedTF = 0
	AND A.OnlineOnly = 0
	And D.type <> 7 	--Program section for FM kanata Invoicing, MS Oct 26, 2005
	AND E.Status IN (30600)
	AND	G.Status IN (30600)
	AND D.CatalogCode NOT LIKE 'FSFM%'


if(@DebugMode=1)
begin
	SELECT * FROM #FSOrderItems
end

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Make sure tax and language are the right ones
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 1 WHERE #FSOrderItems.TaxRegionId = @TaxRegionId  OR #FSOrderItems.TaxRegionId = 0

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine  if any special province thing exclude those that aren't in this campaign province
--  as well as checking inventory for those
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--Disclude any items that are for a different Province
UPDATE	#FSOrderItems
SET		#FSOrderItems.IsValid = 0 
WHERE	FSProvinceCode <> ' ' 
AND		FSProvinceCode <> QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince(@CampaignID)

/*
--Ensure there is enough Inventory
UPDATE		fsoi
SET			IsValid = 0
FROM		#FSOrderItems fsoi
LEFT JOIN	QSPCanadaProduct..InventoryLevel il
				ON	il.InventoryLevelID =  (SELECT		TOP 1
														il2.InventoryLevelID
											FROM		QSPCanadaProduct..InventoryLevel il2
											WHERE		il2.ProductCode = fsoi.Product_Code
											AND			il2.StatusID = 2 --2:Active
											AND			il2.EffectiveDate <= GETDATE()
											ORDER BY	il2.EffectiveDate DESC)
WHERE		fsoi.FSIsBrochure = 1
AND			ISNULL(il.QtyAvailable, 0) >= fsoi.Quantity
*/

--If out of provincial magazine brochures, give them the non-provincial magazine brochure
SELECT	@FSMagProvinceCount = COUNT(*)
FROM	#FSOrderItems fsoi
WHERE	fsoi.IsValid = 1
AND		fsoi.FSIsBrochure = 1
AND		fsoi.FSProvinceCode = QSPCanadaCommon.dbo.FNC_GetCampaignShipToProvince(@CampaignID)
AND		fsoi.ProgramID IN (1, 2) --1: Magazine, 2: Magazine Express

IF @FSMagProvinceCount = 0
BEGIN
	UPDATE	fsoi
	SET		fsoi.IsValid = 1
	FROM	#FSOrderItems fsoi
	WHERE	fsoi.FSIsBrochure = 1
	AND		fsoi.FSProvinceCode = ' '
	AND		fsoi.ProgramID IN (1, 2) --1: Magazine, 2: Magazine Express

	UPDATE	fsoi
	SET		fsoi.IsValid = 0
	FROM	#FSOrderItems fsoi
	WHERE	fsoi.FSIsBrochure = 1
	AND		fsoi.FSProvinceCode <>  ' '
	AND		fsoi.ProgramID IN (1, 2) --1: Magazine, 2: Magazine Express
END
ELSE
BEGIN
	UPDATE	fsoi
	SET		fsoi.IsValid = 0
	FROM	#FSOrderItems fsoi
	WHERE	fsoi.FSIsBrochure = 1
	AND		fsoi.FSProvinceCode = ' '
	AND		fsoi.ProgramID IN (1, 2) --1: Magazine, 2: Magazine Express
END

--Store if certain programs are being run

DECLARE @RunningMagazine BIT
SELECT	@RunningMagazine = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram
WHERE	CampaignID = @CampaignID
AND		DeletedTF = 0
AND		OnlineOnly = 0
AND		ProgramID IN (1, 2)

SET @RunningMagazine = ISNULL(@RunningMagazine, 0)

/*DECLARE @RunningEntertainment BIT
SELECT	@RunningEntertainment = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram
WHERE	CampaignID = @CampaignID
AND		DeletedTF = 0
AND		ProgramID IN (52)

SET @RunningEntertainment = ISNULL(@RunningEntertainment, 0)*/

DECLARE @Running59MinuteFundraiser BIT
SELECT	@Running59MinuteFundraiser = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram
WHERE	CampaignID = @CampaignID
AND		DeletedTF = 0
AND		OnlineOnly = 0
AND		ProgramID IN (51)

SET @Running59MinuteFundraiser = ISNULL(@Running59MinuteFundraiser, 0)

/*DECLARE @BlackboardPacket BIT
SELECT	@BlackboardPacket = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram
WHERE	CampaignID = @CampaignID
AND		DeletedTF = 0
AND		BlackboardPacket = 1

SET @BlackboardPacket = ISNULL(@BlackboardPacket, 0)*/

DECLARE @MagPacket BIT
SELECT	@MagPacket = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram cp
JOIN	QSPCanadaCommon..Campaign c ON c.ID = cp.CampaignID
WHERE	cp.CampaignID = @CampaignID
AND		cp.DeletedTF = 0
AND		cp.OnlineOnly = 0
AND		cp.ProgramID IN (1, 2)
AND		c.OnlineOnlyPrograms = 0

SET @MagPacket = ISNULL(@MagPacket, 0)

DECLARE @CDPacket BIT
SELECT	@CDPacket = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram cp
JOIN	QSPCanadaCommon..Campaign c ON c.ID = cp.CampaignID
WHERE	cp.CampaignID = @CampaignID
AND		cp.DeletedTF = 0
AND		cp.OnlineOnly = 0
AND		cp.ProgramID = 44
AND		cp.FieldSupplyPacket = 1
AND		c.OnlineOnlyPrograms = 0

SET @CDPacket = ISNULL(@CDPacket, 0)

DECLARE @GWLPacket BIT
SELECT	@GWLPacket = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram cp
JOIN	QSPCanadaCommon..Campaign c ON c.ID = cp.CampaignID
WHERE	cp.CampaignID = @CampaignID
AND		cp.DeletedTF = 0
AND		cp.OnlineOnly = 0
AND		cp.ProgramID = 53
AND		cp.FieldSupplyPacket = 1
AND		c.OnlineOnlyPrograms = 0

SET @GWLPacket = ISNULL(@GWLPacket, 0)

DECLARE @RunningGiftsWeLove BIT
SELECT	@RunningGiftsWeLove = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram
WHERE	CampaignID = @CampaignID
AND		DeletedTF = 0
AND		OnlineOnly = 0
AND		ProgramID IN (53)

SET @RunningGiftsWeLove = ISNULL(@RunningGiftsWeLove, 0)

DECLARE @RunningTRT BIT
SELECT	@RunningTRT = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram
WHERE	CampaignID = @CampaignID
AND		DeletedTF = 0
AND		OnlineOnly = 0
AND		ProgramID IN (50)

SET @RunningTRT = ISNULL(@RunningTRT, 0)

DECLARE @RunningCookieDough BIT
SELECT	@RunningCookieDough = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram
WHERE	CampaignID = @CampaignID
AND		DeletedTF = 0
AND		OnlineOnly = 0
AND		ProgramID IN (44)

SET @RunningCookieDough = ISNULL(@RunningCookieDough, 0)

DECLARE @RunningFestival BIT
SELECT	@RunningFestival = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram
WHERE	CampaignID = @CampaignID
AND		DeletedTF = 0
AND		OnlineOnly = 0
AND		ProgramID IN (54)

SET @RunningFestival = ISNULL(@RunningFestival, 0)

DECLARE @RunningCumulative BIT
SELECT	@RunningCumulative = CONVERT(BIT, 1)
FROM	QSPCanadaCommon..CampaignProgram
WHERE	CampaignID = @CampaignID
AND		DeletedTF = 0
AND		OnlineOnly = 0
AND		ProgramID IN (42)

SET @RunningCumulative = ISNULL(@RunningCumulative, 0)

/*--Remove any Gift Packets if there are any Magazine Packets already in Order

IF (@RunningMagazine = 1 AND @RunningGiftsWeLove = 0)
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1065823')
END*/

/*--Entertainment Exceptions

IF (@RunningEntertainment = 1)
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1062481','1062483','1062485','1062487')
END
ELSE
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1062480','1062482','1062484','1062486')
END

IF (@RunningEntertainment = 1)
BEGIN
	IF (@RunningMagazine = 1 AND @Lang = 'EN')
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1061703','1062308')
	END
	
	IF (@Running59MinuteFundraiser = 1)
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1062308')
	END
	ELSE
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1061703')
	END
	
	IF ((@RunningEmbrace = 1 OR @RunningFestival = 1 OR @RunningCookieDough = 1) AND @RunningMagazine = 0 AND @RunningTRT = 0)
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1056957')
	END
END*/

/*--Blackboard Exceptions

IF (@BlackboardPacket = 1)
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1062480', '1062481', '1062484', '1062485','1062486','1062487','1062488')
END
ELSE
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1062482', '1062483')
END*/

--Packet Exceptions

IF (@RunningMagazine = 1)
BEGIN
	IF (@RunningCumulative = 1)
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1073190')
	END
	ELSE
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1073199','1066386')	
	END
END

IF (@MagPacket = 1 AND @Lang = 'EN' AND @FSMagProvinceCount = 0)
BEGIN
	--UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	--WHERE #FSOrderItems.product_code IN ('1071691')
	
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1073181','1073181')	
END

IF (@MagPacket = 1 AND (@RunningGiftsWeLove = 0 OR @GWLPacket = 1))
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1073187')
END

IF (@CDPacket = 1 AND @Lang = 'EN')
BEGIN
	IF (@MagPacket = 1 AND (@RunningGiftsWeLove = 0 OR @GWLPacket = 1))
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1073187','1073197','1073198')
	END
	ELSE
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1056958','1056959','1073187','1073121')
	END
END

IF (@CDPacket = 1 AND @GWLPacket = 0 AND @Lang = 'EN')
BEGIN
	IF (@RunningCumulative = 1)
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1073198')
		
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1073181','1073181')	
	END
	ELSE
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1073197')
	END
END
ELSE
BEGIN
	IF (@CDPacket = 1 AND @Lang = 'EN')
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1056958')
	END
	
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1073198', '1073197','1073187', '1073188')
END

/*IF (@GWLPacket = 1)
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1056957','1069710','1071465','1056958','1056959')
	
	IF (@RunningCumulative = 1)
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1070045')
		
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1069724','1069724')	
	END
	ELSE
	BEGIN
		UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
		WHERE #FSOrderItems.product_code IN ('1070043')
	END
END
ELSE
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1070045','1070043')
END*/

--Remove Magazine Items if running Gifts We Love

IF (@RunningGiftsWeLove = 1)
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.ProgramId IN (1, 2)
END

--59 Minute Fundraiser Exceptions

IF (@Running59MinuteFundraiser = 1)
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1056957')
END

/*--CC Envelope Exceptions

IF (@RunningMagazine = 1 OR @RunningCookieDough = 1 OR @RunningEmbrace = 1 OR @RunningFestival = 1)
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1054055')
END*/

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  Online Only + Staff
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @IsStaff = 1
BEGIN
	UPDATE	#FSOrderItems
	SET		#FSOrderItems.IsValid = 0
	WHERE	#FSOrderItems.CatalogContentName NOT LIKE 'STAFF%'

	UPDATE	#FSOrderItems
	SET		#FSOrderItems.IsValid = 1
	WHERE	#FSOrderItems.CatalogContentName LIKE 'STAFF%'
END

IF (@OnlineOnly = 1 AND @IsStaff = 0)
BEGIN

	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code NOT IN ('1073179','1073180','1071465','1066890','1070914','1064909','1064910','1073181','1073174','1073178','1073121','1065192','1072076','1072077','1073122','1072682','1072902','1072852')
	
	IF (@RunningMagazine = 1)
	BEGIN
		UPDATE	#FSOrderItems
		SET		#FSOrderItems.IsValid = 1
		WHERE	#FSOrderItems.product_code IN ('1073179')
	END
END
/*ELSE IF @OnlineOnly = 0
BEGIN

	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 
	WHERE #FSOrderItems.product_code IN ('1061638')
	
END*/

IF @OnlineOnly = 0
BEGIN
	UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0
	WHERE #FSOrderItems.product_code IN ('1073179','1073180')
END

if(@DebugMode=1)
begin
Select * from #FSOrderItems
end
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine applicability at the campaign level
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @IsCombo int

EXEC QSPCanadaCommon..Campaign_IsCombo_Check @CampaignId, @IsCombo OUTPUT


if(@DebugMode=1)
begin
Print '@IsCombo: ' + Convert(varchar,@IsCombo)
end

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will calculate the quantity of each item
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--- First update the extra_limit_rate for those items who have a null extra_limit_rate based on the distribution level
UPDATE #FSOrderItems
SET #FSOrderItems.FSExtra_Limit_Rate = Convert(int, B.ADPCODE)
FROM
	#FSOrderItems
	INNER JOIN QSPCanadaCommon..CodeDetail B ON B.Instance = #FSOrderItems.FSDistributionLevelId -- KT fixed this 4/1/05
WHERE
	#FSOrderItems.FSExtra_Limit_Rate IS NULL
	AND B.ADPCODE IS NOT NULL
	AND B.ADPCode <> ''

--1UP Quantity Exception

/*IF ((/*@RunningEntertainment = 1 OR*/ @RunningTRT = 1) AND (@RunningMagazine = 1) AND (@RunningGiftsWeLove = 0))
BEGIN
	UPDATE	#FSOrderItems
	SET		#FSOrderItems.FSExtra_Limit_Rate = 25
	WHERE	Product_Code = '1056957'
END*/

if(@IsStaff = 0)
begin

	UPDATE #FSOrderItems
	SET #FSOrderItems.Quantity =
		Case
			WHEN FSDistributionLevelId = 44031 THEN 	-- Campaign Level
				Round(1 * (FSExtra_Limit_Rate/100), 0)
			WHEN FSDistributionLevelId = 44032 THEN 	-- Class Level
				Round(@NumberOfClasses * (FSExtra_Limit_Rate/100), 0)
			WHEN FSDistributionLevelId = 44033 THEN 	-- Student Level
				Round((@NumberOfStudents) * (FSExtra_Limit_Rate/100), 0)
		END
	FROM
		#FSOrderItems
	--	INNER JOIN QSPCanadaProduct..CodeDetail B ON B.Instance = #FSOrderItems.FSDistributionLevelId
	WHERE
		#FSOrderItems.FSExtra_Limit_Rate > 0
		AND IsValid = 1
end
else
begin

	UPDATE #FSOrderItems
	SET #FSOrderItems.Quantity =Round(@NumberOfStaff * (FSExtra_Limit_Rate/100), 0)
		
	FROM
		#FSOrderItems
	--	INNER JOIN QSPCanadaProduct..CodeDetail B ON B.Instance = #FSOrderItems.FSDistributionLevelId
	WHERE
		#FSOrderItems.FSExtra_Limit_Rate > 0
		AND IsValid = 1

end

----Jun 24 2008 MS ------
-- Increase 1Ups and Gift Form by extra quantity supplied
-- Only online brochure if the CA is Online Program Only

IF (@Extra1Ups > 0)
BEGIN
	UPDATE	#FSOrderItems 
	SET		#FSOrderItems.Quantity=#FSOrderItems.Quantity+IsNull(@Extra1Ups,0),
			#FSOrderItems.IsValid = 1
	WHERE	#FSOrderItems.product_code IN ('1056957')
END

IF (@ExtraGiftForm > 0)
BEGIN
	UPDATE	#FSOrderItems 
	SET		#FSOrderItems.Quantity=#FSOrderItems.Quantity+IsNull(@ExtraGiftForm,0),
			#FSOrderItems.IsValid = 1
	WHERE	#FSOrderItems.product_code IN ('1056958', '1056959')
END

IF (@ExtraMagBrochure > 0)
BEGIN
	IF (@RunningGiftsWeLove = 1)
	BEGIN
		UPDATE	#FSOrderItems 
		SET		#FSOrderItems.IsValid = 1, #FSOrderItems.Quantity=#FSOrderItems.Quantity+IsNull(@ExtraMagBrochure,0)
		WHERE	#FSOrderItems.product_code IN ('1071465')
	END
	ELSE
	BEGIN
		UPDATE	#FSOrderItems 
		SET		#FSOrderItems.IsValid = 1
		WHERE	#FSOrderItems.product_code IN ('1073179','1073180')

		UPDATE	#FSOrderItems 
		SET		#FSOrderItems.Quantity=#FSOrderItems.Quantity+IsNull(@ExtraMagBrochure,0)
		WHERE	#FSOrderItems.product_code IN ('1073179','1073180')--('1065211','1065212')--,'1066390','1066387','1066385','1066389')
	END
END

IF (@CoolCardsBoxes > 0)
BEGIN
	UPDATE	#FSOrderItems 
	SET		#FSOrderItems.Quantity=#FSOrderItems.Quantity+IsNull(@CoolCardsBoxes,0),
			#FSOrderItems.IsValid = 1
	WHERE	#FSOrderItems.OracleCode IN ('1075882')
END

---- Apply the applicability rules
If @IsCombo = 1 
	BEGIN

		UPDATE
			#FSOrderItems
		SET
			IsValidApplicability = 1
		WHERE
			--FSIsBrochure = 0
			FSApplicabilityId IN (43101, 43102)
			AND IsValid = 1
	END
Else
	BEGIN
		UPDATE
			#FSOrderItems
		SET
			IsValidApplicability = 1
		WHERE
			--FSIsBrochure = 0
			FSApplicabilityId IN (43101, 43103)
			AND IsValid = 1
	END

/*UPDATE
	#FSOrderItems
SET
	IsValidApplicability = 1
WHERE
	FSIsBrochure = 1
	AND IsValid = 1*/

UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 0 from  #FSOrderItems, QSPCanadaProduct..Program_Master PM, QSPCanadaProduct..ProgramSection  PS
		where #FSOrderItems.ProgramSectionID = PS.ID AND PS.CatalogCode = PM.Code
			and PM.Lang <> @Lang

--Remove 'duplicates' that came from multiple catalog sections
UPDATE	fsoi
SET		Isvalid = 0
FROM	#FSOrderItems fsoi
WHERE	MagPrice_Instance IN
(
	SELECT		fsoi1.MagPrice_Instance
	FROM		#FSOrderItems fsoi1
	JOIN		#FSOrderItems fsoi2 
					ON	fsoi1.ProgramSectionId < fsoi2.ProgramSectionID
					AND	fsoi1.Product_Code = fsoi2.Product_Code
	WHERE		fsoi1.IsValid = 1
	AND			fsoi2.IsValid = 1
)

------------ 

if(@DebugMode=1)
begin
Select * from #FSOrderItems
print 'Contacts:' + @ContactFirst + ' ' + @ContactLast+ ' ' +@ContactPhone
end

if( @Generateorder = 1)
Begin
	------- SHIP TO INFORMATION
	--- Insert Customer Instance
	DECLARE @CustomerInstanceId int
	
	IF @ShipToType = 63001
		BEGIN
			--- Populate Variables with FM data when that table becomes available
			EXEC QSPCanadaOrderManagement..CreateCustomerFM @FMId, @UserId, 54004,@CustomerInstanceId OUTPUT		
		END
	ELSE IF @ShipToType = 63002
		BEGIN
			
			EXEC QSPCanadaOrderManagement..CreateCustomerAccount @ShipToAccountId, @UserId, @CustomerInstanceId OUTPUT	
		END
	ELSE
		BEGIN
			EXEC QSPCanadaOrderManagement..CreateCustomer 
				@FirstName ,
				@LastName ,
				@Street1 ,
				@Street2 ,
				@City ,
				'', --County
				@StateProvince ,
				@PostalCode ,
				'', --Postal2
				'', --Email
				@UserId, @CustomerInstanceId OUTPUT	
		END

	Update Customer set Address1=@Street1, Address2=@Street2, City=@City, State = @StateProvince, zip=@PostalCode,
		FirstName = @ContactFirst, LastName = @ContactLast
		where instance = @CustomerInstanceId
	
	---- Create Order Header And Batch dummying Student And Teacher
	DECLARE @OrderId int
	DECLARE @CustomerOrderHeader int
	DECLARE @OrderQualifierId varchar(10)
	DECLARE @Now datetime
	DECLARE @BatchDate varchar(10)
	declare @teacherinstance int
	declare @studentinstance int
	Declare @teachercount int


	
	SET @Now = getdate()
	SET @OrderQualifierId = '39007'
	SET @BatchDate =  Convert(varchar,getdate(), 101)	


	EXEC QSPCanadaOrderManagement..CreateBatchAndOrderHeader @BatchDate, @BillToAccountId,  @ShipToAccountId,  @CampaignId, '40002', '41002', @OrderQualifierId, @CustomerInstanceId, @OrderId OUTPUT, @CustomerOrderHeader OUTPUT, @UserId

	select @teachercount=count(*) from teacher
		where accountid = @BillToAccountId
			and lastname='ZZ' and classroom='ZZ'

	if(@teachercount <> 0)
	begin
		select @teacherinstance=instance from teacher
		where accountid = @BillToAccountId
			and lastname='ZZ' and classroom='ZZ'
	end
	else
	begin		
		exec CreateTeacher
			@BillToAccountId ,
			'ZZ',
			'ZZ',
			'ZZ',
			'ZZ',
			'ZZ',
			@teacherinstance  OUTPUT
	end	

	exec CreateStudent
		@teacherinstance ,
		'ZZ',
		'ZZ',
		@studentinstance  OUTPUT

	Update CustomerOrderHeader Set StudentInstance=@studentinstance where Instance = @CustomerOrderHeader
	
	--- INSERT EACH PRODUCT
		
	DECLARE @ProductCode varchar(50)
	DECLARE @Quantity int
	DECLARE @Price money
	DECLARE @ProgramSectionId int
	DECLARE @MagPrice_Instance int
	DECLARE @TotalPrice money

	If(@DebugMode=1)
	begin
		Print '@CustomerOrderHeader:' + convert(varchar, @CustomerOrderHeader)
		select * from customerorderheader where instance=@CustomerOrderHeader
		SELECT
			Product_code,
			Quantity,
			Price,
			ProgramSectionId,
			MagPrice_Instance,
			OracleCode
		FROM
			#FSOrderItems
		WHERE
			Quantity > 0
			AND IsValid = 1 
			AND IsValidApplicability = 1
	end


	Declare @inventorycount int
	DECLARE Detail CURSOR FOR
		SELECT
			Product_code,
			Quantity,
			Price,
			ProgramSectionId,
			MagPrice_Instance,
			OracleCode
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
			@MagPrice_Instance,
			@OracleCode
	
	WHILE @@Fetch_Status = 0
		BEGIN
	

			SELECT @TotalPrice = @Quantity * @Price
			


			If(@DebugMode=1)
			Begin
				Print 'OnHand ' + convert(varchar,@OnHandQty)
			End
			

				EXEC QSPCanadaOrderManagement..CreateDetailItem 
					@BatchDate,
					@CustomerOrderHeader,
					@ProductCode,
					@FirstName,
					@LastName,
					@Quantity,
					@TotalPrice,				-- Added Multiplication of Quantity per Karen T.  8/23/2004
					@ProgramSectionId,
					@Price,
					@Quantity,  -- Quantity Reserved
					'46004',
					@MagPrice_Instance,
					'500',	-- Status
					@CustomerInstanceId
	
			FETCH NEXT FROM Detail INTO
				@ProductCode,
				@Quantity,
				@Price,
				@ProgramSectionId,
				@MagPrice_Instance,
				@OracleCode
		END
	
	CLOSE Detail
	DEALLOCATE Detail
	

	Declare @CODCount int
	Select @CODCount=count(*) from CustomerOrderDetail where CustomerOrderHeaderInstance=@CustomerOrderHeader

	Update Batch Set EnterredCount=0,EnterredAmount=0,CalculatedAmount=0,
		OrderCount=1, OrderCountAccept=1, OrderDetailCountError=0,
		OrderDetailCount=@CODCount,
		CheckDate='1/1/95',
		IsStaffOrder=0,
		ContactFirstName = @ContactFirst, ContactLastName = @ContactLast,
		ContactPhone=@ContactPhone,OrderDeliveryDate = @RequestedDeliveryDate
		where OrderID = @OrderId

	--update  CustomerOrderDetail set customershiptoinstance = @CustomerInstanceId where CustomerOrderHeaderInstance=@CustomerOrderHeader 

	If( @DebugMode=1)
	begin
		print 'Contacts:' + @ContactFirst + ' ' + @ContactLast+ ' ' +@ContactPhone
		print '@OrderID: '+convert(varchar, @OrderId)
	end

	Update QSPCanadaCommon..Campaign Set FSOrderRecCreated = 1 where id=@CampaignID
	
End

DROP TABLE #FSOrderItems
GO