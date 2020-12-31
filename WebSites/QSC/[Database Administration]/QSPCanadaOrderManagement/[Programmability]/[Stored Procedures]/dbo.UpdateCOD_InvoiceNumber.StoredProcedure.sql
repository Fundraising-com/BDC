USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCOD_InvoiceNumber]    Script Date: 06/07/2017 09:20:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateCOD_InvoiceNumber]
	@OrderID		int,
	@InvoiceID		int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/18/2004 
--   Update the COD 'InvoiceNumber' field after completing GenerateInvoices process For Canada Finance System.
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

UPDATE QSPCanadaOrderManagement..CustomerOrderDetail 
SET InvoiceNumber = @InvoiceID 
WHERE CustomerOrderHeaderInstance IN (SELECT Instance
					   FROM QSPCanadaOrderManagement..CustomerOrderHeader COH
					   INNER JOIN QSPCanadaOrderManagement..Batch B on OrderBatchDate=Date AND OrderBatchID = ID
					   WHERE B.OrderID = @OrderID)

SET NOCOUNT OFF
GO
