USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_my_group_page_launch_account]    Script Date: 02/14/2014 13:02:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_my_group_page_launch_account]
AS
SELECT     dbo.Consultant.Name, dbo.Lead.Lead_ID, dbo.Lead.First_Name, dbo.Lead.Last_Name, dbo.Lead.Organization, dbo.State.State_Name, 
                      dbo.Lead.Zip_Code, dbo.Lead.Account_Number
FROM         dbo.Lead INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN
                      dbo.State ON dbo.Lead.State_Code = dbo.State.State_Code
WHERE     (dbo.Lead.Account_Number IS NOT NULL)
GO
