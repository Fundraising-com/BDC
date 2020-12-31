USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Insert_BatchDistributionCenter]    Script Date: 06/07/2017 09:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[pr_Insert_BatchDistributionCenter]

@OrderId int

AS
--print 'starting proc pr_Insert_BatchDistributionCenter ' + cast(getdate() as varchar(50))

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempBDC]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempBDC]

--print 'creating table ' + cast(getdate() as varchar(50))
CREATE TABLE [dbo].[#TempBDC] (
	[PricingDetailsId] [int] NOT NULL ,
	[ItemQuantity] [int] NULL,
	[DistributionCenterId] [int] NULL,
	[ProductLine] [int] NULL
) ON [PRIMARY]




DECLARE  @Error int , @ErrorMsg varchar(1000), @HasError int 
Set @Error = 0 

 --print 'exec pr_VerifyOrder ' + cast(getdate() as varchar(50))
 Exec   QSPCanadaOrderManagement.dbo.pr_VerifyOrder  @OrderId, @ErrorMsg  output , @HasError output 

 --print 'selecting from systemerrorlog ' + cast(getdate() as varchar(50))
 Select top 1 @Error = 1
 from QspCanadaCommon.dbo.SystemErrorLog   
 where Orderid = @OrderId
            and isFixed = 0 


 IF  @Error = 1
   begin
              Select 'Error - Order# '+str(@OrderId)+ ' - pr_Insert_BatchDistributionCenter - Did not run due to an un-fixed issue in SystemErrorLog table'
   end 
ELSE

   Begin



DECLARE @BatchDate datetime, @BatchId int

--print 'select from batch ' + cast(getdate() as varchar(50))
SELECT
	@BatchDate = [Date]
	, @BatchId = [Id]
FROM
	QSPCanadaOrderManagement..Batch
WHERE
	OrderId = @OrderId

--print 'insert into #tempbdc ' + cast(getdate() as varchar(50))
INSERT INTO
	#TempBDC
SELECT
	C.PricingDetailsId
	, Quantity
	, NULL
	, E.ProductLine
FROM
	QSPCanadaOrderManagement..Batch A
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader B ON B.OrderBatchDate = A.[Date] AND B.OrderBatchId = A.[Id]
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderDetail C ON C.CustomerOrderHeaderInstance = B.Instance
	INNER JOIN QSPCanadaProduct..Pricing_Details D ON D.MagPrice_Instance = C.PricingDetailsId
	INNER JOIN QSPCanadaProduct..Product E ON E.Product_Instance = D.Product_Instance-- AND E.Product_Season = D.Pricing_Season AND E.Product_Code = D.Product_Code
WHERE	((A.OrderID = @OrderID AND (A.OrderQualifierID NOT IN (39009) OR C.IsShippedToAccount = 0))
OR		(C.IsShippedToAccount = 1 AND A.OrderID IN (SELECT DISTINCT OnlineOrderID  
													 FROM OnlineOrderMappingTable  
													 WHERE LandedOrderID = @OrderID)))
/*	(A.OrderId = @OrderId
		 OR (OrderID IN (SELECT DISTINCT OnlineOrderID  
		 FROM OnlineOrderMappingTable  
		 WHERE LandedOrderID = @OrderID)
		 AND IsShippedToAccount = 1))*/
	AND (C.ProductType not in( 46001,46017, 46012, 46021, 46023, 46024) OR (C.ProductType = 46024 AND (FormCode IS NOT NULL OR OrderQualifierID NOT IN (39001, 39002))))
	AND C.DelFlag = 0
--debug: select * from #TempBDC


--- UPDATE THE DistributionCenterId IF THE ProductLine has an absolute DistributionCenterId
--print 'update #tempbdc ' + cast(getdate() as varchar(50))
UPDATE
	#TempBDC
SET
	DistributionCenterId = B.DistributionCenterId
FROM
	#TempBDC A
	INNER JOIN QSPCanadaCommon..QSPProductLine B ON B.Id = (A.ProductLine)
WHERE
	B.DistributionCenterId is not null
	and A.ProductLine > 46000

--print 'update #tempbdc ' + cast(getdate() as varchar(50))
UPDATE
	#TempBDC
SET
	DistributionCenterId = B.DistributionCenterId
FROM
	#TempBDC A
	INNER JOIN QSPCanadaCommon..QSPProductLine B ON B.Id = (A.ProductLine + 46000)
WHERE
	B.DistributionCenterId is not null
	and  A.ProductLine < 46000

--print 'insert into batchdistributioncenter ' + cast(getdate() as varchar(50))
INSERT INTO
	QSPCanadaOrderManagement..BatchDistributionCenter
SELECT
	@BatchDate,
	@BatchId,
	DistributionCenterId,
	40010,
	 ProductLine + 46000	
FROM
	#TempBDC
where ProductLine < 46000
GROUP BY
	DistributionCenterId,ProductLine + 46000


--print 'insert into batchdistributioncenter ' + cast(getdate() as varchar(50))
INSERT INTO
	QSPCanadaOrderManagement..BatchDistributionCenter
SELECT
	@BatchDate,
	@BatchId,
	DistributionCenterId,
	40010,
	 ProductLine 
FROM
	#TempBDC
where ProductLine > 46000
GROUP BY
	DistributionCenterId,ProductLine

END
GO
