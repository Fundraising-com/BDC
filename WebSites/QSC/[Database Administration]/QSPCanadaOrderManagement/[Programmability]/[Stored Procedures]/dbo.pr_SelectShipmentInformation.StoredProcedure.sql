USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectShipmentInformation]    Script Date: 06/07/2017 09:20:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectShipmentInformation] 

@iShipmentID int =0

AS


select 	s.id as ShipmentID,
	s.shipmentdate,
	s.expecteddeliverydate,
	sw.waybillnumber as waybillno,
	s.numberboxes,
	s.numberskids,
	s.weight,
	s.weightunitofmeasure,
	s.comment as note,
	c.description as carriername,
	s.operatorname
  from 	shipment s,
	ShipmentWaybill sw,
	codedetail c
 where 	s.carrierid=c.instance and
	s.id = sw.shipmentid and 
	id = @iShipmentID
GO
