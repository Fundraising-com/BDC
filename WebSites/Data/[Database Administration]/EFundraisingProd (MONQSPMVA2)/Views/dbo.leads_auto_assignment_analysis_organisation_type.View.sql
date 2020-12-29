USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[leads_auto_assignment_analysis_organisation_type]    Script Date: 02/14/2014 13:02:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[leads_auto_assignment_analysis_organisation_type]
AS
SELECT     dbo.Organization_Type.Organization_Type_Desc, SUM(dbo.leads_auto_assignment_analysis.Count_Leads) AS Count_Leads, 
                      SUM(dbo.leads_auto_assignment_analysis.Count_Sales) AS Count_Sales, SUM(dbo.leads_auto_assignment_analysis.Total_Amount) 
                      AS Total_Amount
FROM         dbo.leads_auto_assignment_analysis LEFT OUTER JOIN
                      dbo.Organization_Type ON dbo.leads_auto_assignment_analysis.Organization_Type_Id = dbo.Organization_Type.Organization_Type_Id
GROUP BY dbo.Organization_Type.Organization_Type_Desc
GO
