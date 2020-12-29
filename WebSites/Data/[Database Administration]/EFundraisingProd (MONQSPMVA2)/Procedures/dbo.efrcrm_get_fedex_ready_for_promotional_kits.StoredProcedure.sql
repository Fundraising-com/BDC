USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_fedex_ready_for_promotional_kits]    Script Date: 02/14/2014 13:04:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_fedex_ready_for_promotional_kits] AS
begin

select Fedex_id, 
Fedex_uid, 
Company_name, 
Contact_name, 
Address_line_1, 
Address_line_2, 
City, 
Province_state, 
Country, 
Zip_postal_code, 
Telephone, 
Extention, 
Tax_id_ssn, 
Fedex_account, 
Shipalert_email_address, 
Shipalert_email_message, 
Shipalert_email_option, 
Total_package_weight, 
Number_of_packages, 
Dimension_height, 
Dimension_width, 
Dimension_length, 
Sevice_level, 
Bill_freight_charges_to, 
Inter_part_description, 
Inter_unit_value, 
Inter_currency, 
Inter_unit_of_measure, 
Inter_quantity, 
Inter_country_of_manufacture, 
Inter_harmonized_code, 
Inter_part_number, 
Inter_marks_number, 
Inter_sku_upc_item, 
Inter_bill_duties_taxes_to, 
Inter_create_date, 
Inter_tracking_number, 
Inter_label_date_shipped_date, 
Inter_update_sale_date, 
Inter_shipping_quote, 
Cancelled, 
Cod_amount, 
Cod_payment_method 

from Fedex inner join Promotional_kit ON Promotional_kit.carrier_tracking_id = fedex.fedex_id
and fedex.inter_tracking_number is not null
and fedex.cancelled <> 1
and Promotional_kit.carrier_id = 2 -- make sure its a fedex tracking_id
and inter_update_sale_date is null

end
GO
