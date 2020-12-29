USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Efund_New_Letter_Leads]    Script Date: 02/14/2014 13:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Efund_New_Letter_Leads    Script Date: 2003-02-22 20:34:17 *****
***** Object:  View dbo.Efund_New_Letter_Leads    Script Date: 2/11/2003 12:27:44 PM ******/
CREATE VIEW [dbo].[Efund_New_Letter_Leads]
AS
SELECT     dbo.Lead.Lead_ID, dbo.Lead.Email AS GoodEmail, dbo.Lead.First_Name, dbo.Lead.Last_Name
FROM         dbo.Lead INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID
WHERE     (dbo.Lead.Email IS NOT NULL) AND (dbo.Lead.First_Name IS NOT NULL) AND (dbo.Lead.Last_Name IS NOT NULL) AND 
                      (dbo.Lead.Country_Code <> 'CA') AND (dbo.Lead.Promotion_ID <> 115) AND (dbo.Lead.Promotion_ID <> 126) AND (dbo.Lead.Promotion_ID <> 129) AND 
                      (dbo.Lead.Promotion_ID <> 131) AND (dbo.Lead.Promotion_ID <> 134) AND (dbo.Lead.Promotion_ID <> 147) AND (dbo.Lead.Promotion_ID <> 172) AND 
                      (dbo.Lead.Promotion_ID <> 217) AND (dbo.Lead.Promotion_ID <> 234) AND (dbo.Lead.Promotion_ID <> 235) AND (dbo.Lead.Promotion_ID <> 236) AND 
                      (dbo.Lead.Promotion_ID <> 538) AND (dbo.Promotion.Promotion_Type_Code <> 'AG') AND (dbo.Promotion.Promotion_Type_Code <> 'OUT') AND 
                      (dbo.Lead.OnEmailList = 1) AND (dbo.Promotion.Partner_ID = 0) AND (dbo.Lead.Valid_email = 1)
GO
