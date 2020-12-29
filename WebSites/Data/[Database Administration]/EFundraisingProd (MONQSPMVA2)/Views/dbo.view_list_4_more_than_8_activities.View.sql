USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_4_more_than_8_activities]    Script Date: 02/14/2014 13:02:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_4_more_than_8_activities]
AS
SELECT     dbo.Department.Department_name, dbo.Lead.Lead_ID, dbo.view_list_state_code_coast.Coast, dbo.Consultant.Name
FROM         dbo.Lead INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN
                      dbo.view_list_state_code_coast ON dbo.Lead.State_Code = dbo.view_list_state_code_coast.State_Code INNER JOIN
                      dbo.Department ON dbo.Consultant.Department_ID = dbo.Department.Department_Id INNER JOIN
                      dbo.view_list_more_than_8_activity ON dbo.Lead.Lead_ID = dbo.view_list_more_than_8_activity.Lead_Id INNER JOIN
                      dbo.view_list_not_harmony_transfer_salesman ON 
                      dbo.Consultant.Consultant_ID = dbo.view_list_not_harmony_transfer_salesman.Consultant_ID
WHERE     (dbo.Consultant.Is_Active = 1) AND (dbo.Lead.Lead_Entry_Date < CONVERT(DATETIME, '2002-11-30 23:59:59', 102)) AND (dbo.Consultant.Is_Agent = 0) 
                      AND (dbo.Consultant.Is_Fm = 0) AND (dbo.Consultant.Department_ID = 7)
GO
