USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_Kazoomster_Leads_Details]    Script Date: 02/14/2014 13:02:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_Kazoomster_Leads_Details]
AS
SELECT     TOP 100 PERCENT dbo.Lead.Lead_ID, dbo.Promotion.Description AS Promo, dbo.Consultant.Name AS FC, dbo.Client.Client_Sequence_Code, 
                      dbo.Client.Client_ID, dbo.Lead_Activity_Type.Description AS [Activity Type], dbo.Lead_Activity.Lead_Activity_Date, dbo.Lead_Activity.Completed_Date, 
                      dbo.Lead_Activity.Comments, dbo.Sale.Sales_ID, dbo.Sale.Total_Amount
FROM         dbo.Client RIGHT OUTER JOIN
                      dbo.Lead INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Lead_Activity ON dbo.Lead.Lead_ID = dbo.Lead_Activity.Lead_Id INNER JOIN
                      dbo.Lead_Activity_Type ON dbo.Lead_Activity.Lead_Activity_Type_Id = dbo.Lead_Activity_Type.Lead_Activity_Type_Id LEFT OUTER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID
WHERE     (dbo.Promotion.Partner_ID = 14)
ORDER BY dbo.Lead.Lead_ID, dbo.Lead_Activity.Lead_Activity_Date
GO
