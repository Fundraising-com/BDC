USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_payment_active]    Script Date: 02/14/2014 13:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_payment_active]
AS
SELECT     TOP 100 PERCENT YEAR(dbo.Lead.Lead_Entry_Date) AS [Year], MONTH(dbo.Lead.Lead_Entry_Date) AS [Month], SUM(dbo.Payment.Payment_Amount) 
                      AS SumPayment
FROM         dbo.Lead INNER JOIN
                      dbo.Client ON dbo.Lead.Lead_ID = dbo.Client.Lead_ID INNER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
                      dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID
WHERE     (dbo.Promotion.Partner_ID = 8)
GROUP BY MONTH(dbo.Lead.Lead_Entry_Date), YEAR(dbo.Lead.Lead_Entry_Date)
ORDER BY YEAR(dbo.Lead.Lead_Entry_Date), MONTH(dbo.Lead.Lead_Entry_Date)
GO
