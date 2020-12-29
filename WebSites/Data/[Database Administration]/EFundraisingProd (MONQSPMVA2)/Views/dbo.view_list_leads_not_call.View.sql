USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_leads_not_call]    Script Date: 02/14/2014 13:02:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_leads_not_call]
AS
SELECT     dbo.view_list_count_lead_activity_by_type.Lead_Id, dbo.view_list_count_lead_activity_by_type.count_activity
FROM         dbo.view_list_count_lead_activity_by_type INNER JOIN
                      dbo.view_list_lead_activity_not_completed_resheduled_first_call ON 
                      dbo.view_list_count_lead_activity_by_type.Lead_Id = dbo.view_list_lead_activity_not_completed_resheduled_first_call.Lead_Id AND 
                      dbo.view_list_count_lead_activity_by_type.Lead_Activity_Type_Id = dbo.view_list_lead_activity_not_completed_resheduled_first_call.Lead_Activity_Type_Id
WHERE     (dbo.view_list_count_lead_activity_by_type.Lead_Activity_Type_Id = 7)
GROUP BY dbo.view_list_count_lead_activity_by_type.Lead_Id, dbo.view_list_count_lead_activity_by_type.count_activity
HAVING      (dbo.view_list_count_lead_activity_by_type.count_activity > 1)
GO
