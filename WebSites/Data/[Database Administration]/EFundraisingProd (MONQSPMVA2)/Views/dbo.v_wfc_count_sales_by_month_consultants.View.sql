USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_wfc_count_sales_by_month_consultants]    Script Date: 02/14/2014 13:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_wfc_count_sales_by_month_consultants]
AS
SELECT     sales_year, sales_month, COUNT(NbSales) AS nb_sales
FROM         dbo.v_wfc_leads_sales
WHERE     (Is_Fm = 0)
GROUP BY sales_month, sales_year
GO
