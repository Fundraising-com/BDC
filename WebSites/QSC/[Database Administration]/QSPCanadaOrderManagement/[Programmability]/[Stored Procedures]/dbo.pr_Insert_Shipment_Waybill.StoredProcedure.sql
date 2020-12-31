USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Insert_Shipment_Waybill]    Script Date: 06/07/2017 09:20:10 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Insert_Shipment_Waybill]

@ShipmentId int,
@WaybillNumber varchar(50)

AS

INSERT INTO
	ShipmentWaybill
	( 
		ShipmentId
		, WaybillNumber
	) VALUES (
		@ShipmentId
		, @WaybillNumber
	)
GO
