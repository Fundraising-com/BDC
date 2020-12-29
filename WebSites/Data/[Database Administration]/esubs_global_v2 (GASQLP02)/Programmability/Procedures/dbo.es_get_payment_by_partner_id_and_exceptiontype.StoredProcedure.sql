USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_by_partner_id_and_exceptiontype]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_payment_by_partner_id_and_exceptiontype] @Partner_id int, @ExceptionType int, @PaymentStatus int AS
begin

select 
	p.Payment_id
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
from 
Payment p
	inner join partner_payment_info ppi
		on (ppi.payment_info_id = p.payment_info_id and ppi.active = 1)
	inner join payment_exception_type pxt 
		on (p.payment_id = pxt.payment_id and pxt.exception_type_id = Isnull(@ExceptionType, pxt.exception_type_id))
	inner join (
		select pps.payment_id, pps.payment_status_id
		from payment_payment_status pps
		inner join (
			select payment_id, max(create_date) as create_date 
			from payment_payment_status 
			group by payment_id
			) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
		)  pps 
		on 
		(p.payment_id = pps.payment_id and pps.payment_status_id = Isnull(@PaymentStatus, pps.payment_status_id))
where ppi.Partner_id = @Partner_id

end
GO
