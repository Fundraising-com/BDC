USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetProductInfo]    Script Date: 06/07/2017 09:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_GetProductInfo] AS
select 	b.OrderID,
	cod.ProductType,
	cod.ProductCode AS CatalogCode,
	cod.ProductName AS ItemDescription,
	cod.Quantity AS QuantityOrdered,
	cod.Price AS PriceEntered,
	cod.CatalogPrice AS CatalogPrice,
	cod.OverrideProduct AS OverrideCode,
	cd.Description AS OrderItemStatus,
	ship.ShipmentDate AS ShipDate,
	cod.Recipient AS PurchasedBy,
	s.FirstName AS StudentFirstName,
	s.LastName AS StudentLastName
  from  Batch b,
	CustomerOrderHeader coh,
	
	student s,
	CodeDetail cd,
	CustomerOrderDetail cod left outer join 
	Shipment ship on cod.shipmentid=ship.id
 where  b.id = coh.OrderBatchID and
	b.Date = coh.OrderBatchDate and
	coh.Instance = cod.CustomerOrderHeaderInstance and
	s.instance = coh.studentinstance and
	cd.Instance = cod.StatusInstance
GO
