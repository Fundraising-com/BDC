USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[leads_auto_assignment_detailled]    Script Date: 02/14/2014 13:02:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE        VIEW [dbo].[leads_auto_assignment_detailled]
AS

SELECT     dbo.Lead.Lead_ID, dbo.Lead.consultant_id, SUM(CASE WHEN Sale.Sales_ID IS NULL THEN 0 ELSE 1 END) AS Count_Sales, 
                      SUM(isnull(Sale.Total_Amount, 0)) AS Total_Amount, dbo.Lead.Channel_Code, dbo.Lead.Promotion_ID, (CASE WHEN dbo.Lead.Day_Phone IS NULL 
                      THEN 0 ELSE 1 END) AS Day_phone, (CASE WHEN dbo.Lead.Evening_Phone IS NULL THEN 0 ELSE 1 END) AS Evening_phone, 
                      dbo.Lead.Group_Type_ID, dbo.Lead.State_Code, dbo.Lead.Participant_Count, dbo.Lead.Decision_Maker, 
                      dbo.Lead.Organization_Type_Id
FROM         dbo.Lead LEFT OUTER JOIN
                      dbo.Client ON dbo.Lead.Lead_ID = dbo.Client.Lead_ID LEFT OUTER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID
WHERE     (dbo.Lead.Lead_Entry_Date BETWEEN CONVERT(DATETIME, '2002-07-01 00:00:00', 102) AND CONVERT(DATETIME, '2003-06-30 23:59:59', 102)) AND 
                      (dbo.Sale.Sales_Date BETWEEN CONVERT(DATETIME, '2002-07-01 00:00:00', 102) AND CONVERT(DATETIME, '2003-06-30 23:59:59', 102)) OR
                      (dbo.Lead.Lead_Entry_Date BETWEEN CONVERT(DATETIME, '2002-07-01 00:00:00', 102) AND CONVERT(DATETIME, '2003-06-30 23:59:59', 102)) AND 
                      (dbo.Sale.Sales_Date IS NULL)
GROUP BY dbo.Lead.Lead_ID, dbo.Lead.consultant_id, dbo.Lead.Channel_Code, dbo.Lead.Promotion_ID, dbo.Lead.Group_Type_ID, dbo.Lead.State_Code, (CASE WHEN dbo.Lead.Day_Phone IS NULL 
                      THEN 0 ELSE 1 END), (CASE WHEN dbo.Lead.Evening_Phone IS NULL THEN 0 ELSE 1 END), dbo.Lead.Participant_Count, dbo.Lead.Decision_Maker, 
                      dbo.Lead.Organization_Type_Id
GO
