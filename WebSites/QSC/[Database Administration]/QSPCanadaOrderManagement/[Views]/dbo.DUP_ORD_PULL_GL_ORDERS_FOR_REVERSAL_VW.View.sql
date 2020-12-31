USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[DUP_ORD_PULL_GL_ORDERS_FOR_REVERSAL_VW]    Script Date: 06/07/2017 09:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DUP_ORD_PULL_GL_ORDERS_FOR_REVERSAL_VW] AS
SELECT DISTINCT AccountID, AccountName, FMID, FMLastName, CampaignID, InvoiceID, OrderID
FROM [DUP_ORD_FIX_ORDER_DUPE_BATCHES_INTORDER_VW] dupes
WHERE dupes.rownumber != 
	(SELECT MIN(rownumber) 
	   FROM [DUP_ORD_FIX_ORDER_DUPE_BATCHES_INTORDER_VW] dupes2 
	  WHERE dupes2.AccountID = dupes.AccountID
	    AND dupes2.InternetOrderID = dupes.InternetOrderID)
GO
