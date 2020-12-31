USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[DUP_ORD_FIX_ORDER_DUPE_BATCHES_INTORDER_VW]    Script Date: 06/07/2017 09:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DUP_ORD_FIX_ORDER_DUPE_BATCHES_INTORDER_VW] AS 
SELECT ROW_NUMBER() OVER (ORDER BY AccountID, InternetOrderID, OrderID) as    RowNumber,
AccountID, 
AccountName, 
InvoiceID,
OrderID,
InternetOrderID,
FMID, 
FMLastName, 
CampaignID
FROM DUP_ORD_FIX_DUPLICATE_BATCHES_INTORDER_VW
GO
