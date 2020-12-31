USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetShipmentDetail]    Script Date: 06/07/2017 09:19:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[GetShipmentDetail] @OrderId Int, @WayBillNumber Varchar(50), @ContentType Int,@Language Varchar(10) , @Detail  Varchar(8000) OutPut
As

	
	Declare  @OrderType		Int,
		@OrderTypeDescription  Varchar(100),
		@CampaignId		Int,
		@FMName		Varchar(100),
		@FMNameCA		Varchar(100),
		@AccountId		Int,
		@AccountName		Varchar(100),
		@DateReceived 	Varchar(12),
		@QuantityShipped	Int,
		@DistributionCenter	Varchar(100),
		@CarrrierId		Int,
		@Carrier		Varchar(100),
		@Shipmentdate		Varchar(12),
		@ExpectedDeliveryDate Varchar(12),
		@NumberBoxes 	Int,
		@ShippingComment	Varchar(300),
		@UnitOfMeasure	Varchar(20),
		@WayBillNoFrom	Varchar(50),
		@WayBillNoTo		Varchar(50),
		@EmailContent		Varchar(8000),
		@Element		Varchar(2000),
		@LeftFieldDelimit	Varchar(5),
		@RightFieldDelimit	Varchar(5),
		@LeftFieldDelimitLength  Int,
		@RightFieldDelimitLength Int,
		@FieldName		 Varchar(50),
		@FieldValue		 Varchar(50),
		@FormatedText		 Varchar(8000),

		@VariableToDeclare 	Int,	
		@EmailContentSplit	Varchar(8000),
		@splitLoc		Int,
		@EmailContentLastLine	Varchar(1000),
		@SuppliesShipto	 Varchar(20),
		@OrderQualifierId             Int,
		@ShiptoAcc 		Int,
		@ShiptoFm 		Int,
		@ShipItemCount             Int,
		@BHEItemCount	Int

		Declare @ShippedItem Table(
		Id		Int 	Identity,
		ProductName 	Varchar(50),
		ProductType	Int,
		Quantity		Int
		)

		

	Declare @CarrierName Table(
		Id		Int 	Identity,
		CarrierId		Int,
		CarrierName 	Varchar(200),
		FreanchName 	Varchar(200)
		)
		
	Declare @Shipment Table(
		Id			Int 	Identity,
		DistributionCenter	Varchar(100),
		CarrrierId		Int,	
		CarrierName 		Varchar(200),
		Shipmentdate		DateTime,
		ExpectedDeliveryDate	Varchar(20),
		NumberBoxes 		Int,
		ShippingComment	Varchar(200),
		UnitOfMeasure 		Varchar(10),
		WayBillNo 		Varchar(100)
		)

	
	If IsNull(@OrderId,0) > 0 And  IsNull(@WayBillNumber,'') <> ''
	Begin

	
	Set @LeftFieldDelimit = '<~~*'
	Set @RightFieldDelimit = '*~~>'

	Set @LeftFieldDelimitLength = Len(@LeftFieldDelimit)
	Set @RightFieldDelimitLength = Len(@RightFieldDelimit)

	--Populate French name for carriers
	Insert into @CarrierName
	Select Instance,Description,Null from qspcanadacommon..codedetail where CodeHeaderinstance=53000
	order by 1

	Update @CarrierName Set FreanchName = 'DHL' 		Where CarrierId =  53001
	Update @CarrierName Set FreanchName = 'MDS' 			Where CarrierId =  53002
	Update @CarrierName Set FreanchName = 'Essai Sur terre Exprès' 	Where CarrierId =  53003
	Update @CarrierName Set FreanchName = 'Fastrate Consolidé'	 Where CarrierId =  53004
	Update @CarrierName Set FreanchName = 'Poteau Du Canada' 	Where CarrierId =  53005
	Update @CarrierName Set FreanchName = 'Intérieur' 		Where CarrierId =  53006
	Update @CarrierName Set FreanchName = 'L''Autre Porteur' 	Where CarrierId =  53007
	Update @CarrierName Set FreanchName ='Sameday'		Where CarrierId =  53008


	Select   @OrderType = OrderTypeCode,
       		@OrderTypeDescription = Case @OrderType
			       When 41001 Then 'Campaign'
			       When 41002 Then 'Campaign Field Supply'
			       When 41005 Then 'Employee'
			       When 41006 Then 'Field Manager'	
			       When 41007 Then 'Field Manager (Bulk)'		
			       When 41008 Then 'Campaign Straight'
			       When 41010 Then 'POS'	
			       When 41011 Then 'Field Manager (Closeout)'		
			       Else 'UNKNOWN'
			       End 	
	From QSPCanadaOrderManagement.dbo.Batch Where OrderId=@OrderId

	

		If @OrderType in   (41001,41002,41008,41009) 
		Begin

		Select @CampaignId =Ca.Id ,
			 @FMName = fm.FirstName+' '+fm.LastName,
			 @AccountId = IsNull(b.ShipToAccountID,ca.ShipToAccountID) ,
   			 @SuppliesShipto = cd.Description,
			 @AccountName = acc.Name, 
			 @DateReceived = Convert(Varchar(10),b.DateReceived,101) ,
			 @OrderQualifierId = b.OrderQualifierID,
	  		-- @OrderType = b.OrderTypeCode,
			 @ShiptoAcc = b.ShipToAccountID,
	 		 @ShiptoFm =b.shiptoFmID
		From  QSPCanadaOrderManagement.dbo.Batch b ,
		             QSPCanadaCommon.dbo.CAccount acc ,
			--QSPCanadaCommon.dbo.Campaign ca,
			QSPCanadaCommon.dbo.Campaign ca Left Join QSPCanadaCommon.dbo.codedetail cd on ca.SuppliesShipToCampaignContactID=cd.Instance,
			QSPCanadaCommon.dbo.FieldManager fm
		Where  	b.ShipToAccountID = acc.Id 
		And	b.CampaignId= Ca.Id
		And    	ca.FMId = fm.FMID
		And    	orderid=@OrderId
		End

		
		If @OrderType in  (41006,41007,41011)
		Begin
	
			Select   @CampaignId=	Ca.Id ,
				@FMName=fm.FirstName+' '+fm.LastName,
				@DateReceived=Convert(Varchar(10),b.DateReceived,101) ,
				@OrderQualifierId = b.OrderQualifierID,
	  			--@OrderType = b.OrderTypeCode,
			 	@ShiptoAcc = b.ShipToAccountID,
	 		 	@ShiptoFm =b.shiptoFmID
			From      QSPCanadaOrderManagement.dbo.Batch b ,
				QSPCanadaCommon.dbo.Campaign ca,
				QSPCanadaCommon.dbo.FieldManager fm
			Where   b.CampaignId= ca.id
			And ca.fmid=fm.fmid
			And b.orderid =@OrderId

		End

		--MS Aug 24 2006 if not FS get ship school
		If  @OrderType <> 41002
		Begin
			
			Select @SuppliesShipto= Case Sign(IsNull(@ShiptoAcc ,0))
						When  0 Then 'FM'
						When  1 Then    Case  Sign(IsNull(@ShiptoFm ,0))
								When 1 Then 'FM'
								Else 'School'
								End
						End
		End

		--Disabled MS Oct 19, 2005
		--  Total Quantity Shipped
		/*Select  @QuantityShipped= Sum(QuantityShipped) --,Count(d.StatusInstance) 
		From   QSPCanadaOrderManagement.dbo.Batch b ,
			QSPCanadaOrderManagement..CustomerorderHeader h,
			QSPCanadaOrderManagement..customerorderDetail d
		Where b.id=h.orderBatchId
		And b.date=h.orderBatchDate
		And h.Instance=d.customerorderHeaderInstance
		And d.StatusInstance=508
		And d.DelFlag <> 1
		And orderId=@OrderId
		And d.ShipmentId=@ShipId*/

		-->MS Oct 19, 2005 Shipment detail
		Insert into @ShippedItem
		SELECT		SUBSTRING(cod.ProductName, 1, 27) AS ProductName,
					cod.ProductType,
					SUM(cod.QuantityShipped) AS qty
		FROM		CustomerOrderDetail cod
		JOIN		CustomerOrderHeader coh
						ON	coh.Instance = cod.CustomerOrderHeaderInstance
		JOIN		Batch batch
						ON	batch.ID = coh.OrderBatchID
						AND	batch.Date = coh.OrderBatchDate
		JOIN		Shipment ship
						ON	ship.ID = cod.ShipmentID
		JOIN		ShipmentWayBill sw
						ON	sw.ShipmentID = ship.ID
		WHERE		batch.StatusInstance <> 40005 --40005: Cancelled
		AND			cod.DelFlag <> 1
		AND			cod.StatusInstance IN (508, 510) --508: Order Detail Shipped, 510: Order Detail Pickable (Backorder)
		AND			cod.ProductType NOT IN (46006, 46007, 46012) --Don't send BHE Shipment Notifications
		AND			sw.WayBillNumber = @WayBillNumber
		GROUP BY	cod.ProductCode,
					cod.ProductName,
					cod.ProductType


		--Ensure there are items under shipmentId
		Select @ShipItemCount = Count(*) From @ShippedItem

		If @ShipItemCount > 0
		Begin

		Select @variableToDeclare = Count(*), @QuantityShipped=Sum(Quantity)
		From @ShippedItem
		

		-- Shipment Detail
		--Disabled MS Jan10 2006
		/*Select  @DistributionCenter= dc.Name,
			@CarrrierId=s.CarrierId,
			@Carrier = Case IsNull(Upper(@Language),'FR')
				   When 'FR' Then cn.FreanchName
				   Else cn.CarrierName
				   End,
			@Shipmentdate= Convert(Varchar(10),s.Shipmentdate,101),
			@ExpectedDeliveryDate = Case (Convert(Varchar(10),s.ExpectedDeliveryDate,101))
						   When '01/01/1995' Then 'N/A'
						    Else Convert(Varchar(10),s.ExpectedDeliveryDate,101)
						    End  ,
			@NumberBoxes=s.NumberBoxes,
			@ShippingComment = s.Comment,
			@UnitOfMeasure = s.WeightUnitOfMeasure,
			@WayBillNo = wb.WayBillNumber
		From    QSPCanadaOrderManagement..Shipment s, 
			QSPCanadaOrderManagement..ShipmentWayBill Wb,
			QSPCanadaOrderManagement..ShipmentOrder so,
			QSPCanadaOrderManagement..DistributionCenter dc, @CarrierName cn
		Where S.Id=wb.ShipmentId
		And so.ShipmentId= S.Id
		And dc.Id= so.DistributionCenterId
		And s.CarrierId = cn.CarrierId
		And s.Id= @ShipId */

		/*DECLARE @ShipmentTotalNumberBoxes INT
		SELECT	@ShipmentTotalNumberBoxes = SUM(ship.NumberBoxes)
		FROM	Shipment ship
		JOIN	ShipmentWayBill sw
					ON	sw.ShipmentID = ship.ID
		WHERE	sw.WayBillNumber = @WayBillNumber*/

		Insert into @Shipment
		Select  TOP 1
			dc.Name,
			s.CarrierId,
			Case IsNull(Upper(@Language),'FR')
				   When 'FR' Then cn.FreanchName
				   Else cn.CarrierName
				   End Carrier,
			Convert(Varchar(10),s.Shipmentdate,101)Shipmentdate,
			Case (Convert(Varchar(10),s.ExpectedDeliveryDate,101))
						   When '01/01/1995' Then 'N/A'
						    Else Convert(Varchar(10),s.ExpectedDeliveryDate,101)
						    End ExpectedDeliveryDate ,
			s.NumberBoxes,
			Substring(s.Comment,1,100) Comment,
			s.WeightUnitOfMeasure,
			wb.WayBillNumber
		From    QSPCanadaOrderManagement..Shipment s, 
			QSPCanadaOrderManagement..ShipmentWayBill Wb,
			QSPCanadaOrderManagement..ShipmentOrder so,
			QSPCanadaOrderManagement..DistributionCenter dc, @CarrierName cn
		Where S.Id=wb.ShipmentId
		And so.ShipmentId= S.Id
		And dc.Id= so.DistributionCenterId
		And s.CarrierId = cn.CarrierId
		And wb.WayBillNumber = @WayBillNumber

		Select  @DistributionCenter= DistributionCenter,
			@CarrrierId= CarrrierId,
			@Carrier = CarrierName,
			@Shipmentdate= Shipmentdate,
			@ExpectedDeliveryDate = ExpectedDeliveryDate,
			@NumberBoxes= NumberBoxes,
			@ShippingComment = ShippingComment,
			@UnitOfMeasure = UnitOfMeasure
		From    @Shipment

		--Get maximum and minimum waybill number 
		Select  @WayBillNoFrom = Min(WayBillNo),@WayBillNoTo = Max(WayBillNo)
		From @Shipment

		-- Get Email contant 
		Declare  EmailContent Cursor For
		Select Content
		From QSPCanadaOrderManagement.dbo.EmailContent
		Where  LanguageCode= IsNull(@Language,'EN')
		And ContentNumber = @ContentType -- Subject text or Email text


		Open EmailContent
		Fetch Next From EmailContent Into @EmailContent

		While (@@Fetch_Status =0)
		Begin
			-->MS Oct 19, 2005 Shipment detail
			If @ContentType =1 
			Begin
				Select @splitLoc= Case IsNull(@Language,'EN')
							When 'EN' Then CharIndex('Thank You',@EmailContent,1)
							When 'FR' Then  CharIndex('MERCI',@EmailContent,1)
						   End
						
				Select @EmailContentSplit= Substring (@EmailContent,1,  @splitLoc-1 )
				Select @EmailContentLastLine = Substring (@EmailContent,  @splitLoc,1000 )

				--Set @EmailContentSplit = @EmailContentSplit + Char(9)+ 'Shipment Detail'+Char(13)
				
				While (@variableToDeclare > 0)
				Begin
					Select   @FieldName= Productname ,@FieldValue= Quantity from @ShippedItem where id=@variableToDeclare
					
					--Items not shipped will be displayed as backorder under quantity column
					If (IsNull(@FieldValue,'')= '' or IsNull(@FieldValue,0)= 0)
					Begin
					  	  Set @FieldValue= 'B/O'
					End

					Set @EmailContentSplit = @EmailContentSplit + Char(9)+
								 @FieldName + Char(9)+
								 @FieldValue +Char(13)

					Set @variableToDeclare = @variableToDeclare -1
				End
				
				Set  @EmailContent = Rtrim(@EmailContentSplit) + '' + Ltrim(Rtrim(@EmailContentLastLine))
			End -->

			Set @Element = Null

			Exec QSPCanadaOrderManagement.dbo.SplitElementFromList   @EmailContent  Output, @Element  Output , @LeftFieldDelimit , @LeftFieldDelimitLength  

			While (IsNull(@Element,'') <> '' or IsNull(@EmailContent,'') <> '')
			Begin
				Set @FormatedText = IsNull(@FormatedText,'') + @Element
				If IsNull(@EmailContent,'')=''
				Begin
					Set @FieldName = Null
					Set @FieldValue =Null
				End
				Else
				Begin
					Exec dbo.SplitElementFromList   @EmailContent  Output, @Element  Output , @RightFieldDelimit , @RightFieldDelimitLength  
					Set @FieldName =  Ltrim(Rtrim(@Element))

				End

				If @FieldName = 'ORDER_ID'
				Begin
					Set @FieldValue = Cast(@OrderId As Varchar)
				End

				If @FieldName = 'ORDER_TYPE_CODE'
				Begin
					Set @FieldValue = @OrderTypeDescription
				End

				If @FieldName = 'CAMPAIGN_ID'
				Begin
					Set @FieldValue = Cast(@CampaignId As Varchar)
				End

				If @FieldName = 'FM_NAME' 
				Begin
					Set @FieldValue = @FMName
				End

				If @FieldName =  'FM_NAME_CA'
				Begin
				
					Set @FieldValue = @FMNameCA
				End

				If @FieldName = 'GROUP_ID'
				Begin
					Set @FieldValue = Cast(@AccountId As Varchar)
				End

				If @FieldName = 'GROUP_NAME'
				Begin
					-- To put an space in subject line between group and FM name
					Set @FieldValue = @AccountName+' ' 
				End

				If @FieldName = 'DATE_ORDER_RECEIVED'
				Begin
					Set @FieldValue = @DateReceived
				End

				If @FieldName = 'DATE_SHIPPED'
				Begin
					Set @FieldValue = @Shipmentdate
				End

				If @FieldName = 'CARRIER'
				Begin
					Set @FieldValue = @Carrier
				End


				If @FieldName = 'EXPECTED_DELIVERY_DATE'
				Begin
					Set @FieldValue = @ExpectedDeliveryDate
				End


				If @FieldName = 'WAYBILL_NO'
				Begin
					If @WayBillNoFrom = @WayBillNoTo
					Begin
						Set @FieldValue = @WayBillNoFrom 
					End
					Else
						Set @FieldValue = @WayBillNoFrom + ' To '+@WayBillNoTo
					
				End

				If @FieldName = 'NUMBER_BOXES'
				Begin
					Set @FieldValue = Cast(@NumberBoxes As Varchar)
				End

				If @FieldName = 'QTY_SHIPPED_CUR'
				Begin
					Set @FieldValue =  Cast(@QuantityShipped As Varchar)
				End

				If @FieldName = 'WAREHOUSE'
				Begin
					Set @FieldValue =  @DistributionCenter
				End

				If @FieldName = 'SHIPTO'
				Begin
					Set @FieldValue =  IsNull(@SuppliesShipto,'N/A')
				End


				Set @FormatedText = IsNull(@FormatedText,'') + IsNull(@FieldValue,'')

				Exec QSPCanadaOrderManagement.dbo.SplitElementFromList   @EmailContent  Output, @Element  Output , @LeftFieldDelimit , @LeftFieldDelimitLength  
			End

		Fetch Next From EmailContent INTO @EmailContent
		End

		Set @Detail = IsNull(@FormatedText,'')

		Close EmailContent
		Deallocate EmailContent

		End --If not BHE
		Else
		Begin
			Set @Detail = '' --Donot send Email
		End
		
	End
GO
