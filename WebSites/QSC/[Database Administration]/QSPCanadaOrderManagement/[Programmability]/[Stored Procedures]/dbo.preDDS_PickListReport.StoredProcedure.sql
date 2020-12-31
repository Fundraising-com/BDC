USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[preDDS_PickListReport]    Script Date: 06/07/2017 09:20:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[preDDS_PickListReport]
	@OrderID		Int,
	@BatchId		Int,
	@BatchDate		DateTime,
	@ShipDateFrom		DateTime ,
	@ShipDateTo		DateTime,
	@ReportType		Int  -- PackingSlip(2) or PickList(1) Used By GetLabelsForPickList Proc
As
Declare @OrderType	Int,
	@CALanguage	Varchar(2),
	@CampaignId	Int,
	@OrderQualifierID int

Select Top 1 @OrderType= b.orderTypeCode ,@CampaignId=campaignid, @OrderQualifierID=OrderQualifierID
From  	QSPCanadaOrderManagement.dbo.batch b
Where  b.orderId = @OrderID
And b.Id	 = @BatchId
And b.Date	 = @BatchDate

if(@OrderQualifierID = 39008)
begin
	  Exec dbo.GetDataforPickListForCustomerService  @OrderID,@BatchId,@BatchDate,@ShipDateFrom,@ShipDateTo,@ReportType   -- PackingSlip(2) or PickList(1)
end
else
begin

	If  ( @OrderType in (41001,41009) Or
	      (	@OrderType = 41008 And @CampaignId <> 0))
	Begin
		
	             Exec dbo.GetDataforPickList  @OrderID,@BatchId,@BatchDate,@ShipDateFrom,@ShipDateTo,@ReportType   -- PackingSlip(2) or PickList(1)
	
	End
	If  ( @OrderType in (41002,41005,41006, 41007, 41010) Or
	     (	@OrderType = 41008 And IsNull(@CampaignId,0)= 0))
	Begin
	
	             Exec dbo.GetDataForPickListNonEnvelope  @OrderID,@BatchId,@BatchDate, @ShipDateFrom  ,@ShipDateTo,@ReportType   -- PackingSlip(2) or PickList(1)
	
	End

end
GO
