USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_xavier_consultants_analysis]    Script Date: 02/14/2014 13:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_xavier_consultants_analysis]
AS
SELECT TOP 100 PERCENT COUNT(dbo.leads_auto_assignment_detailled.Lead_ID) AS Count_Leads, 
               SUM(dbo.leads_auto_assignment_detailled.Count_Sales) AS Count_Sales, SUM(dbo.leads_auto_assignment_detailled.Total_Amount) 
               AS Total_Amount, dbo.Consultant.Name AS Consultant, dbo.Promotion_Type.Channel, dbo.Promotion.Promotion_Type_Code, 
               dbo.Partner.Partner_Name
FROM  dbo.leads_auto_assignment_detailled INNER JOIN
               dbo.Consultant ON dbo.leads_auto_assignment_detailled.consultant_id = dbo.Consultant.Consultant_ID INNER JOIN
               dbo.Promotion ON dbo.leads_auto_assignment_detailled.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
               dbo.Group_Type ON dbo.leads_auto_assignment_detailled.Group_Type_ID = dbo.Group_Type.Group_Type_ID INNER JOIN
               dbo.Partner ON dbo.Promotion.Partner_ID = dbo.Partner.Partner_ID INNER JOIN
               dbo.State ON dbo.leads_auto_assignment_detailled.State_Code = dbo.State.State_Code INNER JOIN
               dbo.Promotion_Type ON dbo.Promotion.Promotion_Type_Code = dbo.Promotion_Type.Promotion_Type_Code
WHERE (dbo.Consultant.Is_Active = 1) AND (dbo.Consultant.Department_ID = 7)
GROUP BY dbo.Consultant.Name, dbo.Partner.Partner_Name, dbo.Promotion_Type.Channel, dbo.Promotion.Promotion_Type_Code
ORDER BY SUM(dbo.leads_auto_assignment_detailled.Count_Sales) DESC
GO
