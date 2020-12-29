USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payments_for_DPCC]    Script Date: 03/11/2015 16:15:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC [dbo].[es_get_payments_for_DPCC] '03/01/2015'
 
ALTER PROCEDURE [dbo].[es_get_payments_for_DPCC] @Up_To_This_Day Datetime As
-- DPCC: Double PostCard Check
begin

select p.Payment_id
	, p.Payment_type_id
	, p.Payment_info_id
	, p.Payment_period_id
	, p.Cheque_number
	, p.Cheque_date
	, p.Paid_amount
	, [dbo].[fn_RemoveExtraChars](p.[Name]) as Name
	, p.Phone_number
	, [dbo].[fn_RemoveExtraChars](p.Address_1) as Address_1
	, [dbo].[fn_RemoveExtraChars](p.Address_2) as Address_2
	, [dbo].[fn_RemoveExtraChars](p.City) as City
	, p.Zip_code
	, p.Country_code
	, p.Subdivision_code
	, p.Create_date 
	, p.payment_batch_id
	, p.is_validated
	, p.is_processed
from Payment p
	inner  join 
		(select pps.payment_id, pps.payment_status_id
		from payment_payment_status pps
		inner join (
			select payment_id, max(create_date) as create_date 
			from payment_payment_status 
			group by payment_id
			) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
		)  pps
		on pps.payment_id = p.payment_id
where pps.payment_status_id = 1 -- In Process
and p.Payment_id NOT IN (SELECT Payment_id FROM Payment_exception_type WHERE Isnull(Is_Corrected, 0)=0)
and p.Create_date <= @Up_To_This_Day
and p.is_validated = 1 and p.is_processed = 0 

Union

select p.Payment_id
	, p.Payment_type_id
	, p.Payment_info_id
	, p.Payment_period_id
	, p.Cheque_number
	, p.Cheque_date
	, p.Paid_amount
	, p.[Name]
	, p.Phone_number
	, p.Address_1
	, p.Address_2
	, p.City
	, p.Zip_code
	, p.Country_code
	, p.Subdivision_code
	, p.Create_date 
	, p.payment_batch_id
	, p.is_validated
	, p.is_processed
from Payment p
	inner  join 
		(select pps.payment_id, pps.payment_status_id
		from payment_payment_status pps
		inner join (
			select payment_id, max(create_date) as create_date 
			from payment_payment_status 
			group by payment_id
			) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
		)  pps
		on pps.payment_id = p.payment_id
	inner  join payment_exception_type pet
		on pet.payment_id = p.payment_id
where 
pps.payment_status_id = 1  
-- In Process 
and pet.Is_Corrected = 1 
and p.Create_date <= @Up_To_This_Day
and p.is_validated = 1 and p.is_processed = 0 
end
