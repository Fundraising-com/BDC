USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[tbd_partner_payment]    Script Date: 02/14/2014 13:02:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[tbd_partner_payment]
AS
SELECT     TOP 100 PERCENT YEAR(dbo.payment.payment_entry_date) AS [Year], MONTH(dbo.payment.payment_entry_date) AS [Month], 
                      SUM(dbo.payment.payment_amount) AS SumPayment, dbo.partner.partner_name, dbo.product_class.description
FROM         dbo.lead INNER JOIN
                      dbo.client ON dbo.lead.lead_id = dbo.client.lead_id INNER JOIN
                      dbo.sale ON dbo.client.client_sequence_code = dbo.sale.client_sequence_code AND dbo.client.client_id = dbo.sale.client_id INNER JOIN
                      dbo.payment ON dbo.sale.sales_id = dbo.payment.sales_id INNER JOIN
                      dbo.promotion ON dbo.lead.promotion_id = dbo.promotion.promotion_id INNER JOIN
                      dbo.partner ON dbo.promotion.partner_id = dbo.partner.partner_id INNER JOIN
                      dbo.sales_item ON dbo.sale.sales_id = dbo.sales_item.sales_id INNER JOIN
                      dbo.scratch_book ON dbo.sales_item.scratch_book_id = dbo.scratch_book.scratch_book_id INNER JOIN
                      dbo.product_class ON dbo.scratch_book.product_class_id = dbo.product_class.product_class_id
WHERE     (dbo.sales_item.sales_item_no = 1)
GROUP BY MONTH(dbo.payment.payment_entry_date), YEAR(dbo.payment.payment_entry_date), dbo.partner.partner_name, dbo.sales_item.sales_item_no, 
                      dbo.product_class.description
HAVING      (YEAR(dbo.payment.payment_entry_date) = 2005) AND (MONTH(dbo.payment.payment_entry_date) = 9)
ORDER BY YEAR(dbo.payment.payment_entry_date), MONTH(dbo.payment.payment_entry_date)
GO
