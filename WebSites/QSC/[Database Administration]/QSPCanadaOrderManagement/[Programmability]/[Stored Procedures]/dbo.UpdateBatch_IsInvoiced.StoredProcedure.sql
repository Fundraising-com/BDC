USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[UpdateBatch_IsInvoiced]    Script Date: 06/07/2017 09:20:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateBatch_IsInvoiced]
	@OrderID		int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/10/2004 
--   Update the Batch Record 'IsInvoiced' field after completing GenerateInvoices process For Canada Finance System.
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

UPDATE QSPCanadaOrderManagement..Batch
	SET 	IsInvoiced 	= 1
WHERE OrderId = @OrderID


--MS Oct 31, 2006
--Loonie Library should be marked as printed and not be sent

Update QSPCanadaFinance..Invoice 
Set Is_Printed='Y'
From 	QSPcanadaOrdermanagement..Batch b,
	qspcanadacommon..campaignprogram cp
Where QSPCanadaFinance..Invoice.Order_id=b.OrderId
And  b.campaignId=cp.CampaignID
And cp.programid=24
And cp.DeletedTF=0
And b.orderId=@OrderID


Update QSPCanadaFinance..Invoice 
Set Is_Printed='Y'
From 	QSPcanadaOrdermanagement..Batch b
Where QSPCanadaFinance..Invoice.Order_id=b.OrderId
--And  b.OrderQualifierID in (39020,39015)
And  b.OrderQualifierID in (39015)		--MS March 22, 2007 Issue#2047
And b.orderId=@OrderID





SET NOCOUNT OFF
GO
