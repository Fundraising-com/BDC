USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[order_form_view_completed_sales]    Script Date: 02/14/2014 13:02:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*WHERE lead.lead_id = 266801
ORDER BY Sale_To_Add.Sale_To_Add_ID
SELECT * FROM Lead where 1=2
SELECT * FROM lead_channel where 1=2*/
CREATE    VIEW [dbo].[order_form_view_completed_sales]
AS

SELECT     dbo.lead.salutation + ' ' + dbo.lead.first_name + ' ' + dbo.lead.last_name AS name, dbo.title.title_desc AS title, dbo.lead.lead_id, s.sales_id, si.sales_item_no,
                      dbo.Lead_Channel.Description AS Channel, dbo.lead.consultant_id, dbo.lead.organization, dbo.lead.salutation, dbo.lead.first_name, 
                      dbo.lead.last_name,
                      (case when ca.street_address is null then dbo.lead.street_address else ca.street_address end) as street_address,
                      (case when ca.city is null then dbo.lead.city else ca.city end) as city,
 		      (case when ca.state_code is null then dbo.lead.state_code else ca.state_code end) as state_code,
		      (case when ca.country_code is null then dbo.lead.country_code else ca.country_code end) as country_code,
                      (case when ca.zip_code is null then dbo.lead.zip_code else ca.zip_code end) as zip_code,
		      dbo.lead.evening_phone, 
                      dbo.lead.day_phone, dbo.lead.email, dbo.lead.fax, dbo.lead.participant_count, dbo.Country.Country_Name AS Country, 
                      dbo.consultant.name AS Consultant, dbo.consultant.name AS Consultant_Name, dbo.consultant.phone_extension, 
                      dbo.consultant.email_address AS Consultant_email, dbo.consultant.default_proposal_text, s.sales_date, 
                      dbo.payment_term.description AS Payment_Term, dbo.payment_method.description AS Payment_Method, 
                      s.shipping_fees - s.shipping_fees_discount AS Shipping_Fees, dbo.scratch_book.description AS Scratch_Book, 
                      dbo.scratch_book.description AS ScratchBook, si.quantity_sold, si.quantity_free, 
                      si.unit_price_sold, s.total_amount AS Total_Amount, si.sales_amount AS Amount, 
                       'Items : ' + dbo.scratch_book.description + '<br>' + 'Qty Sold : ' + CONVERT(VARCHAR(20), 
                      si.quantity_sold) 
                      + '<br>' + 'Unit Price : $' + CONVERT(varCHAR(20), CAST(si.unit_price_sold AS MONEY), 1) 
                      + '<br>' + 'Amount: $' + CONVERT(varCHAR(20), CAST(si.sales_amount AS MONEY), 1) AS Details, 
                      efrcommon.dbo.partner.partner_name,
                      'Article : ' + dbo.scratch_book.description + '<br>' + 'Quantité vendue : ' + CONVERT(VARCHAR(20), 
                      si.quantity_sold)  
                      + '<br>' + 'Prix unitaire : $' + CONVERT(varCHAR(20), CAST(si.unit_price_sold AS MONEY), 1) 
                      + '<br>' + 'Montant: $' + CONVERT(varCHAR(20), CAST(si.sales_amount AS MONEY), 1) AS Details_fr
FROM         dbo.lead INNER JOIN
                      dbo.consultant ON dbo.lead.consultant_id = dbo.consultant.consultant_id LEFT OUTER JOIN
                      client c on c.lead_id = dbo.lead.lead_id left join
                      client_address ca on c.client_id = ca.client_id and c.client_sequence_code = ca.client_sequence_code and ca.address_type COLLATE Latin1_General_CS_AS = 'ST' left join
                      dbo.sale s ON s.client_id = c.client_id and s.client_sequence_code = c.client_sequence_code LEFT OUTER JOIN
                      dbo.sales_item si ON s.sales_id = si.sales_id LEFT OUTER JOIN
                      dbo.payment_term ON dbo.payment_term.payment_term_id = s.payment_term_id LEFT OUTER JOIN
                      dbo.payment_method ON dbo.payment_method.payment_method_id = s.payment_method_id LEFT OUTER JOIN
                      dbo.scratch_book ON dbo.scratch_book.scratch_book_id = si.scratch_book_id INNER JOIN
                      dbo.Country ON dbo.lead.country_code = dbo.Country.Country_Code INNER JOIN
                      dbo.title ON dbo.lead.title_id = dbo.title.title_id INNER JOIN
                      dbo.Lead_Channel ON dbo.lead.channel_code = dbo.Lead_Channel.Channel_Code INNER JOIN
						/*dbo.promotion ON dbo.lead.promotion_id = dbo.promotion.promotion_id INNER JOIN
                      dbo.partner ON dbo.promotion.partner_id = dbo.partner.partner_id*/
					  efrcommon.dbo.promotion  ON dbo.lead.promotion_id = efrcommon.dbo.promotion.promotion_id INNER JOIN
                      efrcommon.dbo.partner_promotion ON efrcommon.dbo.partner_promotion.promotion_id = efrcommon.dbo.promotion.promotion_id INNER JOIN
                      efrcommon.dbo.partner  ON efrcommon.dbo.partner_promotion.partner_id = efrcommon.dbo.partner.partner_id

                     -- client c on c.lead_id = dbo.lead.lead_id-- left join
                     --client_address ca on c.client_id = ca.client_id and c.client_sequence_code = ca.client_sequence_code and ca.address_type = 'BT'
--where dbo.lead.lead_id = 673807
GO
