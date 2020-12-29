USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sale]    Script Date: 02/14/2014 13:07:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sale
CREATE   PROCEDURE [dbo].[efrcrm_insert_sale] @Sales_id int OUTPUT, @Consultant_id int, @Carrier_id tinyint, @Shipping_option_id tinyint, @Payment_term_id tinyint, 
@Client_sequence_code char(2), @Client_id int, @Sales_status_id int, @Payment_method_id tinyint, @Po_status_id tinyint, @Production_status_id int, 
@Sponsor_consultant_id int, @Ar_consultant_id int, @Ar_status_id int, @Lead_id int, @Billing_company_id int, @Upfront_payment_method_id tinyint, 
@Confirmer_id int, @Collection_status_id int, @Confirmation_method_id int, @Credit_approval_method_id int, @Po_number varchar(50), @Credit_card_no varchar(200), 
@Expiry_date varchar(7), @Sales_date datetime, @Shipping_fees decimal(15,4), @Shipping_fees_discount decimal(15,4), @Payment_due_date datetime, 
@Confirmed_date datetime, @Scheduled_delivery_date datetime, @Scheduled_ship_date datetime, @Actual_ship_date datetime, @Waybill_no varchar(20), 
@Comment varchar(200), @Coupon_sheet_assigned bit, @Total_amount decimal(15,4), @Invoice_date datetime, @Cancellation_date datetime, @Is_ordered bit, 
@Po_received_on smalldatetime, @Is_delivered bit, @Local_sponsor_found bit, @Box_return_date smalldatetime, @Reship_date smalldatetime, 
@Upfront_payment_required decimal(15,4), @Upfront_payment_due_date smalldatetime, @Sponsor_required bit, @Actual_delivery_date smalldatetime, 
@Accounting_comments text, @Ssn_number varchar(9), @Ssn_address varchar(50), @Ssn_city varchar(50), @Ssn_state_code varchar(10), 
@Ssn_country_code varchar(10), @Ssn_zip_code varchar(10), @Is_validated bit, @Promised_due_date datetime, @General_flag bit, @Fuelsurcharge tinyint, 
@IsPackedByStudent as bit = null, @carrier_tracking_id as int = null, @ext_order_id as int = null, @qsp_order_id as int = null,
@cvv2 CHAR(3) = NULL, @po_consultant_commission int = NULL
AS
begin

declare @id int
exec @id = sp_NewID  'Sales_ID', 'All'
set @Sales_id = @id


/*CREATE SYMMETRIC KEY SecureSymmetricKey
        WITH ALGORITHM = DESX
        ENCRYPTION BY PASSWORD = N'StrongPassword';
--SELECT * FROM sys.symmetric_keys;
*/

DECLARE  @str NVARCHAR(200)
SET @str = @Credit_card_no;

DECLARE @passphrase NVARCHAR(100)
	SET @passphrase = N'j7@3bv!009';

DECLARE @encrypted_str VARBINARY(MAX)
SET @encrypted_str = EncryptByPassPhrase(@passphrase, @str);


insert into Sale(Sales_id, 
	Consultant_id, 
	Carrier_id, 
	Shipping_option_id, 
	Payment_term_id, 
	Client_sequence_code, 
	Client_id, 
	Sales_status_id, 
	Payment_method_id, 
	Po_status_id, 
	Production_status_id, 
	Sponsor_consultant_id, 
	Ar_consultant_id, 
	Ar_status_id, 
	Lead_id, 
	Billing_company_id, 
	Upfront_payment_method_id, 
	Confirmer_id, 
	Collection_status_id, 
	Confirmation_method_id, 
	Credit_approval_method_id, 
	Po_number, 
	Credit_card_no, 
	Expiry_date, 
	Sales_date, 
	Shipping_fees, 
	Shipping_fees_discount, 
	Payment_due_date, 
	Confirmed_date, 
	Scheduled_delivery_date, 
	Scheduled_ship_date, 
	Actual_ship_date, 
	Waybill_no, 
	Comment, 
	Coupon_sheet_assigned, 
	Total_amount, 
	Invoice_date, 
	Cancellation_date, 
	Is_ordered, 
	Po_received_on, 
	Is_delivered, 
	Local_sponsor_found, 
	Box_return_date, 
	Reship_date, 
	Upfront_payment_required, 
	Upfront_payment_due_date, 
	Sponsor_required, 
	Actual_delivery_date, 
	Accounting_comments, 
	Ssn_number, 
	Ssn_address, 
	Ssn_city, 
	Ssn_state_code, 
	Ssn_country_code, 
	Ssn_zip_code, 
	Is_validated, 
	Promised_due_date, 
	General_flag, 
	Fuelsurcharge,
	is_packed_by_participant,
	carrier_tracking_id,
    ext_order_id,
    qsp_order_id,
	cvv2,
	po_consultant_commission
) 
values(@Sales_ID, 
	@Consultant_id, 
	@Carrier_id, 
	@Shipping_option_id, 
	@Payment_term_id, 
	@Client_sequence_code, 
	@Client_id, 
	@Sales_status_id, 
	@Payment_method_id, 
	@Po_status_id, 
	@Production_status_id, 
	@Sponsor_consultant_id, 
	@Ar_consultant_id, 
	@Ar_status_id, 
	@Lead_id, 
	@Billing_company_id, 
	@Upfront_payment_method_id, 
	@Confirmer_id, 
	@Collection_status_id, 
	@Confirmation_method_id, 
	@Credit_approval_method_id, 
	@Po_number, 
	@encrypted_str, 
	@Expiry_date, 
	@Sales_date, 	
	@Shipping_fees, 
	@Shipping_fees_discount, 
	@Payment_due_date, 
	@Confirmed_date, 
	@Scheduled_delivery_date, 
	@Scheduled_ship_date, 
	@Actual_ship_date, 
	@Waybill_no, 
	@Comment, 
	@Coupon_sheet_assigned, 
	@Total_amount, 
	@Invoice_date, 
	@Cancellation_date, 
	@Is_ordered, 
	@Po_received_on, 
	@Is_delivered, 
	@Local_sponsor_found, 
	@Box_return_date, 
	@Reship_date, 
	@Upfront_payment_required, 
	@Upfront_payment_due_date, 
	@Sponsor_required, 
	@Actual_delivery_date, 
	@Accounting_comments, 
	@Ssn_number, 	@Ssn_address, 
	@Ssn_city, 
	@Ssn_state_code, 
	@Ssn_country_code, 
	@Ssn_zip_code, 
	@Is_validated, 
	@Promised_due_date, 
	@General_flag, 
	@Fuelsurcharge,
	@IsPackedByStudent,
	@carrier_tracking_id,
    @ext_order_id,
    @qsp_order_id,
	@cvv2,
	@po_consultant_commission)

end
GO
