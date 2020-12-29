USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_count_lead_activity_by_type]    Script Date: 02/14/2014 13:02:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_count_lead_activity_by_type]
AS
SELECT     Lead_Id, Lead_Activity_Type_Id, COUNT(Lead_Activity_Id) AS count_activity
FROM         dbo.Lead_Activity
GROUP BY Lead_Id, Lead_Activity_Type_Id
GO
