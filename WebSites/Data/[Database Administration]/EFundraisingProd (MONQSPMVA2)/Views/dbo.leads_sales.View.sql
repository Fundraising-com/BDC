USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[leads_sales]    Script Date: 02/14/2014 13:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[leads_sales]
AS
SELECT     dbo.Lead.Lead_ID, dbo.Client.Client_Sequence_Code, dbo.Client.Client_ID, dbo.Sale.Sales_ID
FROM         dbo.Lead INNER JOIN
                      dbo.Client ON dbo.Lead.Lead_ID = dbo.Client.Lead_ID INNER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID
WHERE     (dbo.Lead.Lead_ID = 259239)
GO
