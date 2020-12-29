USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_3_school_sports_groups_leads]    Script Date: 02/14/2014 13:02:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_3_school_sports_groups_leads]
AS
SELECT     TOP 100 PERCENT dbo.Department.Department_name, dbo.Lead.Lead_ID, dbo.Lead.Organization, dbo.view_list_state_code_coast.Coast, 
                      dbo.Consultant.Name, dbo.Group_Type.Description, dbo.Lead.Participant_Count, dbo.Organization_Type.Organization_Type_Desc
FROM         dbo.Lead INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN
                      dbo.view_list_state_code_coast ON dbo.Lead.State_Code = dbo.view_list_state_code_coast.State_Code INNER JOIN
                      dbo.Department ON dbo.Consultant.Department_ID = dbo.Department.Department_Id INNER JOIN
                      dbo.Group_Type ON dbo.Lead.Group_Type_ID = dbo.Group_Type.Group_Type_ID INNER JOIN
                      dbo.Organization_Type ON dbo.Lead.Organization_Type_Id = dbo.Organization_Type.Organization_Type_Id INNER JOIN
                      dbo.view_list_not_harmony_transfer_salesman ON 
                      dbo.Consultant.Consultant_ID = dbo.view_list_not_harmony_transfer_salesman.Consultant_ID LEFT OUTER JOIN
                      dbo.view_list_lead_all_activity_not_completed ON dbo.Lead.Lead_ID = dbo.view_list_lead_all_activity_not_completed.Lead_Id
WHERE     (dbo.view_list_lead_all_activity_not_completed.Lead_Activity_Date IS NULL) AND (dbo.Consultant.Is_Active = 1) AND 
                      (dbo.Lead.Lead_Entry_Date < CONVERT(DATETIME, '2002-11-30 23:59:59', 102)) AND (dbo.Lead.Participant_Count < 100) AND 
                      (NOT (dbo.Group_Type.Group_Type_ID IN (5, 16, 18, 19, 42, 22, 27, 37, 38, 40, 41, 43, 99))) AND 
                      (NOT (dbo.Organization_Type.Organization_Type_Id IN (1, 7, 8, 23, 24, 25, 26, 27, 99))) AND (dbo.Consultant.Is_Agent = 0) AND 
                      (dbo.Consultant.Is_Fm = 0) AND (dbo.Department.Department_Id = 7) OR
                      (dbo.view_list_lead_all_activity_not_completed.Lead_Activity_Date < CONVERT(DATETIME, '2002-11-30 23:59:59', 102)) AND 
                      (dbo.Consultant.Is_Active = 1) AND (dbo.Lead.Lead_Entry_Date < CONVERT(DATETIME, '2002-11-30 23:59:59', 102)) AND 
                      (dbo.Lead.Participant_Count < 100) AND (NOT (dbo.Group_Type.Group_Type_ID IN (5, 16, 18, 19, 42, 22, 27, 37, 38, 40, 41, 43, 99))) AND 
                      (NOT (dbo.Organization_Type.Organization_Type_Id IN (1, 7, 8, 23, 24, 25, 26, 27, 99))) AND (dbo.Consultant.Is_Agent = 0) AND 
                      (dbo.Consultant.Is_Fm = 0) AND (dbo.Department.Department_Id = 7)
ORDER BY dbo.Group_Type.Description
GO
