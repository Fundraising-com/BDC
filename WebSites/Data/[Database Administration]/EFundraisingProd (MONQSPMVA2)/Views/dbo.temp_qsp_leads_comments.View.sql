USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[temp_qsp_leads_comments]    Script Date: 02/14/2014 13:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[temp_qsp_leads_comments]
AS
SELECT     dbo.temp_qsp_leads.Lead_ID, dbo.temp_qsp_leads.Promotion_Type_Code, dbo.temp_qsp_leads.Description, dbo.temp_qsp_leads.Partner_Name, 
                      dbo.temp_qsp_leads.Temp_Lead_ID, dbo.Comments.Entry_Date, dbo.Comments.Comments, dbo.Consultant.Name
FROM         dbo.Consultant INNER JOIN
                      dbo.Comments ON dbo.Consultant.Consultant_ID = dbo.Comments.Consultant_ID INNER JOIN
                      dbo.temp_qsp_leads ON dbo.Comments.Lead_ID = dbo.temp_qsp_leads.Lead_ID
GO
