USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Adj_Tax_Sales]    Script Date: 02/14/2014 13:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Adj_Tax_Sales]
AS
SELECT     TOP 100 PERCENT Sales_Id, Adjustement_No, SUM(Tax_Amount) AS Tot_Tax_Amount, 
                      SUM(CASE WHEN [Tax_Code] = 'GST' THEN [Tax_Amount] ELSE 0 END) AS GST, 
                      SUM(CASE WHEN [Tax_Code] = 'QST' THEN [Tax_Amount] ELSE 0 END) AS QST
FROM         dbo.Applicable_Adjustment_Tax
GROUP BY Sales_Id, Adjustement_No
ORDER BY Sales_Id
GO
