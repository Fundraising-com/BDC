USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_latest_payment_periods]    Script Date: 02/14/2014 13:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_period
CREATE PROCEDURE [dbo].[es_get_latest_payment_periods] AS
begin

select Payment_period_id, Start_date, End_date, Create_date from Payment_period
where start_date > '2009-12-31' or start_date = '2-1-2007'
order by end_date desc

end
GO
