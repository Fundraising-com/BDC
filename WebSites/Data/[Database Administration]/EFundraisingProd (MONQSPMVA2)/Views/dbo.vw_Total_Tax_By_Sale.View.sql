USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_Total_Tax_By_Sale]    Script Date: 02/14/2014 13:02:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_Total_Tax_By_Sale]
AS
SELECT     dbo.Sale.Sales_ID, SUM(dbo.Applicable_Tax.Tax_Amount) AS Total_Tax, SUM(CASE WHEN [Tax_Code] = 'GST' THEN [Tax_Amount] ELSE 0 END) 
                      AS GST, SUM(CASE WHEN [Tax_Code] = 'QST' THEN [Tax_Amount] ELSE 0 END) AS QST
FROM         dbo.Sale INNER JOIN
                      dbo.Applicable_Tax ON dbo.Sale.Sales_ID = dbo.Applicable_Tax.Sales_ID
GROUP BY dbo.Sale.Sales_ID
GO
