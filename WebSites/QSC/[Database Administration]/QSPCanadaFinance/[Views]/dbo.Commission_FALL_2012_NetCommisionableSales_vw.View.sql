USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[Commission_FALL_2012_NetCommisionableSales_vw]    Script Date: 06/07/2017 09:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Commission_FALL_2012_NetCommisionableSales_vw]
AS 
SELECT invoice_id, Payment_ID, SUM(Amount) AS PaymentAmount
FROM Commission_FALL_2012_PaymentRevenue_vw
GROUP BY invoice_id, Payment_ID
GO
