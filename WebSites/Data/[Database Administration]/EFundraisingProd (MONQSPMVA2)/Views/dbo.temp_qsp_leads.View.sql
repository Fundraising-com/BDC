USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[temp_qsp_leads]    Script Date: 02/14/2014 13:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[temp_qsp_leads]
AS
SELECT     dbo.Lead.Lead_ID, dbo.Promotion.Promotion_Type_Code, dbo.Promotion.Description, dbo.Partner.Partner_Name, dbo.Promotion.Cookie_Content, 
                      dbo.Promotion.Keyword, dbo.Promotion.Script_Name, dbo.Promotion.Promotion_ID, dbo.Lead.Temp_Lead_ID
FROM         dbo.Lead INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Partner ON dbo.Promotion.Partner_ID = dbo.Partner.Partner_ID
WHERE     (dbo.Lead.Lead_Entry_Date BETWEEN CONVERT(DATETIME, '2003-01-01 00:00:00', 102) AND CONVERT(DATETIME, '2003-01-31 23:59:59', 102))
GROUP BY dbo.Promotion.Promotion_Type_Code, dbo.Partner.Partner_Name, dbo.Promotion.Description, dbo.Promotion.Cookie_Content, 
                      dbo.Promotion.Keyword, dbo.Promotion.Script_Name, dbo.Promotion.Promotion_ID, dbo.Lead.Temp_Lead_ID, dbo.Lead.Lead_ID
HAVING      (dbo.Promotion.Description LIKE '%qsp%')
GO
