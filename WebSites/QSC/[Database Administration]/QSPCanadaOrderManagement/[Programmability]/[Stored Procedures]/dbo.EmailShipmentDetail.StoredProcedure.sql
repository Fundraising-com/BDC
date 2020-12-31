USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[EmailShipmentDetail]    Script Date: 06/07/2017 09:19:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[EmailShipmentDetail]
	@WayBillNumber Varchar(50),
	@SendToFM Varchar(1),
	@SendToAccount Varchar(1)

As
	
	Declare @AccountEmail		Varchar(200),
		@AccountLang		Varchar(10),
		@FMEmail		Varchar(200),
		@FMLang		Varchar(10),
		@OrderType		Int,
		@EmailList		Varchar(1000),
		@EmailContenttoSend	Varchar(8000),
		@EmailSubject		Varchar(1000),
		@Language		Varchar(5),
		@OrderListSeperator	Varchar(5),
		@OrderListSeperatorLen	Int,
		@OrderId		Int

	set nocount on
	Set @OrderListSeperator = ','
	Set @OrderListSeperatorLen = Len(@OrderListSeperator)

	If (IsNull(Upper(@SendToFM) ,'Y')= 'Y'  OR IsNull(Upper(@SendToAccount) ,'Y')= 'Y' ) 
	Begin

		SELECT	@OrderID = OrderID
		FROM	ShipmentWayBill sw
		JOIN	Shipment ship
					ON	ship.ID = sw.ShipmentID
		JOIN	ShipmentOrder so
					ON	so.ShipmentID = ship.ID
		WHERE	sw.WayBillNumber = @WayBillNumber

		Select @OrderType = OrderTypeCode From QSPCanadaOrderManagement.dbo.Batch Where OrderId=@OrderId

		If @OrderType in   (41001,41002,41008,41009) 
		Begin

			Select  @AccountEmail = (Case 	IsNUll(b.ContactEmail,'')
						When '' Then cont.email
						Else b.ContactEmail
						End ) ,
				@AccountLang = cacc.Lang , 
				@FMEmail=fm.email ,
				@FMLang=fm.Lang 
			From      QSPCanadaOrderManagement.dbo.Batch b ,
    					QSPCanadaCommon.dbo.CAccount cacc ,
				QSPCanadaCommon.dbo.Contact cont,
				QSPCanadaCommon.dbo.Campaign ca,
				QSPCanadaCommon.dbo.FieldManager fm
			Where  b.ShipToAccountID = cacc.Id 
			And    cacc.Id = cont.CAccountID
			And b.CampaignId= ca.id
			And ca.fmid=fm.fmid
			And b.orderid =@OrderId
			--And b.OrderTypeCode in (41001,41002,41008,41009) --CA, CAFS,GROUP, MAGNET
			And cont.id in (Select Max(id) From QSPCanadaCommon.dbo.Contact Where caccountid= cacc.id And DeletedTf <> 1) -- LatestContact

			Set @Language = IsNull(@AccountLang,'EN')

		End

		If @OrderType in  (41006,41007,41011) --FM,FMBULK,FMCLOSEOUT
		Begin

			Select    @FMEmail = fm.email , 
            				@FMLang = fm.Lang 
			From      QSPCanadaOrderManagement.dbo.Batch b ,
				QSPCanadaCommon.dbo.Campaign ca,
				QSPCanadaCommon.dbo.FieldManager fm
			Where   b.CampaignId= ca.id
			And ca.fmid=fm.fmid
			And b.orderid =@OrderId

		Set @Language = IsNull(@FMLang,'EN')

		End


		If IsNull(@SendToAccount,'Y') = 'N' And IsNull(@SendToFM,'Y') = 'Y'
		Begin
			Set @EmailList =@FMEmail
		End

		If IsNull(@SendToAccount,'Y') = 'Y' And IsNull(@SendToFM,'Y') = 'N'
		Begin
			Set @EmailList =@AccountEmail
		End

		If IsNull(@SendToAccount,'Y') = 'Y' And IsNull(@SendToFM,'Y') = 'Y'
		Begin
			Set @EmailList =@AccountEmail
		
			If IsNull(@EmailList,'') = ''
			Begin
				Set @EmailList = @FMEmail
			End
			Else
			Begin
				Set @EmailList = @EmailList+','+@FMEmail
			End
		End

		If  IsNull(@EmailList,'') <>  ''
		Begin
			Exec QSPCanadaOrdermanagement.dbo.GetShipmentDetail @OrderId , @WayBillNumber , 2 , @Language , @EmailSubject   OutPut

			Exec QSPCanadaOrdermanagement.dbo.GetShipmentDetail @OrderId , @WayBillNumber , 1 ,@Language , @EmailContenttoSend   OutPut
			
			If  IsNull(@EmailContenttoSend,'') <>  ''
			Begin				

				Exec QSPCanadaCommon..Send_EMail   'Shipment_Notification@qsp.com', @EmailList ,@EmailSubject, @EmailContenttoSend
			End
		End		
	End
GO
