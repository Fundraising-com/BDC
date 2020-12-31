USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GenerateFieldSupplyOrder_V5]    Script Date: 06/07/2017 09:19:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
--set nocount on
--drop table #FSOrderItems
CREATE    Procedure [dbo].[GenerateFieldSupplyOrder_V5] @CampaignID	Int,
							 @UserId 	Varchar(4),
							 @DebugMode 	Int,
							 @GenerateOrder Int
As
Declare	@OnHandQty int,
	@OracleCode varchar(50)



Declare @Season char(1)
Declare @Year int
Declare @Lang varchar(10), @FMId varchar(4)
Declare @ShipToAccountId int, @BillToAccountId int, @ShipToType Int, @TaxRegionId Int, @NumberOfStudents int, @NumberOfStaff int, @NumberOfClasses int
Declare @ShipToAccountProvince varchar(10)

Declare @ContactFirst varchar(50)
Declare @ContactLast varchar(50)
Declare @ContactPhone varchar(50)

select  @ContactFirst =''
select  @ContactLast = ''

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
	
FROM
	QSPCanadaCommon..Campaign
WHERE
	Id = @CampaignID

if(@DebugMode=1)
Begin
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
/*
Select * from QSPCanadaProduct..ProductInventory
Select * from QSPCanadaCommon..Address
select * from QSPCanadaCommon..CampaignToContentCatalog A where campaignid=2657
select * from QSPCanadaCommon..CampaignProgram A where campaignid=17359
select * from QSPCanadaCommon..Campaign A where SuppliesCampaignContactID is not NULL id=17359
select * from QSPCanadaProduct..Pricing_Details E where E.FSContent_Catalog_Code ='MAGENS05'
Select Oraclecode,FSContent_Catalog_Code,* from QSPCanadaProduct..Pricing_Details E where program_id <> 0 and Pricing_Year=2005 and pricing_season='S' and FSContent_Catalog_Code is null
and Language_Code='EN'

Select * from QSPCanadaProduct..ProgramSection where id in (225, 228)
select * from QSPCanadaProduct..Product where Product_code = 'MP707-04G'
Select * from QSPCanadaCommon..Program where id in (2,11)
select * from program_master where code in ('FSSP05-E')
select * from progfssectionmap where catalog_section_id  in (225, 228)
Update ProgramSection set Program_id = p.Program_id  from ProgramSection, progfssectionmap p where id=catalog_section_id
*/
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
	,H.ID as ProgramContentCatalogCodeLookupID
Into #FSOrderItems
FROM
	QSPCanadaCommon..CampaignToContentCatalog A
	INNER Join QSPCanadaCommon..ProgramContentCatalogCodeLookup H on A.ProgramContentCatalogCodeLookupID=H.ID
	INNER JOIN QSPCanadaCommon..Program B ON A.ProgramId = B.Id
	INNER JOIN QSPCanadaCommon..Campaign C ON C.Id = A.CampaignId
	INNER JOIN QSPCanadaCommon..CampaignProgram CP on CP.Campaignid = A.CampaignId
	INNER JOIN QSPCanadaProduct..Pricing_Details E ON E.Product_Code = H.PrimaryCatalogCode
	INNER JOIN QSPCanadaProduct..ProgramSection D ON D.Id = E.ProgramSectionId
	INNER JOIN QSPCanadaProduct..Program_Master F ON F.Code LIKE E.FSContent_Catalog_Code
	INNER JOIN QSPCanadaProduct..Product G ON (G.Product_instance = E.Product_instance)
WHERE
	E.FSProgram_Id = A.ProgramId
	AND A.CampaignId = @CampaignId
	AND E.Pricing_Year = @Year 
	AND E.Pricing_Season = @Season
	--AND F.Status in (30403,30404 )  
	--AND F.FSIsBrochure = 1 
	AND E.Language_Code = @Lang
	and A.ProgramID=CP.ProgramiD
	And cp.DeletedTF = 0
	And D.type <> 7 	--Program section for FM kanata Invoicing, MS Oct 26, 2005
--	and E.Product_instance=G.Product_Instance
	

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
	, 0 -- Place holder

FROM
	QSPCanadaCommon..CampaignProgram A,
	QSPCanadaCommon..Program B,
	QSPCanadaCommon..Campaign C ,

	QSPCanadaProduct..ProgramSection D ,
 	QSPCanadaProduct..Pricing_Details E ,
	QSPCanadaProduct..Program_Master F ,
	QSPCanadaProduct..Product G,
	QSPCanadaProduct..ProgFSSectionMap H
WHERE
	A.CampaignId = @CampaignId
	AND E.Pricing_Year = @Year 
	AND E.Pricing_Season = @Season
	--AND F.Status in (30403,30404 )  
	AND E.Language_Code =  @Lang
	AND (E.FSContent_Catalog_Code IS NULL OR E.FSContent_Catalog_Code = '')
	AND E.FSProgram_Id = A.ProgramId
	and A.ProgramId = B.Id
	and  C.Id = A.CampaignId
	and  H.Program_ID = E.FSProgram_Id
--	and  D.Program_Id = H.Program_Id
	and  D.Id = H.Catalog_section_id
	and  E.ProgramSectionId = D.Id
--	and  E.ProgramID=A.ProgramID
	and  D.CatalogCode LIKE F.Code
	and E.Product_instance=G.Product_Instance
	 and (G.Product_Instance = E.Product_Instance )
	And DeletedTF = 0
	And D.type <> 7 	--Program section for FM kanata Invoicing, MS Oct 26, 2005


