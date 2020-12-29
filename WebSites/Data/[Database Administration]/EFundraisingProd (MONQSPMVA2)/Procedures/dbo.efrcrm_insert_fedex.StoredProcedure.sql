USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_fedex]    Script Date: 02/14/2014 13:06:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Fedex
CREATE PROCEDURE [dbo].[efrcrm_insert_fedex] @Fedex_id int OUTPUT, @Fedex_uid varchar(25), @Company_name varchar(35), @Contact_name varchar(35), @Address_line_1 varchar(35), @Address_line_2 varchar(35), @City varchar(35), @Province_state varchar(2), @Country varchar(2), @Zip_postal_code varchar(10), @Telephone varchar(10), @Extention varchar(5), @Tax_id_ssn varchar(15), @Fedex_account int, @Shipalert_email_address varchar(120), @Shipalert_email_message varchar(450), @Shipalert_email_option int, @Total_package_weight int, @Number_of_packages int, @Dimension_height int, @Dimension_width int, @Dimension_length int, @Sevice_level varchar(3), @Bill_freight_charges_to int, @Inter_part_description varchar(148), @Inter_unit_value bigint, @Inter_currency varchar(3), @Inter_unit_of_measure varchar(3), @Inter_quantity int, @Inter_country_of_manufacture varchar(2), @Inter_harmonized_code decimal(15,4), @Inter_part_number varchar(20), @Inter_marks_number varchar(15), @Inter_sku_upc_item varchar(15), @Inter_bill_duties_taxes_to int, @Inter_create_date datetime, @Inter_tracking_number varchar(127), @Inter_label_date_shipped_date datetime, @Inter_update_sale_date datetime, @Inter_shipping_quote decimal(15,4), @Cancelled smallint, @Cod_amount decimal(15,4), @Cod_payment_method smallint AS
begin

insert into Fedex(Fedex_uid, Company_name, Contact_name, Address_line_1, Address_line_2, City, Province_state, Country, Zip_postal_code, Telephone, Extention, Tax_id_ssn, Fedex_account, Shipalert_email_address, Shipalert_email_message, Shipalert_email_option, Total_package_weight, Number_of_packages, Dimension_height, Dimension_width, Dimension_length, Sevice_level, Bill_freight_charges_to, Inter_part_description, Inter_unit_value, Inter_currency, Inter_unit_of_measure, Inter_quantity, Inter_country_of_manufacture, Inter_harmonized_code, Inter_part_number, Inter_marks_number, Inter_sku_upc_item, Inter_bill_duties_taxes_to, Inter_create_date, Inter_tracking_number, Inter_label_date_shipped_date, Inter_update_sale_date, Inter_shipping_quote, Cancelled, Cod_amount, Cod_payment_method) values(@Fedex_uid, @Company_name, @Contact_name, @Address_line_1, @Address_line_2, @City, @Province_state, @Country, @Zip_postal_code, @Telephone, @Extention, @Tax_id_ssn, @Fedex_account, @Shipalert_email_address, @Shipalert_email_message, @Shipalert_email_option, @Total_package_weight, @Number_of_packages, @Dimension_height, @Dimension_width, @Dimension_length, @Sevice_level, @Bill_freight_charges_to, @Inter_part_description, @Inter_unit_value, @Inter_currency, @Inter_unit_of_measure, @Inter_quantity, @Inter_country_of_manufacture, @Inter_harmonized_code, @Inter_part_number, @Inter_marks_number, @Inter_sku_upc_item, @Inter_bill_duties_taxes_to, @Inter_create_date, @Inter_tracking_number, @Inter_label_date_shipped_date, @Inter_update_sale_date, @Inter_shipping_quote, @Cancelled, @Cod_amount, @Cod_payment_method)

select @Fedex_id = SCOPE_IDENTITY()

end
GO
