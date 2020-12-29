USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_sales_local_sponsor_found]    Script Date: 02/14/2014 13:02:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_sales_local_sponsor_found]
AS
SELECT     dbo.Sale.Sales_ID, 'Yes' AS Local_Sponsor_Found
FROM         dbo.Sale INNER JOIN
                      dbo.Local_Sponsor_Sales_Item ON dbo.Sale.Sales_ID = dbo.Local_Sponsor_Sales_Item.Sales_ID
GROUP BY dbo.Sale.Sales_ID
GO
