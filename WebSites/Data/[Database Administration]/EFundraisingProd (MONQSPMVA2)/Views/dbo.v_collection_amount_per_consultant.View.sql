USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_collection_amount_per_consultant]    Script Date: 02/14/2014 13:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_collection_amount_per_consultant]
AS
SELECT     dbo.Consultant.Consultant_ID, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, GETDATE()) AS Ageing, 
                      SUM(dbo.v_Total_Receivable_By_Sales_View.Total_Receivable) AS Total_Receivable
FROM         dbo.Consultant INNER JOIN
                      dbo.Sale ON dbo.Consultant.Consultant_ID = dbo.Sale.AR_Consultant_ID AND dbo.Consultant.Consultant_ID = dbo.Sale.AR_Consultant_ID INNER JOIN
                      dbo.v_Total_Receivable_By_Sales_View ON dbo.Sale.Sales_ID = dbo.v_Total_Receivable_By_Sales_View.Sales_ID
GROUP BY dbo.Consultant.Consultant_ID, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, GETDATE())
GO
