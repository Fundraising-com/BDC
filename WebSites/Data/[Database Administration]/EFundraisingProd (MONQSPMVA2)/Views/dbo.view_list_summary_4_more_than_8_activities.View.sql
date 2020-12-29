USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_summary_4_more_than_8_activities]    Script Date: 02/14/2014 13:02:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_summary_4_more_than_8_activities]
AS
SELECT     Coast, Name, COUNT(Lead_ID) AS count_lead
FROM         dbo.view_list_4_more_than_8_activities
GROUP BY Name, Coast
GO
