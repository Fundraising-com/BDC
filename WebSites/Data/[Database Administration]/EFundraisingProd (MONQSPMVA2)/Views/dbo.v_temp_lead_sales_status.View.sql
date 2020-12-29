USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_temp_lead_sales_status]    Script Date: 02/14/2014 13:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_temp_lead_sales_status]
AS
SELECT     TOP 100 PERCENT dbo.Lead.Lead_ID, dbo.Client.Client_Sequence_Code, dbo.Client.Client_ID, dbo.Sale.Sales_ID, dbo.Consultant.Name, 
                      dbo.Production_Status.Description, dbo.Sale.Actual_Ship_Date, dbo.Sale.Box_Return_Date, dbo.Sale.Reship_Date, 
                      dbo.Sales_Change_Log.Table_Name, dbo.Sales_Change_Log.Column_Name, dbo.Sales_Change_Log.User_Name, 
                      dbo.Sales_Change_Log.Comment
FROM         dbo.Lead INNER JOIN
                      dbo.Client ON dbo.Lead.Lead_ID = dbo.Client.Lead_ID INNER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
                      dbo.Production_Status ON dbo.Sale.Production_Status_ID = dbo.Production_Status.Production_Status_ID INNER JOIN
                      dbo.Consultant ON dbo.Sale.Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN
                      dbo.Sales_Change_Log ON dbo.Sale.Sales_ID = dbo.Sales_Change_Log.Sales_ID
WHERE     (dbo.Lead.Lead_ID IN (244428, 219055, 217879, 215444, 225915, 197689, 249348, 249165, 250853, 251765, 251013, 253148, 254128, 249940, 
                      240098)) AND (dbo.Sales_Change_Log.Column_Name LIKE 'Production_status_id')
ORDER BY dbo.Lead.Lead_ID, dbo.Sale.Sales_ID
GO
