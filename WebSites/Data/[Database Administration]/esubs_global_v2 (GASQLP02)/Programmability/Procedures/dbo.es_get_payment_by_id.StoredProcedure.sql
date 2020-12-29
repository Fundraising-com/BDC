USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_by_id]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment
CREATE PROCEDURE [dbo].[es_get_payment_by_id] @Payment_id int AS
begin

select Payment_id, Payment_type_id, Payment_info_id, Payment_period_id, Cheque_number, Cheque_date, Paid_amount, Name, Phone_number, Address_1, Address_2, City, Zip_code, Country_code, Subdivision_code, Create_date, Payment_batch_id, Is_validated, Is_processed from Payment where Payment_id=@Payment_id

end
GO
