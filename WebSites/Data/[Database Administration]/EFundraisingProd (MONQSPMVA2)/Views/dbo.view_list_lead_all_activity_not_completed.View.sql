USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_lead_all_activity_not_completed]    Script Date: 02/14/2014 13:02:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_lead_all_activity_not_completed]
AS
SELECT     Lead_Id, MAX(Lead_Activity_Date) AS Lead_Activity_Date
FROM         dbo.view_list_lead_all_activity_not_completed_with_double
GROUP BY Lead_Id
GO
