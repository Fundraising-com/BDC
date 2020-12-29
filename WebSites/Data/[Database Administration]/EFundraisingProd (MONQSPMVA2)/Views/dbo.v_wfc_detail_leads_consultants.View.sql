USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_wfc_detail_leads_consultants]    Script Date: 02/14/2014 13:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_wfc_detail_leads_consultants]
AS
SELECT     TOP 100 PERCENT YEAR(dbo.Lead.Lead_Entry_Date) AS lead_year, MONTH(dbo.Lead.Lead_Entry_Date) AS lead_month, dbo.Lead.Lead_ID, 
                      dbo.Lead.State_Code, dbo.Consultant.Is_Fm, dbo.Consultant.Name AS consultant_name, dbo.Lead.Participant_Count
FROM         dbo.Lead INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID
WHERE     (dbo.Lead.Promotion_ID = 672) AND (dbo.Consultant.Department_ID = 7 OR
                      dbo.Consultant.Department_ID = 9) AND (dbo.Consultant.Is_Fm = 0) OR
                      (dbo.Lead.Promotion_ID = 683) AND (dbo.Consultant.Department_ID = 7 OR
                      dbo.Consultant.Department_ID = 9) AND (dbo.Consultant.Is_Fm = 0)
GO
