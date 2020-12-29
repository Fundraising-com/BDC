USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[VIEW2]    Script Date: 02/14/2014 13:02:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VIEW2]
AS
SELECT     TOP 100 PERCENT Count_Leads, Count_Sales, Total_Amount, Channel, Partner_Name, Day_phone, Evening_phone, Group_Type, Coast, 
                      Part_count
FROM         dbo.leads_auto_assignment_without_consultant
WHERE     (Partner_Name = 'Active Team Fundraising') AND (Channel = 1)
ORDER BY Part_count
GO
