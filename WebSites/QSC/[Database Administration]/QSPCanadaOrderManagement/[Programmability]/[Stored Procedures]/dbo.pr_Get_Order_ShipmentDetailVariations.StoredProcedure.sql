USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_Order_ShipmentDetailVariations]    Script Date: 06/07/2017 09:19:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_Get_Order_ShipmentDetailVariations]

@OrderId int
, @SessionId varchar(50)
, @ShipmentGroupID Int

AS
set nocount on
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempShipV]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempShipV]

CREATE TABLE [dbo].[#TempShipV] (
	[OrderId] [int] NOT NULL ,
	[COHInstance] [int] NOT NULL,
	[TransId] [int] NOT NULL ,
	[ProductCode] [varchar] (100) NOT NULL,
	[ProductName] [varchar] (200) NOT NULL,
	[QtyOrdered] [int] NOT NULL ,
	[QtyShipped] [int] NOT NULL ,
	[QtyReplaced] [int] NOT NULL ,
	[ReplacementProductCode] [varchar] (100) NULL ,
	[ShipItemTF] [bit] NULL,
	[Comment] [varchar] (255) NULL ,
	[CustomerComment] [varchar] (255) NULL,
	[IsFromThisSession] [bit] NULL ,
	[IsEditable] [bit] NULL ,
	[ReplacementItemId] [int] NULL
	
) ON [PRIMARY]

--- First Populate With Temporary Storage Info From ShipmentVariations, this will override anything in the COD record
INSERT INTO
	#TempShipV
SELECT
	A.OrderId
	, C.CustomerOrderHeaderInstance
	, C.TransId
	, C.ProductCode
	, IsNull(C.ProductName, '')
	, C.Quantity
	, IsNull(D.QuantityShipped,0)
	, IsNull(D.QuantityReplaced,0)
	, IsNull(D.ReplacementItemId,'')
	, D.ShipTF
	, D.Comment
	, D.CustomerComment
	, 0
	, IsEditable = Case
		WHEN C.StatusInstance = 508 THEN 0
		WHEN C.StatusInstance = 501 THEN 0
		WHEN C.StatusInstance = 506 THEN 0
		ELSE 1
		END
	, IsNull(D.ReplacementItemId, 0)
	
FROM
	Batch A
	INNER JOIN CustomerOrderHeader B ON B.OrderBatchId = A.Id AND B.OrderBatchDate = A.Date
	INNER JOIN CustomerOrderDetail C ON C.CustomerOrderHeaderInstance = B.Instance
	INNER JOIN ShipmentVariation D ON C.CustomerOrderHeaderInstance = D.CustomerOrderHeaderInstance AND C.TransId = D.TransId
	INNER JOIN QSPCanadaCommon..QSPProductLine pl ON pl.ID = C.ProductType
WHERE	((A.OrderID = @OrderID AND (A.OrderQualifierID NOT IN (39009) OR C.IsShippedToAccount = 0))
				OR		(C.IsShippedToAccount = 1 AND A.OrderID IN (SELECT DISTINCT OnlineOrderID  
																	 FROM OnlineOrderMappingTable  
																	 WHERE LandedOrderID = @OrderID)))	--AND D.SessionId LIKE @SessionId
AND		(@ShipmentGroupID IS NULL OR pl.ShipmentGroupID = @ShipmentGroupID)

	--AND C.StatusInstance IN (500, 502, 508, 509)   --- get list of valid instances to pull from
	--AND Convert(varchar(10), B.Instance) + ':' +  Convert(varchar(10), C.TransId) NOT IN (SELECT Convert(varchar(10),  #TempShipV.COHInstance) + ':' +  Convert(varchar(10), #TempShipV.TransId)  FROM #TempShipV)		--- ignore stuff already in temp storage
ORDER BY
	C.ProductCode


--- Next Populate With Customer Order Detail information

INSERT INTO
	#TempShipV
SELECT
	A.OrderId
	, C.CustomerOrderHeaderInstance
	, C.TransId
	, C.ProductCode
	, IsNull(C.ProductName, '')
	, C.Quantity
	, IsNull(C.QuantityShipped,0)
	, IsNull(C.ReplacedProductQty,0)
	, IsNull(C.ReplacedProductCode,'')
	, 1
	, ''
	, ''
	, 0
	, IsEditable = Case
		WHEN C.StatusInstance = 508 THEN 0
		WHEN C.StatusInstance = 501 THEN 0
		WHEN C.StatusInstance = 506 THEN 0
		ELSE 1
		END
	, 0
	
FROM
	Batch A
	INNER JOIN CustomerOrderHeader B ON B.OrderBatchId = A.Id AND B.OrderBatchDate = A.Date
	INNER JOIN CustomerOrderDetail C ON C.CustomerOrderHeaderInstance = B.Instance
	INNER JOIN QSPCanadaCommon..QSPProductLine pl ON pl.ID = C.ProductType
WHERE	((A.OrderID = @OrderID AND (A.OrderQualifierID NOT IN (39009) OR C.IsShippedToAccount = 0))
				OR		(C.IsShippedToAccount = 1 AND A.OrderID IN (SELECT DISTINCT OnlineOrderID  
																	 FROM OnlineOrderMappingTable  
																	 WHERE LandedOrderID = @OrderID)))	
	--AND D.SessionId LIKE @SessionId
	AND	C.DistributionCenterID = 1
	AND C.StatusInstance IN (500, 502, 508, 510,511)   --- get list of valid instances to pull from
	AND Convert(varchar(10), B.Instance) + ':' +  Convert(varchar(10), C.TransId) NOT IN (SELECT Convert(varchar(10),  #TempShipV.COHInstance) + ':' +  Convert(varchar(10), #TempShipV.TransId)  FROM #TempShipV)		--- ignore stuff already in temp storage
	AND	(@ShipmentGroupID IS NULL OR pl.ShipmentGroupID = @ShipmentGroupID)
ORDER BY
	C.ProductCode


SELECT
	*
FROM
	#TempShipV
ORDER BY
	ProductCode
GO
