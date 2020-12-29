USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_wfc_leads_sales_consult]    Script Date: 02/14/2014 13:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_wfc_leads_sales_consult]
AS
SELECT     dbo.Lead.Lead_ID, MONTH(dbo.Lead.Lead_Entry_Date) AS lead_month, YEAR(dbo.Lead.Lead_Entry_Date) AS lead_year, dbo.Lead.State_Code, 
                      COUNT(dbo.Sale.Sales_ID) AS NbSales, MONTH(dbo.Sale.Sales_Date) AS sales_month, YEAR(dbo.Sale.Sales_Date) AS sales_year, 
                      dbo.Consultant.Is_Fm, dbo.Sale.Total_Amount, dbo.Lead.Participant_Count
FROM         dbo.Lead INNER JOIN
                      dbo.Client ON dbo.Lead.Lead_ID = dbo.Client.Lead_ID INNER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID
WHERE     (dbo.Lead.Promotion_ID = 672) AND (dbo.Consultant.Department_ID = 7 OR
                      dbo.Consultant.Department_ID = 9) AND (dbo.Consultant.Is_Fm = 0) OR
                      (dbo.Lead.Promotion_ID = 683) AND (dbo.Consultant.Department_ID = 7 OR
                      dbo.Consultant.Department_ID = 9) AND (dbo.Consultant.Is_Fm = 0)
GROUP BY dbo.Lead.Lead_ID, MONTH(dbo.Lead.Lead_Entry_Date), MONTH(dbo.Sale.Sales_Date), YEAR(dbo.Sale.Sales_Date), 
                      YEAR(dbo.Lead.Lead_Entry_Date), dbo.Lead.State_Code, dbo.Consultant.Is_Fm, dbo.Sale.Total_Amount, dbo.Lead.Participant_Count
GO
