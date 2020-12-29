USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_to_validated]    Script Date: 02/14/2014 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--update non processed and unvalidated payments with no exception

CREATE PROCEDURE [dbo].[es_update_payment_to_validated] @country_code as varchar(3) AS
begin

update Payment 
set is_validated = 1
where Payment_id in(

		select p.payment_id
		from payment p 
		  inner join(
				select pps.payment_id, pps.payment_status_id
				from payment_payment_status pps
				inner join (
					select payment_id, max(create_date) as create_date 
					from payment_payment_status 
					group by payment_id
					) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
					where payment_status_id = 1  --only want payment where last status is In Process
				)  pps
				on pps.payment_id = p.payment_id
		  left join     (select payment_id, min(cast(is_corrected as int)) is_corrected from payment_exception_type pet
						group by payment_id)  exc  --get the min value of is_corrected to see if one exception is not corrected
				on exc.payment_id = p.payment_id
		  
		where (exc.is_corrected > 0 or exc.is_corrected is null )
			   and p.country_code = @country_code and p.is_validated = 0
)
end
GO
