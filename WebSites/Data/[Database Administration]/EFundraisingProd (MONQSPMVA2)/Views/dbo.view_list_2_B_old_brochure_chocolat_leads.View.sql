USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_2_B_old_brochure_chocolat_leads]    Script Date: 02/14/2014 13:02:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_2_B_old_brochure_chocolat_leads]
AS
SELECT     dbo.Department.Department_name, dbo.Lead.Lead_ID, dbo.view_list_state_code_coast.Coast, dbo.Consultant.Name, 
                      dbo.view_list_client_sales_products.Description
FROM         dbo.Lead INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN
                      dbo.view_list_state_code_coast ON dbo.Lead.State_Code = dbo.view_list_state_code_coast.State_Code INNER JOIN
                      dbo.Department ON dbo.Consultant.Department_ID = dbo.Department.Department_Id INNER JOIN
                      dbo.view_list_client_sales_products ON dbo.Lead.Lead_ID = dbo.view_list_client_sales_products.Lead_ID INNER JOIN
                      dbo.view_list_old_salesman ON dbo.Consultant.Consultant_ID = dbo.view_list_old_salesman.Consultant_ID INNER JOIN
                      dbo.view_list_not_harmony_transfer_salesman ON 
                      dbo.Consultant.Consultant_ID = dbo.view_list_not_harmony_transfer_salesman.Consultant_ID LEFT OUTER JOIN
                      dbo.view_list_lead_all_activity_not_completed ON dbo.Lead.Lead_ID = dbo.view_list_lead_all_activity_not_completed.Lead_Id
WHERE     (dbo.view_list_lead_all_activity_not_completed.Lead_Activity_Date IS NULL) AND (dbo.Lead.Lead_Entry_Date < CONVERT(DATETIME, 
                      '2002-07-01 23:59:59', 102)) AND (dbo.Consultant.Department_ID = 7) AND (dbo.Consultant.Is_Fm = 0) AND (dbo.Consultant.Is_Active = 1) AND 
                      (dbo.Consultant.Is_Agent = 0) OR
                      (dbo.view_list_lead_all_activity_not_completed.Lead_Activity_Date < CONVERT(DATETIME, '2002-07-01 23:59:59', 102)) AND 
                      (dbo.Lead.Lead_Entry_Date < CONVERT(DATETIME, '2002-07-01 23:59:59', 102)) AND (dbo.Consultant.Department_ID = 7) AND 
                      (dbo.Consultant.Is_Fm = 0) AND (dbo.Consultant.Is_Active = 1) AND (dbo.Consultant.Is_Agent = 0)
GO
