USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_netShipping]    Script Date: 02/14/2014 13:02:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_netShipping]
AS
SELECT     dbo.Sale.Sales_ID, dbo.Country.Currency_Code AS Currency_, dbo.Sale.Shipping_Fees - dbo.Sale.Shipping_Fees_Discount AS Net_Shipping
FROM         dbo.Client_Address INNER JOIN
                      dbo.Client INNER JOIN
                      dbo.Sale ON dbo.Client.Client_ID = dbo.Sale.Client_ID AND dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code ON 
                      dbo.Client_Address.Client_ID = dbo.Client.Client_ID AND dbo.Client_Address.Client_Sequence_Code = dbo.Client.Client_Sequence_Code INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code
WHERE     (dbo.Client_Address.Address_Type = 'BT')
GO
