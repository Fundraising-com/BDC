USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[order_form_view]    Script Date: 02/14/2014 13:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*WHERE lead.lead_id = 266801
ORDER BY Sale_To_Add.Sale_To_Add_ID
SELECT * FROM Lead where 1=2
SELECT * FROM lead_channel where 1=2*/
CREATE  VIEW [dbo].[order_form_view]
AS
SELECT     dbo.lead.salutation + ' ' + dbo.lead.first_name + ' ' + dbo.lead.last_name AS name, dbo.title.title_desc AS title, dbo.lead.lead_id, 
                      dbo.Lead_Channel.Description AS Channel, dbo.lead.consultant_id, dbo.lead.organization, dbo.lead.salutation, dbo.lead.first_name, 
                      dbo.lead.last_name, dbo.lead.street_address, dbo.lead.city, dbo.lead.state_code, dbo.lead.country_code, dbo.lead.zip_code, dbo.lead.evening_phone, 
                      dbo.lead.day_phone, dbo.lead.email, dbo.lead.fax, dbo.lead.participant_count, dbo.Country.Country_Name AS Country, 
                      dbo.consultant.name AS Consultant, dbo.consultant.name AS Consultant_Name, dbo.consultant.phone_extension, 
                      dbo.consultant.email_address AS Consultant_email, dbo.consultant.default_proposal_text, dbo.sale_to_add.sales_date, 
                      dbo.payment_term.description AS Payment_Term, dbo.payment_method.description AS Payment_Method, 
                      dbo.sale_to_add.shipping_fees - dbo.sale_to_add.shipping_fees_discount AS Shipping_Fees, dbo.scratch_book.description AS Scratch_Book, 
                      dbo.scratch_book.description AS ScratchBook, dbo.sales_item_to_add.quantity_sold, dbo.sales_item_to_add.quantity_free, 
                      dbo.sales_item_to_add.unit_price_sold, dbo.sale_to_add.total_amount AS Total_Amount, dbo.sales_item_to_add.sales_amount AS Amount, 
                      dbo.sale_to_add.sale_to_add_id, 'Items : ' + dbo.scratch_book.description + '<br>' + 'Qty Sold : ' + CONVERT(VARCHAR(20), 
                      dbo.sales_item_to_add.quantity_sold) + '<br>' + 'Unit Price : $' + CONVERT(varCHAR(20), CAST(dbo.sales_item_to_add.unit_price_sold AS MONEY), 1) 
                      + '<br>' + 'Amount: $' + CONVERT(varCHAR(20), CAST(dbo.sales_item_to_add.sales_amount AS MONEY), 1) AS Details, 
                      efrcommon.dbo.partner.partner_name
FROM         dbo.lead INNER JOIN
                      dbo.consultant ON dbo.lead.consultant_id = dbo.consultant.consultant_id LEFT OUTER JOIN
                      dbo.sale_to_add ON dbo.sale_to_add.lead_id = dbo.lead.lead_id LEFT OUTER JOIN
                      dbo.sales_item_to_add ON dbo.sale_to_add.sale_to_add_id = dbo.sales_item_to_add.sale_to_add_id LEFT OUTER JOIN
                      dbo.payment_term ON dbo.payment_term.payment_term_id = dbo.sale_to_add.payment_term_id LEFT OUTER JOIN
                      dbo.payment_method ON dbo.payment_method.payment_method_id = dbo.sale_to_add.payment_method_id LEFT OUTER JOIN
                      dbo.scratch_book ON dbo.scratch_book.scratch_book_id = dbo.sales_item_to_add.scratch_book_id INNER JOIN
                      dbo.Country ON dbo.lead.country_code = dbo.Country.Country_Code INNER JOIN
                      dbo.title ON dbo.lead.title_id = dbo.title.title_id INNER JOIN
                      dbo.Lead_Channel ON dbo.lead.channel_code = dbo.Lead_Channel.Channel_Code INNER JOIN
                      /*dbo.promotion ON dbo.lead.promotion_id = dbo.promotion.promotion_id INNER JOIN
                      dbo.partner ON dbo.promotion.partner_id = dbo.partner.partner_id*/
					  efrcommon.dbo.promotion  ON dbo.lead.promotion_id = efrcommon.dbo.promotion.promotion_id INNER JOIN
                      efrcommon.dbo.partner_promotion ON efrcommon.dbo.partner_promotion.promotion_id = efrcommon.dbo.promotion.promotion_id INNER JOIN
                      efrcommon.dbo.partner  ON efrcommon.dbo.partner_promotion.partner_id = efrcommon.dbo.partner.partner_id
GO
