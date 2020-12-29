USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_lead_activity_not_completed_resheduled_first_call]    Script Date: 02/14/2014 13:02:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_lead_activity_not_completed_resheduled_first_call]
AS
SELECT DISTINCT Lead_Id, MAX(Lead_Activity_Date) AS Lead_Activity_Date, Lead_Activity_Type_Id
FROM         dbo.Lead_Activity
GROUP BY Lead_Id, Completed_Date, Lead_Activity_Type_Id
HAVING      (Completed_Date IS NOT NULL)
GO
