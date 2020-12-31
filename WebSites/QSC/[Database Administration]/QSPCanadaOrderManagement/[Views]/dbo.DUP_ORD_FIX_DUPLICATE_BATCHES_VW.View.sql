USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[DUP_ORD_FIX_DUPLICATE_BATCHES_VW]    Script Date: 06/07/2017 09:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DUP_ORD_FIX_DUPLICATE_BATCHES_VW] AS 
	SELECT      ac.ID AS AccountID, ac.Name AS AccountName, inv.INVOICE_ID AS InvoiceID, b.OrderID AS OrderID
	FROM        QSPCanadaOrderManagement..InternetOrderID             ioi
	JOIN        QSPCanadaOrderManagement..customerorderheader         coh  ON ioi.CustomerOrderHeaderInstance = coh.instance
	JOIN        QSPCanadaOrderManagement..CustomerOrderDetail         cod  ON cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN        QSPCanadaOrderManagement..batch                 b    ON coh.orderbatchid = b.id and coh.orderbatchdate = b.date
	JOIN        QSPCanadaOrderManagement..Account               ac   ON b.AccountID = ac.ID
	JOIN        QSPCanadaCommon..Campaign                       ca   ON b.CampaignID = ca.ID
	JOIN        QSPCanadaCommon..FieldManager                   fm   ON ca.FMID = fm.FMID
	JOIN		QSPCanadaFinance..INVOICE		inv ON inv.ORDER_ID = b.OrderID
	JOIN QSPCanadaFinance..GL_ENTRY ge ON ge.INVOICE_ID = inv.INVOICE_ID
	JOIN QSPCanadaFinance..GL_TRANSACTION gt ON gt.GL_ENTRY_ID = ge.GL_ENTRY_ID
	JOIN QSPCanadaFinance.dbo.GLAccount ga ON ga.GLAccountID = gt.GLAccountID
	WHERE       ioi.InternetOrderID IN (
	SELECT      InternetOrderID
	FROM        QSPCanadaOrderManagement..InternetOrderID             ioi
	JOIN        QSPCanadaOrderManagement..customerorderheader         coh  ON ioi.CustomerOrderHeaderInstance = coh.instance
	JOIN        QSPCanadaOrderManagement..batch                 b    ON coh.orderbatchid = b.id and coh.orderbatchdate = b.date
	WHERE       b.statusinstance <> 40005
	AND               b.DateCreated >= '2011-07-01'
	GROUP BY    InternetOrderID
	HAVING COUNT(InternetOrderID) > 1)
	GROUP BY ac.ID, ac.Name, inv.INVOICE_ID,b.OrderID
GO
