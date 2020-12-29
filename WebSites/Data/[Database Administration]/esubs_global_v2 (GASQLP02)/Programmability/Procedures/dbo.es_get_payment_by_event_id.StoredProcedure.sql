USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_by_event_id]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment
CREATE PROCEDURE [dbo].[es_get_payment_by_event_id] @Event_id int AS
begin

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
from Payment p
	inner join payment_info pif 
		on p.payment_info_id = pif.payment_info_id
where pif.event_id = @Event_id

end
GO
