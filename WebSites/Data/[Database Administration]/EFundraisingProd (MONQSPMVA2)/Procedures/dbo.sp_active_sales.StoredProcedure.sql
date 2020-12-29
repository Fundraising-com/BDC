USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_active_sales]    Script Date: 02/14/2014 13:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_active_sales](@date_from datetime  = NULL, @date_to datetime = NULL) AS

	SELECT     dbo.Partner.Partner_ID, dbo.Promotion.Description, dbo.Sale.Sales_ID, dbo.Lead.Promotion_ID, 
			dbo.Sale.Box_Return_Date, dbo.Sale.Reship_Date, 
                      dbo.Sale.Actual_Ship_Date
	FROM         dbo.Sale INNER JOIN
                      dbo.Lead ON dbo.Sale.Lead_Id = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Client INNER JOIN
                      dbo.Promotion ON dbo.Client.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Partner ON dbo.Promotion.Partner_ID = dbo.Partner.Partner_ID ON dbo.Lead.Lead_ID = dbo.Client.Lead_ID
	WHERE     (dbo.Partner.Partner_ID = 8) AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Reship_Date IS NULL) AND 
                      (dbo.Sale.Box_Return_Date IS NULL) OR
                      (dbo.Partner.Partner_ID = 8) AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Reship_Date IS NOT NULL) AND 
                      (dbo.Sale.Box_Return_Date IS NOT NULL) AND 
			dbo.Sale.Sales_Date BETWEEN isnull(@date_from, dbo.Sale.Sales_Date) AND isnull(@date_to, dbo.Sale.Sales_Date)	
	GROUP BY dbo.Partner.Partner_ID, dbo.Promotion.Description, dbo.Sale.Sales_ID, dbo.Lead.Promotion_ID, dbo.Sale.Box_Return_Date, dbo.Sale.Reship_Date, 
                      dbo.Sale.Actual_Ship_Date
GO
