USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectShipmentDetails]    Script Date: 06/07/2017 09:20:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectShipmentDetails]

@iShipmentID int =0

 AS


select 	cod.shipmentid,
	cod.ProductCode,
	cod.ProductName,
	sum(cod.Quantity) AS QuantityOrdered,
	sum(cod.QuantityShipped) AS QuantityShipped
  from	customerorderdetail cod
 where	cod.shipmentid = @iShipmentID and
	cod.producttype > 46001
 group by
	ShipmentID,
	ProductCode,
	ProductName
GO
