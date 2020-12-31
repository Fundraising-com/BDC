USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateOrderPrintStatus]    Script Date: 06/07/2017 09:20:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_UpdateOrderPrintStatus]

@pOrderID int,
@pShipmentGroupID int

AS

--saqib shah - may 2005
--update the order print status...mark the user selected order as printed...

update	QspCanadaorderManagement.dbo.ReportRequestBatch
set		Isprinted  = 1, DatePrinted = Getdate()
where	BatchOrderId = @pOrderID
and		(ShipmentGroupID = @pShipmentGroupID OR @pShipmentGroupID IS NULL)
GO
