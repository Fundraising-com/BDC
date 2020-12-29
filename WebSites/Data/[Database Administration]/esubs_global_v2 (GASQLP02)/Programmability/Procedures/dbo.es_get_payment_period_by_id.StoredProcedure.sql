USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_period_by_id]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment_period
CREATE PROCEDURE [dbo].[es_get_payment_period_by_id] @Payment_period_id int AS
begin

select Payment_period_id, Start_date, End_date, Create_date from Payment_period where Payment_period_id=@Payment_period_id

end
GO
