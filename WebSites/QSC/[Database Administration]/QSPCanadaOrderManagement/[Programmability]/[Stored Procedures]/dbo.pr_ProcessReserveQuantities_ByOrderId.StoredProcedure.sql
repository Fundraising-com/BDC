USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProcessReserveQuantities_ByOrderId]    Script Date: 06/07/2017 09:20:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
--+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+
--  This proc will reserve the quantities for a batch, set the cod status to picked and set the 
--  overall status to picked.
--+--+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+--+-+-+-+


CREATE    PROCEDURE [dbo].[pr_ProcessReserveQuantities_ByOrderId]

@DistributionCenterId int = null
, @OrderId int

AS

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempRQ]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempRQ]

CREATE TABLE [dbo].[#TempRQ] (
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransId] [int] NOT NULL,
	[PricingDetailsId] [int] NOT NULL ,
	[ProductCode] [varchar](50) NOT NULL,
	[ItemQuantity] [int] NULL,
	[DistributionCenterId] [int] NULL,
	[ProductLine] [int] NULL
) ON [PRIMARY]



DECLARE @BatchDate datetime, @BatchId int

SELECT
	@BatchDate = [Date]
	, @BatchId = [Id]
FROM
	QSPCanadaOrderManagement..Batch
WHERE
	OrderId = @OrderId



INSERT INTO
	#TempRQ
SELECT
	C.CustomerOrderHeaderInstance
	, C.TransId
	, C.PricingDetailsId
	, C.ProductCode
	, Quantity
	, @DistributionCenterId
	, E.ProductLine
FROM
	QSPCanadaOrderManagement..Batch A
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader B ON B.OrderBatchDate = A.[Date] AND B.OrderBatchId = A.[Id]
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderDetail C ON C.CustomerOrderHeaderInstance = B.Instance
	INNER JOIN QSPCanadaProduct..Pricing_Details D ON D.MagPrice_Instance = C.PricingDetailsId
	INNER JOIN QSPCanadaProduct..Product E ON E.Product_Year = D.Pricing_Year AND E.Product_Season = D.Pricing_Season AND E.Product_Code = D.Product_Code
WHERE
	A.OrderId = @OrderId
	AND C.ProductType <> 46001
	AND C.PricingDetailsID <> 0



--- UPDATE THE DistributionCenterId IF THE ProductLine has an absolute DistributionCenterId
UPDATE
	#TempRQ
SET
	DistributionCenterId = B.DistributionCenterId
FROM
	#TempRQ A
	INNER JOIN QSPCanadaCommon..QSPProductLine B ON B.Id = (A.ProductLine + 46000)
WHERE
	B.DistributionCenterId is not null


--INSERT INTO
	--QSPCanadaOrderManagement..BatchDistributionCenter
SELECT
	DistributionCenterId,
	ProductCode
FROM
	#TempRQ
GROUP BY
	ProductCode
	, DistributionCenterId


SELECT * FROM #TempRQ

---- LOOP THROUGH AND Reserve Qty For Each Product Code/DistributionCenter

DECLARE @DistributionCenterIdTemp int, @ProductCode varchar(50)

DECLARE C1 CURSOR FOR
	SELECT
		DistributionCenterId,
		ProductCode
	FROM
		#TempRQ
	GROUP BY
		ProductCode
		, DistributionCenterId

OPEN C1
	FETCH NEXT FROM C1 INTO	
		@DistributionCenterIdTemp,@ProductCode

WHILE @@Fetch_Status = 0
	BEGIN
		
		--PRINT 'ProductCode: ' + @ProductCode + ' DC: ' + Convert(varchar, @DistributionCenterIdTemp)
			
		exec pr_FSReserveProductQty @OrderId, @DistributionCenterIdTemp, @ProductCode

		FETCH NEXT FROM C1 INTO	
		@DistributionCenterIdTemp,@ProductCode

	END

CLOSE C1
DEALLOCATE C1



---- Update COD's affected with new status.
UPDATE
	QSPCanadaOrderManagement..CustomerOrderDetail
SET
	StatusInstance = 509
	, DistributionCenterId = B.DistributionCenterId
FROM
	QSPCanadaOrderManagement..CustomerOrderDetail A
	INNER JOIN #TEMPRQ B ON A.CustomerOrderHeaderInstance = B.CustomerOrderHeaderInstance AND A.TransId = B.TransId
	where B.DistributionCenterID=2

UPDATE
	QSPCanadaOrderManagement..CustomerOrderDetail
SET
	StatusInstance = 511
	, DistributionCenterId = B.DistributionCenterId
FROM
	QSPCanadaOrderManagement..CustomerOrderDetail A
	INNER JOIN #TEMPRQ B ON A.CustomerOrderHeaderInstance = B.CustomerOrderHeaderInstance AND A.TransId = B.TransId
	where B.DistributionCenterID=1



---- UPDATE THE OVERALL BATCH STATUS HERE
UPDATE 
	QSPCanadaOrderManagement..Batch
SET
	StatusInstance = 40010
WHERE
	OrderId = @OrderId
GO
