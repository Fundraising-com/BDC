USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_Order_DC_Breakdown]    Script Date: 06/07/2017 09:19:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_Order_DC_Breakdown]

@DistributionCenterId int = null
, @OrderId int

AS

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempBreakdown]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempBreakdown]

CREATE TABLE [dbo].[#TempBreakdown] (
	[PricingDetailsId] [int] NOT NULL ,
	[ItemQuantity] [int] NULL,
	[DistributionCenterId] [int] NULL,
	[ProductLine] [int] NULL
) ON [PRIMARY]

INSERT INTO
	#TempBreakdown
SELECT
	C.PricingDetailsId
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


--- UPDATE THE DistributionCenterId IF THE ProductLine has an absolute DistributionCenterId
UPDATE
	#TempBreakdown
SET
	DistributionCenterId = B.DistributionCenterId
FROM
	#TempBreakdown A
	INNER JOIN QSPCanadaCommon..QSPProductLine B ON B.Id = (A.ProductLine + 46000)
WHERE
	B.DistributionCenterId is not null


SELECT
	A.DistributionCenterId,
	B.Name,
	Sum(A.ItemQuantity) As 'ItemQuantity'
FROM 
	#TempBreakdown A
	INNER JOIN QSPCanadaOrderManagement..DistributionCenter B ON A.DistributionCenterId = B.Id
GROUP BY
	A.DistributionCenterId,
	B.Name
ORDER BY
	B.Name
GO
