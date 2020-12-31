USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[OrdersStatus]    Script Date: 06/07/2017 09:19:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[OrdersStatus]

@CampaignStartDate datetime,
@CampaignEndDate datetime


 AS

Declare @BatchDate datetime, @OrderType varchar(100),  @OrderQualifier varchar(100) , @OrderStatus varchar(100), @OrderCount int

Declare @OutputRec varchar(8000)
Declare @OutputRec2 varchar(8000)
Declare @Subject varchar(200)

Set @Subject  = 'Orders Status as of ' + cast(getdate()as varchar)

--   Select   char(13)+Cast(Convert(varchar(10),batch.date,101) as varchar) +'  '+ cdOT.Description +'  '+ cdQ.Description +'  '+ cd.Description +'  '+ cast(Count(*) as varchar)

Declare C1 Cursor  For
   Select   char(13)+Cast(Convert(varchar(10),batch.date,101) as varchar) +'   '+
	    substring(cdOT.Description +'          ',1,10)+ 
	    substring(cdQ.Description +'                              ',1,30)+
	    substring(cd.Description+'                              ',1,30)+
	    cast(Count(*) as varchar)
   From QspCanadaOrderManagement.dbo.Batch as batch,
	QspCanadaCommon.dbo.CodeDetail as cd,
	QspCanadaCommon.dbo.CodeDetail as cdOT,
	QspCanadaCommon.dbo.CodeDetail as cdQ,
	QspCanadaCommon.dbo.Campaign as ca  
   Where batch.statusInstance  = cd.instance
	and batch.OrderTypeCode  = cdOT.instance
	and batch.OrderQualifierID  = cdQ.instance
   and batch.campaignid   = ca.id
   and ca.StartDate >= @CampaignStartDate
   and ca.endDate <= @CampaignEndDate
   and batch.statusInstance <> 40013
   Group by batch.date,cdOT.Description,batch.OrderTypeCode,cdQ.Description,cd.Description 
   Order by batch.date desc,cdOT.Description


Set @OutputRec = 'BatchDate  Order Type  Order Qualifier               Order Status              # of Orders'
select @OutputRec = @OutputRec+char(13)
Set @OutputRec = @OutputRec + '==========================================================================================='

OPEN C1

	    FETCH NEXT FROM C1  INTO @OutputRec2
	    WHILE @@FETCH_Status = 0

                BEGIN

		Set @OutputRec  = @OutputRec +@OutputRec2

		FETCH NEXT FROM C1  INTO @OutputRec2
	   END

	CLOSE C1
	DEALLOCATE C1
		

 Exec QSPCanadaCommon..Send_EMail  'qsp-qspfulfillment-dev@qsp.com', 'qsp-qspfulfillment-dev@qsp.com', @Subject,@OutputRec
GO
