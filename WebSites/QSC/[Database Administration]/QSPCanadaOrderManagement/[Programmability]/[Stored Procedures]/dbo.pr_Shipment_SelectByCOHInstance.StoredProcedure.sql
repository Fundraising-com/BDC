USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Shipment_SelectByCOHInstance]    Script Date: 06/07/2017 09:20:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Shipment_SelectByCOHInstance]

	@iCustomerOrderHeaderInstance		int,
	@iTransID				int

AS

SELECT	s.ID as ID,
		coalesce(s.CarrierID, 0) as CarrierID,
		coalesce(s.ShipmentDate, '') as ShipmentDate,
		coalesce(s.CountryCode, '') as CountryCode,
		coalesce(s.ExpectedDeliveryDate, '') as ExpectedDeliveryDate,
		coalesce(s.NumberBoxes, 0) as NumberBoxes,
		coalesce(s.Weight, 0.0) as Weight,
		coalesce(s.DateModified, '') as DateModified,
		coalesce(s.UserIDModified, '') as UserIDModified,
		coalesce(s.OperatorName, '') as OperatorName,
		coalesce(s.NumberSkids, 0) as NumberSkids,
		coalesce(s.WeightUnitOfMeasure, '') as WeightUnitOfMeasure,
		coalesce(s.Comment, '') as Comment,
		coalesce(s.FMEmailNotificationSent, '') as FMEmailNotificationSent

FROM		QSPCanadaOrderManagement..Shipment s,
		QSPCanadaOrderManagement..CustomerOrderDetail cod

WHERE	s.ID = cod.ShipmentID
AND		cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		cod.TransID = @iTransID
GO
