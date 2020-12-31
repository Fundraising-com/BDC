USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[VerifyInventoryPush]    Script Date: 06/07/2017 09:20:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[VerifyInventoryPush] AS

declare @shipmentid int
select @shipmentid= max(shipmentbatchid) from QSPCanadaordermanagement..shipmentorder
select p.OracleCode,sum(quantityshipped) as Qty 
into #Check from QSPCanadaordermanagement..shipmentorder so,
		QSPCanadaordermanagement..Batch b,
		QSPCanadaordermanagement..CustomerOrderheader coh,
		QSPCanadaordermanagement..CustomerOrderDetail cod,
		QSPCanadaproduct..Pricing_details pd,
		QSPCanadaproduct..Product p
		 where shipmentbatchid=@shipmentid
			and b.orderid= so.orderid
			and coh.orderbatchid=b.id and coh.orderbatchdate= date
			and coh.instance = cod.customerorderheaderinstance
			and pd.magprice_instance = pricingdetailsid
			and pd.product_instance = p.product_instance
			and COD.StatusInstance =508 
 and so.ShipmentID = cod.ShipmentID
			and b.orderid in
(
select orderid from QSPCanadaordermanagement..shipmentorder where shipmentbatchid=@shipmentid
)
group by p.OracleCode


select segment1,quantity,qty,* from QSPOracleInterface..OM_TBL_INV_TRANS_INTERFACE,#Check where Segment1+SEGMENT2+SEGMENT3=Oraclecode
and quantity<>Qty 
drop table #Check
GO
