USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_leads_mng_1rst_calls]    Script Date: 02/14/2014 13:02:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_leads_mng_1rst_calls]
AS
SELECT     TOP 100 PERCENT dbo.view_list_count_lead_activity_by_type.Lead_Id, dbo.view_list_count_lead_activity_by_type.count_activity, 
                      dbo.Consultant.Name, dbo.Lead_Activity_Type.Description
FROM         dbo.view_list_count_lead_activity_by_type INNER JOIN
                      dbo.view_list_lead_activity_not_completed_resheduled_first_call ON 
                      dbo.view_list_count_lead_activity_by_type.Lead_Id = dbo.view_list_lead_activity_not_completed_resheduled_first_call.Lead_Id AND 
                      dbo.view_list_count_lead_activity_by_type.Lead_Activity_Type_Id = dbo.view_list_lead_activity_not_completed_resheduled_first_call.Lead_Activity_Type_Id
                       INNER JOIN
                      dbo.Lead ON dbo.view_list_count_lead_activity_by_type.Lead_Id = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN
                      dbo.Lead_Activity_Type ON 
                      dbo.view_list_count_lead_activity_by_type.Lead_Activity_Type_Id = dbo.Lead_Activity_Type.Lead_Activity_Type_Id
WHERE     (dbo.view_list_count_lead_activity_by_type.Lead_Activity_Type_Id = 7) AND 
                      (dbo.view_list_lead_activity_not_completed_resheduled_first_call.Lead_Activity_Date BETWEEN CONVERT(DATETIME, '2003-01-01 00:00:00', 102) 
                      AND CONVERT(DATETIME, '2003-01-31 23:59:59', 102))
GROUP BY dbo.view_list_count_lead_activity_by_type.Lead_Id, dbo.view_list_count_lead_activity_by_type.count_activity, dbo.Consultant.Name, 
                      dbo.Lead_Activity_Type.Description
HAVING      (dbo.view_list_count_lead_activity_by_type.count_activity > 4)
ORDER BY dbo.Consultant.Name DESC
GO
