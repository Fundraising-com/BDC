USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_payment_receivable_by_sale_id]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Address_zone
CREATE PROCEDURE [dbo].[efrcrm_get_payment_receivable_by_sale_id] --72536
                   @sale_id int
AS
begin

SELECT     dbo.Sale.Total_Amount - COALESCE (dbo.Total_Payment.Payment_Amount, 0) 
                      - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0) AS Total_Receivable
           FROM         dbo.Sale LEFT OUTER JOIN
                      dbo.Total_Adjustment ON dbo.Sale.Sales_ID = dbo.Total_Adjustment.Sales_ID LEFT OUTER JOIN
                      dbo.Total_Payment ON dbo.Sale.Sales_ID = dbo.Total_Payment.Sales_ID
           WHERE      (dbo.Sale.Total_Amount - COALESCE (dbo.Total_Payment.Payment_Amount, 0) - COALESCE (dbo.Total_Adjustment.Adjustment_Amount, 0) > 0.01) 
                      and dbo.sale.box_return_date IS NULL 
                      and dbo.sale.sales_id = @sale_id





end
GO
