USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_salesShipped]    Script Date: 02/14/2014 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_salesShipped]
AS
SELECT     dbo.Country.Currency_Code AS Currency_, dbo.Sales_Item.Sales_Amount AS Total, dbo.Sale.Sales_ID
FROM         dbo.Country INNER JOIN
                      dbo.Client_Address INNER JOIN
                      dbo.Sale ON dbo.Client_Address.Client_ID = dbo.Sale.Client_ID AND dbo.Client_Address.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code ON 
                      dbo.Country.Country_Code = dbo.Client_Address.Country_Code INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID
WHERE     (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Client_Address.Address_Type = 'bt')
GO
