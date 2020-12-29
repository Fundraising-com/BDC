USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_status_by_payment_id]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment_status
CREATE PROCEDURE [dbo].[es_get_payment_status_by_payment_id] 
@Payment_id INT

 AS

BEGIN

SELECT ps.Payment_status_id, ps.[Description] 
FROM 
	(
	select pps.payment_id, pps.payment_status_id
	from payment_payment_status pps
	inner join (
		select payment_id, max(create_date) as create_date 
		from payment_payment_status 
		group by payment_id
		) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
	) pps 
INNER JOIN Payment_status ps ON pps.payment_status_id = ps.payment_status_id
WHERE pps.Payment_id=@Payment_id

END
GO
