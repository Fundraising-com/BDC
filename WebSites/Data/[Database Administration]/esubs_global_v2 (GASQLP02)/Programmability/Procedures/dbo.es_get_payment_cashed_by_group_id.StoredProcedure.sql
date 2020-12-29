USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_cashed_by_group_id]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment
CREATE PROCEDURE [dbo].[es_get_payment_cashed_by_group_id]
 @Group_id INT

 AS

BEGIN

SELECT p.Payment_id
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
FROM Payment p INNER JOIN Payment_info pif ON p.payment_info_id = pif.payment_info_id
INNER JOIN 
	(
	select pps.payment_id, pps.payment_status_id
	from payment_payment_status pps
	inner join (
		select payment_id, max(create_date) as create_date 
		from payment_payment_status 
		group by payment_id
		) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
	) pps ON p.payment_id = pps.payment_id
INNER JOIN Payment_status ps ON pps.payment_status_id = ps.payment_status_id
WHERE pif.group_id = @Group_id AND ps.[description] ='Cashed';

END
GO
