USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[leads_auto_assignment_analysis_participant_count]    Script Date: 02/14/2014 13:02:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[leads_auto_assignment_analysis_participant_count]
AS
SELECT     SUM(Count_Leads) AS Count_Leads, SUM(Count_Sales) AS Count_Sales, SUM(Total_Amount) AS Total_Amount
FROM         dbo.leads_auto_assignment_analysis
WHERE     (Participant_Count > 300)
GO
