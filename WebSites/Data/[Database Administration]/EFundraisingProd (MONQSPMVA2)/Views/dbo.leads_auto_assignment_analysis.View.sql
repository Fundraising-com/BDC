USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[leads_auto_assignment_analysis]    Script Date: 02/14/2014 13:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[leads_auto_assignment_analysis]
AS
SELECT     COUNT(dbo.leads_auto_assignment_detailled.Lead_ID) AS Count_Leads, SUM(dbo.leads_auto_assignment_detailled.Count_Sales) AS Count_Sales, 
                      SUM(dbo.leads_auto_assignment_detailled.Total_Amount) AS Total_Amount, dbo.leads_auto_assignment_detailled.consultant_id, 
                      dbo.Consultant.Name AS Consultant, dbo.leads_auto_assignment_detailled.Channel_Code, dbo.leads_auto_assignment_detailled.Promotion_ID, 
                      dbo.Promotion.Promotion_Type_Code, dbo.Promotion.Description AS Promotion, dbo.Partner.Partner_Name, 
                      dbo.leads_auto_assignment_detailled.Day_phone, dbo.leads_auto_assignment_detailled.Evening_phone, 
                      dbo.leads_auto_assignment_detailled.Group_Type_ID, dbo.Group_Type.Description AS Group_Type, 
                      dbo.leads_auto_assignment_detailled.State_Code, dbo.State.State_Name, dbo.State.Time_Zone_Difference, 
                      dbo.leads_auto_assignment_detailled.Participant_Count
FROM         dbo.leads_auto_assignment_detailled INNER JOIN
                      dbo.Consultant ON dbo.leads_auto_assignment_detailled.consultant_id = dbo.Consultant.Consultant_ID INNER JOIN
                      dbo.Promotion ON dbo.leads_auto_assignment_detailled.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Group_Type ON dbo.leads_auto_assignment_detailled.Group_Type_ID = dbo.Group_Type.Group_Type_ID INNER JOIN
                      dbo.Partner ON dbo.Promotion.Partner_ID = dbo.Partner.Partner_ID INNER JOIN
                      dbo.State ON dbo.leads_auto_assignment_detailled.State_Code = dbo.State.State_Code
GROUP BY dbo.leads_auto_assignment_detailled.consultant_id, dbo.Consultant.Name, dbo.leads_auto_assignment_detailled.Channel_Code, 
                      dbo.leads_auto_assignment_detailled.Promotion_ID, dbo.Promotion.Promotion_Type_Code, dbo.Partner.Partner_Name, 
                      dbo.leads_auto_assignment_detailled.Day_phone, dbo.leads_auto_assignment_detailled.Evening_phone, 
                      dbo.leads_auto_assignment_detailled.Group_Type_ID, dbo.Group_Type.Description, dbo.Promotion.Description, 
                      dbo.leads_auto_assignment_detailled.State_Code, dbo.State.State_Name, dbo.State.Time_Zone_Difference, 
                      dbo.leads_auto_assignment_detailled.Participant_Count
GO
