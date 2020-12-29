USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payments_without_exceptions]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment
CREATE PROCEDURE [dbo].[es_get_payments_without_exceptions] 
AS
BEGIN
SELECT p.Payment_id, p.Payment_type_id, p.Payment_info_id, p.Payment_period_id, p.Cheque_number, p.Cheque_date, p.Paid_amount, p.[Name], p.Phone_number, p.Address_1, p.Address_2, p.City, p.Zip_code, p.Country_code, p.Subdivision_code, p.Create_date
FROM Payment p 
INNER JOIN Payment_info pif 
	ON p.Payment_info_id = pif.Payment_info_id
INNER JOIN 
	(select pps.payment_id, pps.payment_status_id
	from payment_payment_status pps
	inner join (
		select payment_id, max(create_date) as create_date 
		from payment_payment_status 
		group by payment_id
		) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
	) pps
	ON pps.payment_id = p.payment_id
WHERE 
--pif.Active = 1  AND  
p.Payment_id NOT IN (SELECT Payment_id FROM Payment_exception_type WHERE Isnull(Is_Corrected, 0)=0)
END
GO
