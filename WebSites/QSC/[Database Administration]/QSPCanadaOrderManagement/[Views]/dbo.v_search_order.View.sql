USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[v_search_order]    Script Date: 06/07/2017 09:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_search_order]
AS
SELECT DISTINCT 
                      coh.Instance AS OrderID, ch.Description AS status, coh.CreationDate AS OrderDate, coh.Type AS OrderType, coh.CampaignID, 
                      coh.AccountID AS ShipToGroupID, cShipTo.LastName + cShipTo.FirstName AS ShipToGroupName, cShipTo.City AS ShipToGroupCity, 
                      cShipTo.Zip AS ShipToGroupPostalCode, coh.AccountID AS BillToGroupID, cBillTo.LastName + cBillTo.FirstName AS BillToGroupName, 
                      cBillTo.City AS BillToGroupCity, cBillTo.Zip AS BillToGroupPostalCode, inv.INVOICE_DATE AS InvoiceDate, cod.InvoiceNumber AS InvoiceID
FROM         QSPCanadaCommon.dbo.Campaign cp INNER JOIN
                      QSPCanadaCommon.dbo.FieldManager fm ON cp.FMID = fm.FMID INNER JOIN
                      dbo.CustomerOrderDetail cod INNER JOIN
                      QSPCanadaCommon.dbo.CodeHeader ch ON cod.StatusInstance = ch.Instance INNER JOIN
                      dbo.CustomerOrderHeader coh ON cod.CustomerOrderHeaderInstance = coh.Instance INNER JOIN
                      dbo.Customer cShipToGroup ON cod.CustomerShipToInstance = cShipToGroup.Instance INNER JOIN
                      dbo.Customer cBillTo ON coh.CustomerBillToInstance = cBillTo.Instance INNER JOIN
                      QSPCanadaFinance.dbo.INVOICE inv ON cod.InvoiceNumber = inv.INVOICE_ID ON cp.ID = coh.CampaignID CROSS JOIN
                      QSPCanadaCommon.dbo.CAccount acc CROSS JOIN
                      dbo.Customer cShipTo
GO
