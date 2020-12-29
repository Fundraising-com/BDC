USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_active_leads_by_promotions]    Script Date: 02/14/2014 13:08:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_active_leads_by_promotions](@date_from datetime  = NULL, @date_to datetime = NULL) AS
	SELECT     COUNT(dbo.Lead.Lead_ID) AS CountOfLead_ID, dbo.Promotion.Promotion_ID
	FROM         dbo.Lead INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Partner ON dbo.Promotion.Partner_ID = dbo.Partner.Partner_ID
	WHERE     (dbo.Partner.Partner_ID = 8) AND 
		dbo.Lead.Lead_Entry_Date BETWEEN isnull(@date_from, dbo.Lead.Lead_Entry_Date) AND isnull(@date_to, dbo.Lead.Lead_Entry_Date)	
	GROUP BY dbo.Promotion.Promotion_ID
GO
