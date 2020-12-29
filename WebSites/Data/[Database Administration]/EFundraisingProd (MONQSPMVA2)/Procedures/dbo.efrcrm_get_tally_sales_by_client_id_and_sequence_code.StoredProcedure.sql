USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tally_sales_by_client_id_and_sequence_code]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_tally_sales_by_client_id_and_sequence_code] @Client_id int, @Client_sequence_code 
varchar(2) AS
begin

SELECT 
p.[description] as package_description,
s.Sales_id,s.Consultant_id, s.Carrier_id, s.Shipping_option_id, s.Payment_term_id, s.Client_sequence_code, 
s.Client_id, s.Sales_status_id, s.Payment_method_id, s.Po_status_id, s.Production_status_id,
s.Sponsor_consultant_id, s.Ar_consultant_id, s.Ar_status_id, s.Lead_id, s.Billing_company_id, s.Upfront_payment_method_id, s.Confirmer_id, s.Collection_status_id, s.Confirmation_method_id, 
s.Credit_approval_method_id, s.Po_number, s.Credit_card_no, s.Expiry_date, s.Sales_date, s.Shipping_fees, 
s.Shipping_fees_discount, s.Payment_due_date, s.Confirmed_date, s.Scheduled_delivery_date, s.Scheduled_ship_date, 
s.Actual_ship_date, Waybill_no, s.Comment, Coupon_sheet_assigned, s.Total_amount, s.Invoice_date, s.Cancellation_date, s.Is_ordered, s.Po_received_on, s.Is_delivered, s.Local_sponsor_found, s.Box_return_date, 
s.Reship_date,s.Upfront_payment_required, s.Upfront_payment_due_date, s.Sponsor_required, s.Actual_delivery_date, 
s.Accounting_comments, s.Ssn_number, s.Ssn_address, s.Ssn_city, s.Ssn_state_code, s.Ssn_country_code, 
s.Ssn_zip_code, s.Is_validated,s.Promised_due_date, s.General_flag, s.Fuelsurcharge,
si.participant_id
FROM sale s
inner join sales_item si on s.sales_id = si.sales_id
inner join scratch_book sb on si.scratch_book_id = sb.scratch_book_id
inner join package p on sb.package_id = p.package_id 
WHERE 
sb.product_class_id=23
AND 
client_id=@Client_id 
AND 
client_sequence_code = @Client_sequence_code
end
GO
