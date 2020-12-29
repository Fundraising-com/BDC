USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_salesNetShipped]    Script Date: 02/14/2014 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_salesNetShipped]
AS
SELECT     dbo.view_salesShipped.Currency_, ISNULL(dbo.view_salesShipped.Total, 0) - ISNULL(dbo.view_boxReturn.Total, 0) 
                      + ISNULL(dbo.view_boxReship.Total, 0) + ISNULL(dbo.view_netShipping.Net_Shipping, 0) AS Total, dbo.view_salesShipped.Sales_ID
FROM         dbo.view_salesShipped LEFT OUTER JOIN
                      dbo.view_boxReship ON dbo.view_salesShipped.Sales_ID = dbo.view_boxReship.Sales_ID LEFT OUTER JOIN
                      dbo.view_boxReturn ON dbo.view_salesShipped.Sales_ID = dbo.view_boxReturn.Sales_ID INNER JOIN
                      dbo.view_netShipping ON dbo.view_salesShipped.Sales_ID = dbo.view_netShipping.Sales_ID
GO
