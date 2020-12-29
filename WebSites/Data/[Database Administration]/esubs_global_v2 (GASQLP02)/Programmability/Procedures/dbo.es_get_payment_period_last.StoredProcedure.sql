USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_period_last]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment_period
create PROCEDURE [dbo].[es_get_payment_period_last] AS
begin

select Payment_period_id, Start_date, End_date, Create_date from Payment_period where 
end_date  = (select max(end_date) from payment_period)

end
GO
