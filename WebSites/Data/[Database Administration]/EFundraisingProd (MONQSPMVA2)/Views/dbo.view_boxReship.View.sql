USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_boxReship]    Script Date: 02/14/2014 13:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_boxReship]
AS
SELECT     dbo.Country.Currency_Code AS Currency_, dbo.Sale.Total_Amount - ISNULL(dbo.view_taxSummaryView.Total_Tax_Amount_Base, 0) AS Total, 
                      dbo.Sale.Sales_ID, dbo.Sale.Reship_Date, dbo.Sale.Actual_Ship_Date
FROM         dbo.Sale INNER JOIN
                      dbo.Client_Address INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code ON dbo.Sale.Client_ID = dbo.Client_Address.Client_ID AND 
                      dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code LEFT OUTER JOIN
                      dbo.view_taxSummaryView ON dbo.Sale.Sales_ID = dbo.view_taxSummaryView.Sales_ID
WHERE     (dbo.Client_Address.Address_Type = 'BT') AND (dbo.Sale.Reship_Date IS NOT NULL)
GO
