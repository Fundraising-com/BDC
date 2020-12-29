USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[VIEW4]    Script Date: 02/14/2014 13:02:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VIEW4]
AS
SELECT COUNT(dbo.Lead.Lead_ID) AS Expr1, COUNT(dbo.Sale.Sales_ID) AS Expr2, dbo.Lead.Promotion_ID, SUM(dbo.Sale.Total_Amount) AS Expr3, 
               dbo.Lead.Lead_Entry_Date
FROM  dbo.Sale INNER JOIN
               dbo.Client ON dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code AND dbo.Sale.Client_ID = dbo.Client.Client_ID RIGHT OUTER JOIN
               dbo.Lead ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID
WHERE (dbo.Lead.Promotion_ID = 10529) AND (dbo.Lead.kit_sent_date IS NULL)
GROUP BY dbo.Lead.Promotion_ID, dbo.Lead.Lead_Entry_Date
HAVING (dbo.Lead.Lead_Entry_Date BETWEEN CONVERT(DATETIME, '2004-03-24 00:00:00', 102) AND CONVERT(DATETIME, '2004-04-21 00:00:00', 102))
GO
