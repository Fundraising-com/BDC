USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_summary_3_school_sports_groups_leads]    Script Date: 02/14/2014 13:02:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_summary_3_school_sports_groups_leads]
AS
SELECT     TOP 100 PERCENT COUNT(Lead_ID) AS count_leads, Name, Coast
FROM         dbo.view_list_3_school_sports_groups_leads
GROUP BY Coast, Name
ORDER BY Name, Coast
GO
