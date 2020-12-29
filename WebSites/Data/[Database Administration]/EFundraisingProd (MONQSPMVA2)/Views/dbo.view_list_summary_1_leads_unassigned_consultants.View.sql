USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_summary_1_leads_unassigned_consultants]    Script Date: 02/14/2014 13:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_summary_1_leads_unassigned_consultants]
AS
SELECT     TOP 100 PERCENT COUNT(Lead_ID) AS leads_count, Coast, Name, Department_name
FROM         dbo.view_list_1_leads_unassigned_consultants
GROUP BY Department_name, Coast, Name
ORDER BY COUNT(Lead_ID)
GO
