USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[leads_auto_assignment_analysis_phone_number]    Script Date: 02/14/2014 13:02:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[leads_auto_assignment_analysis_phone_number]
AS
SELECT     Day_phone, Evening_phone, SUM(Count_Leads) AS Count_Leads, SUM(Count_Sales) AS Count_Sales, SUM(Total_Amount) AS Total_Amount
FROM         dbo.leads_auto_assignment_analysis
GROUP BY Day_phone, Evening_phone
GO
