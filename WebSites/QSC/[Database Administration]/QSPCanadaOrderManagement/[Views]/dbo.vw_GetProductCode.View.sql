USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetProductCode]    Script Date: 06/07/2017 09:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vw_GetProductCode] AS
select 	b.OrderID,
	cod.ProductType,
	cod.ProductCode AS CatalogCode,
	cod.ProductName AS ItemDescription,
	cod.Quantity AS QuantityOrdered,
	cod.Price AS PriceEntered,
	cod.CatalogPrice AS CatalogPrice,
	cod.OverrideProduct AS OverrideCode,
	cd.Description AS OrderItemStatus,
	getDate() AS ShipDate,
	cod.SupporterName AS PurchasedBy,
	s.FirstName AS StudentFirstName,
	s.LastName AS StudentLastName
  from  Batch b,
	CustomerOrderHeader coh,
	CustomerOrderDetail cod,
	student s,
	CodeDetail cd
 where  b.id = coh.OrderBatchID and
	b.Date = coh.OrderBatchDate and
	coh.Instance = cod.CustomerOrderHeaderInstance and
	s.instance = coh.studentinstance and
	cd.Instance = cod.StatusInstance
GO
