USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderStageTrackingForInvoiced]    Script Date: 06/07/2017 09:20:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateOrderStageTrackingForInvoiced]  @OrderId int

AS

Set NoCount On

Declare @CampaignId 		Int
Declare @FMID 		Varchar(05)
Declare @FMName 		Varchar(100)
Declare @GroupID	 	Int
Declare @GroupName 		Varchar(50)
Declare @ReceiptDate 		DateTime
Declare @ImageDate		DateTime
Declare @DataCapturedate	DateTime
Declare @VerificationDate 	DateTime
Declare @EditDate 		DateTime
Declare @TransmitDate 		DateTime
Declare @DateInvoiced 		DateTime 
Declare @ScanCount 	 	Int
Declare @ToteID			Int


--Select @DateInvoiced = Invoice_Date From QSPCanadaFinance.dbo.Invoice Where Order_Id=@OrderId

-- Only Orders from Resolve
Select @DateInvoiced = i.Invoice_Date 
From QSPCanadaFinance.dbo.Invoice i, QSPCanadaOrdermanagement..batch b
Where i.Order_Id=b.OrderId
And OrderQualifierId In (39001,39002,39003)
And Order_Id=@OrderId

If @@RowCount >0
Begin

	Select 	Top 1	@campaignId=CampaignId,
			@GroupID=GroupId,
			@GroupName=GroupName,
			@FMID=FMID,
			@FMName=FMName,
			@ReceiptDate=ReceiptDate,
			@ImageDate=ImageDate,
			@DataCaptureDate=DataCaptureDate,
			@VerificationDate=VerificationDate,
			@EditDate=EditDate,
			@TransmitDate=TransmitDate,
			@ScanCount=ScanCount,
			@ToteID=ToteID
	From   QSPCanadaOrderManagement.dbo.OrderStageTracking 
	Where OrderId=@OrderId  And Stage=59005 -- transmit, "At Unigistix" stage does not have these dates

	--If not resolve order get info from batch
	If @@RowCount =0 
	Begin
	
		Select 	@campaignId=campaignId,
			@GroupID=AccountId,
			@GroupName=Name,
			@FMID= f.fmid,
			@FMName= f.LastName+' '+f.FirstName,
			@ReceiptDate=Null,
			@ImageDate=Null,
			@DataCaptureDate=Null,
			@VerificationDate=Null,
			@EditDate=Null,
			@TransmitDate=Null,
			@ScanCount=0
		From 	QSPcanadaOrdermanagement..Batch b, 	
			QSPcanadacommon..Fieldmanager f, 
			QSPcanadacommon..Campaign c, 
			QSPcanadaCommon..Caccount ac
		Where b.CampaignId= c.Id
		And c.FMID=f.FMID
		And c.BillToAccountID = ac.Id
		And OrderId=@OrderId 

	End

	If @campaignId <> 0  
	Begin

		Insert Into QSPCanadaOrdermanagement..OrderStageTracking (StageDate, CampaignID, OrderID, FMID, Stage,  ScanCount ,GroupID, GroupName, FMName, ReceiptDate, ImageDate,DataCaptureDate,VerificationDate,EditDate,TransmitDate,DateInvoiced,ToteID)
		Values(@DateInvoiced,@campaignId,@OrderId,@FmID,59007,@ScanCount, @GroupID,@GroupName,@FMName,@ReceiptDate,@ImageDate,@DataCaptureDate,@VerificationDate,@EditDate,@TransmitDate,@DateInvoiced,@ToteID)

		--Update DateInvoiced in all other record for the Order
		Update QSPCanadaOrdermanagement..OrderStageTracking
		Set DateInvoiced = @DateInvoiced
		Where ToteID= ISNULL(@ToteID,0)

	End
End
Set NoCount Off
GO
