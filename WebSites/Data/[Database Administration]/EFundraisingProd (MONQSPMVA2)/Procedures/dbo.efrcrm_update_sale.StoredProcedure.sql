USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sale]    Script Date: 02/14/2014 13:08:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sale
CREATE   PROCEDURE [dbo].[efrcrm_update_sale] @Sales_id int, @Consultant_id int, @Carrier_id tinyint, @Shipping_option_id tinyint, @Payment_term_id tinyint, @Client_sequence_code char(2), @Client_id int, @Sales_status_id int, @Payment_method_id tinyint, @Po_status_id tinyint, @Production_status_id int, @Sponsor_consultant_id int, @Ar_consultant_id int, @Ar_status_id int, @Lead_id int, @Billing_company_id int, @Upfront_payment_method_id tinyint, @Confirmer_id int, @Collection_status_id int, @Confirmation_method_id int, @Credit_approval_method_id int, @Po_number varchar(50), @Credit_card_no varchar(20), @Expiry_date varchar(7), @Sales_date datetime, @Shipping_fees decimal(15,4), @Shipping_fees_discount decimal(15,4), @Payment_due_date datetime, @Confirmed_date datetime, @Scheduled_delivery_date datetime, @Scheduled_ship_date datetime, @Actual_ship_date datetime, @Waybill_no varchar(20), @Comment varchar(200), @Coupon_sheet_assigned bit, @Total_amount decimal(15,4), @Invoice_date datetime, @Cancellation_date datetime, @Is_ordered bit, @Po_received_on smalldatetime, @Is_delivered bit, @Local_sponsor_found bit, @Box_return_date smalldatetime, @Reship_date smalldatetime, @Upfront_payment_required decimal(15,4), @Upfront_payment_due_date smalldatetime, @Sponsor_required bit, @Actual_delivery_date smalldatetime, @Accounting_comments text, @Ssn_number varchar(9), @Ssn_address varchar(50), @Ssn_city varchar(50), @Ssn_state_code varchar(10), @Ssn_country_code varchar(10), @Ssn_zip_code varchar(10), @Is_validated bit, @Promised_due_date datetime, @General_flag bit, 
@Fuelsurcharge tinyint, @IsPackedByStudent bit  = null, @carrier_tracking_id int = null, @ext_order_id int = null , @qsp_order_id int = null, @po_consultant_commission int = NULL AS
begin



	DECLARE @passphrase NVARCHAR(200)
	SET @passphrase = 'j7@3bv!009';

	-- declare and set varible @encrypted_str to store
	-- ciphertext
	DECLARE @cc VARBINARY(MAX)
	SET @cc = EncryptByPassPhrase(@passphrase, cast(@credit_card_no as NVARCHAR(20)));


	update Sale set Consultant_id=@Consultant_id, Carrier_id=@Carrier_id, Shipping_option_id=@Shipping_option_id, Payment_term_id=@Payment_term_id, Client_sequence_code=@Client_sequence_code, Client_id=@Client_id, Sales_status_id=@Sales_status_id, Payment_method_id=@Payment_method_id, Po_status_id=@Po_status_id, Production_status_id=@Production_status_id, Sponsor_consultant_id=@Sponsor_consultant_id, Ar_consultant_id=@Ar_consultant_id, Ar_status_id=@Ar_status_id, Lead_id=@Lead_id, Billing_company_id=@Billing_company_id, Upfront_payment_method_id=@Upfront_payment_method_id, Confirmer_id=@Confirmer_id, Collection_status_id=@Collection_status_id, Confirmation_method_id=@Confirmation_method_id, Credit_approval_method_id=@Credit_approval_method_id, Po_number=@Po_number, Credit_card_no=@cc, Expiry_date=@Expiry_date, Sales_date=@Sales_date, Shipping_fees=@Shipping_fees, Shipping_fees_discount=@Shipping_fees_discount, Payment_due_date=@Payment_due_date, Confirmed_date=@Confirmed_date, Scheduled_delivery_date=@Scheduled_delivery_date, Scheduled_ship_date=@Scheduled_ship_date, Actual_ship_date=@Actual_ship_date, Waybill_no=@Waybill_no, Comment=@Comment, Coupon_sheet_assigned=@Coupon_sheet_assigned, Total_amount=@Total_amount, Invoice_date=@Invoice_date, Cancellation_date=@Cancellation_date, Is_ordered=@Is_ordered, Po_received_on=@Po_received_on, Is_delivered=@Is_delivered, Local_sponsor_found=@Local_sponsor_found, Box_return_date=@Box_return_date, Reship_date=@Reship_date, Upfront_payment_required=@Upfront_payment_required, Upfront_payment_due_date=@Upfront_payment_due_date, Sponsor_required=@Sponsor_required, Actual_delivery_date=@Actual_delivery_date, Accounting_comments=@Accounting_comments, Ssn_number=@Ssn_number, Ssn_address=@Ssn_address, Ssn_city=@Ssn_city, Ssn_state_code=@Ssn_state_code, Ssn_country_code=@Ssn_country_code, Ssn_zip_code=@Ssn_zip_code, Is_validated=@Is_validated, Promised_due_date=@Promised_due_date, General_flag=@General_flag, 
	Fuelsurcharge=@Fuelsurcharge, is_packed_by_participant=@IsPackedByStudent, carrier_tracking_id=@carrier_tracking_id, ext_order_id = @ext_order_id, qsp_order_id = @qsp_order_id, po_consultant_commission=@po_consultant_commission
	where Sales_id=@Sales_id

end
GO
