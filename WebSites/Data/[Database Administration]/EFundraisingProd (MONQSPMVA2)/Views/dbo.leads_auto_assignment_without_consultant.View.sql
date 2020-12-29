USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[leads_auto_assignment_without_consultant]    Script Date: 02/14/2014 13:02:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[leads_auto_assignment_without_consultant]
AS
SELECT     TOP 100 PERCENT COUNT(dbo.leads_auto_assignment_detailled.Lead_ID) AS Count_Leads, SUM(dbo.leads_auto_assignment_detailled.Count_Sales) 
                      AS Count_Sales, SUM(dbo.leads_auto_assignment_detailled.Total_Amount) AS Total_Amount, dbo.Promotion_Type.Channel, 
                      dbo.Partner.Partner_Name, dbo.leads_auto_assignment_detailled.Day_phone, dbo.leads_auto_assignment_detailled.Evening_phone, 
                      dbo.Group_Type.Description AS Group_Type, dbo.fct_GetCoast(dbo.State.Time_Zone_Difference) AS Coast, 
                      dbo.fct_GetGreaterLimit(dbo.leads_auto_assignment_detailled.Participant_Count, 30, 200, 1) AS Part_count
FROM         dbo.leads_auto_assignment_detailled INNER JOIN
                      dbo.Promotion ON dbo.leads_auto_assignment_detailled.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Group_Type ON dbo.leads_auto_assignment_detailled.Group_Type_ID = dbo.Group_Type.Group_Type_ID INNER JOIN
                      dbo.Partner ON dbo.Promotion.Partner_ID = dbo.Partner.Partner_ID INNER JOIN
                      dbo.State ON dbo.leads_auto_assignment_detailled.State_Code = dbo.State.State_Code INNER JOIN
                      dbo.Promotion_Type ON dbo.Promotion.Promotion_Type_Code = dbo.Promotion_Type.Promotion_Type_Code
GROUP BY dbo.Partner.Partner_Name, dbo.leads_auto_assignment_detailled.Day_phone, dbo.leads_auto_assignment_detailled.Evening_phone, 
                      dbo.Group_Type.Description, dbo.fct_GetCoast(dbo.State.Time_Zone_Difference), 
                      dbo.fct_GetGreaterLimit(dbo.leads_auto_assignment_detailled.Participant_Count, 30, 200, 1), dbo.Promotion_Type.Channel
HAVING      (COUNT(dbo.leads_auto_assignment_detailled.Lead_ID) > 10)
ORDER BY COUNT(dbo.leads_auto_assignment_detailled.Lead_ID) DESC
GO