if(@DebugMode=1)
begin
	SELECT * FROM #FSOrderItems
end
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
			@Country = A.Country			
			 from QSPCanadaCommon..Contact C,QSPCanadaCommon..Campaign as Campaign,
				
				QSPCanadaCommon..Address  A where A.address_id = AddressID
				and SuppliesCampaignContactID=C.ID and Campaign.Id = @CampaignID
	END

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  This section will determine the TaxRegionId And Update the temp table to weed out products that don't apply
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
end

UPDATE #FSOrderItems SET #FSOrderItems.IsValid = 1 WHERE #FSOrderItems.TaxRegionId = @TaxRegionId  OR #FSOrderItems.TaxRegionId = 0

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
--select * from #FSOrderItems
--select * from QSPCanadaProduct..CodeDetail where ADPCODE IS NOT NULL
--select * from QSPCanadaCommon..CodeDetail where codeheaderinstance=44030

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
		WHEN FSDistributionLevelId = 44031 THEN 	-- Campaign Level
			Round(1 * (FSExtra_Limit_Rate/100), 0)
		WHEN FSDistributionLevelId = 44032 THEN 	-- Class Level
			Round(@NumberOfClasses * (FSExtra_Limit_Rate/100), 0)
		WHEN FSDistributionLevelId = 44033 THEN 	-- Student Level
			Round((@NumberOfStudents + @NumberOfStaff) * (FSExtra_Limit_Rate/100), 0)
	END
FROM
	#FSOrderItems
--	INNER JOIN QSPCanadaProduct..CodeDetail B ON B.Instance = #FSOrderItems.FSDistributionLevelId
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
			AND FSApplicabilityId IN (43101, 43102)
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
			AND FSApplicabilityId IN (43101, 43103)
			AND IsValid = 1

		--Non Combo CAs Sweet Sensation and Gift Only need 'Other Order Forms' MS Sept 02, 05
		UPDATE
			#FSOrderItems
		SET
			IsValidApplicability = 1
		WHERE
			FSIsBrochure = 0
			AND FSApplicabilityId IN (43102)
			AND IsValid = 1
			AND ProgramId in (19,20) --GiftOnly Sweet Sensation
	END

--- IsBrochure Items autmatically added

UPDATE
	#FSOrderItems
SET
	IsValidApplicability = 1
WHERE
	FSIsBrochure = 1
	AND IsValid = 1

UPDATE
	#FSOrderItems
SET
	IsValid =0
WHERE
	Product_code LIKE 'MP707%'

UPDATE
	#FSOrderItems
SET
	IsValid =0
WHERE
	Product_code LIKE '537F'


---select Quantity,Round((@NumberOfStudents + @NumberOfStaff) * (FSExtra_Limit_Rate/100), 0),* from #FSOrderItems --where IsValid=1


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
				@StateProvince ,
				@PostalCode ,
				'','',
				@UserId, @CustomerInstanceId OUTPUT	
		END

	Update Customer set Address1=@Street1, Address2=@Street2, City=@City, State = @StateProvince, zip=@PostalCode
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
			
			
			Select @inventorycount = count(*)  from QSPCanadaProduct..ProductInventory
				where OracleCode=@OracleCode

			if(@inventorycount  > 0)
			begin
				Select @OnHandQty = QtyOnHand from QSPCanadaProduct..ProductInventory
					where OracleCode=@OracleCode
			end
			else
			begin
				Select @OnHandQty =0
			end

/*****
****  remove following line for production
****/
--select @OnHandQty= @Quantity + 1000
			-- If we've runout flip to the secondary item
			if(@OnHandQty < @Quantity)
			Begin
				-- fetch from ProgramContentCatalogCodeLookup
				select @ProductCode=SecondaryCatalogCode from QSPCanadaCommon..ProgramContentCatalogCodeLookup where
					PrimaryCatalogCode=@ProductCode and FiscalYear=@Year and Season=@Season
					and Province=@ShipToAccountProvince

				-- get new mag price instance
				Select @MagPrice_Instance = MagPrice_Instance From QSPCanadaProduct..Pricing_details
					where Product_code = @ProductCode and Pricing_year=@Year and Pricing_Season=@Season
						and TaxRegionID = @TaxRegionID
			End

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
					'500'	-- Status
			
	
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
	
--	Exec DoCloseOrder @OrderId, 1

	Declare @CODCount int
	Select @CODCount=count(*) from CustomerOrderDetail where CustomerOrderHeaderInstance=@CustomerOrderHeader

	Update Batch Set EnterredCount=0,EnterredAmount=0,CalculatedAmount=0,
		OrderCount=1, OrderCountAccept=1, OrderDetailCountError=0,
		OrderDetailCount=@CODCount,
		CheckDate='1/1/95',
		IsStaffOrder=0,
		ContactFirstName = @ContactFirst, ContactLastName = @ContactLast,
		ContactPhone=@ContactPhone
		where OrderID = @OrderId

	If( @DebugMode=1)
	begin
		print 'Contacts:' + @ContactFirst + ' ' + @ContactLast+ ' ' +@ContactPhone
		print '@OrderID: '+convert(varchar, @OrderId)
	end

	Update QSPCanadaCommon..Campaign Set FSOrderRecCreated = 1 where id=@CampaignID
	
--	SELECT *, @CustomerInstanceId As 'CustomerInstanceId', @OrderId As OrderId,  @CustomerOrderHeader As CustomerOrderHeader FROM #FSOrderItems WHERE IsValid = 1 AND IsValidApplicability = 1
End

DROP TABLE #FSOrderItems
GO
