USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_wfc_leads]    Script Date: 02/14/2014 13:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_wfc_leads]
AS
SELECT     TOP 100 PERCENT dbo.Lead.Lead_ID, MONTH(dbo.Lead.Lead_Entry_Date) AS lead_month, YEAR(dbo.Lead.Lead_Entry_Date) AS lead_year, 
                      dbo.Lead.State_Code, dbo.Consultant.Is_Fm, COUNT(*) AS Expr1
FROM         dbo.Lead INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID
WHERE     (dbo.Lead.Promotion_ID = 672) AND (dbo.Consultant.Department_ID = 7 OR
                      dbo.Consultant.Department_ID = 9) OR
                      (dbo.Lead.Promotion_ID = 683) AND (dbo.Consultant.Department_ID = 7 OR
                      dbo.Consultant.Department_ID = 9)
GROUP BY dbo.Lead.Lead_ID, MONTH(dbo.Lead.Lead_Entry_Date), YEAR(dbo.Lead.Lead_Entry_Date), dbo.Lead.State_Code, dbo.Consultant.Is_Fm
ORDER BY dbo.Consultant.Is_Fm
GO
