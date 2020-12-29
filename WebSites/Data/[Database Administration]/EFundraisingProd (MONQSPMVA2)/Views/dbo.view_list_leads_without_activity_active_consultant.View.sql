USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_leads_without_activity_active_consultant]    Script Date: 02/14/2014 13:02:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_leads_without_activity_active_consultant]
AS
SELECT     TOP 100 PERCENT dbo.Department.Department_name, dbo.Lead.Lead_ID, dbo.view_list_state_code_coast.Coast, dbo.Consultant.Name, 
                      dbo.Department.Department_Id, dbo.Lead.Lead_Entry_Date
FROM         dbo.Lead INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN
                      dbo.view_list_state_code_coast ON dbo.Lead.State_Code = dbo.view_list_state_code_coast.State_Code INNER JOIN
                      dbo.Department ON dbo.Consultant.Department_ID = dbo.Department.Department_Id LEFT OUTER JOIN
                      dbo.view_list_lead_all_activity_not_completed ON dbo.Lead.Lead_ID = dbo.view_list_lead_all_activity_not_completed.Lead_Id
WHERE     (dbo.Consultant.Is_Active = 1) AND (dbo.Lead.Lead_Entry_Date < CONVERT(DATETIME, '2002-11-30 23:59:59', 102)) AND (dbo.Consultant.Is_Agent = 0) 
                      AND (dbo.Consultant.Is_Fm = 0) AND (dbo.Department.Department_Id = 7) AND (dbo.view_list_lead_all_activity_not_completed.Lead_Id IS NULL) OR
                      (dbo.Consultant.Is_Active = 1) AND (dbo.Lead.Lead_Entry_Date < CONVERT(DATETIME, '2002-11-30 23:59:59', 102)) AND (dbo.Consultant.Is_Agent = 0) 
                      AND (dbo.Consultant.Is_Fm = 0) AND (dbo.Department.Department_Id = 7) AND (dbo.view_list_lead_all_activity_not_completed.Lead_Id IS NULL)
ORDER BY dbo.Consultant.Name
GO
