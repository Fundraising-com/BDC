USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Paid_Tot_Sales]    Script Date: 02/14/2014 13:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Paid_Tot_Sales]
AS
SELECT     Sales_ID, SUM(Payment_Amount) AS Total_Paid
FROM         dbo.Payment
GROUP BY Sales_ID
GO
