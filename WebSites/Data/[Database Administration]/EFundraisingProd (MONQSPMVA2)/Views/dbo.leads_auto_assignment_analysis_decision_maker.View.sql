USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[leads_auto_assignment_analysis_decision_maker]    Script Date: 02/14/2014 13:02:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[leads_auto_assignment_analysis_decision_maker]
AS
SELECT     Decision_Maker, SUM(Count_Leads) AS Count_Leads, SUM(Count_Sales) AS Count_Sales, SUM(Total_Amount) AS Total_Amount
FROM         dbo.leads_auto_assignment_analysis
GROUP BY Decision_Maker
GO
