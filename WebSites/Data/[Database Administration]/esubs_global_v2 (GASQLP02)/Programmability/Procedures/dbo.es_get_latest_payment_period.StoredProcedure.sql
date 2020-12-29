USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_latest_payment_period]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_period
CREATE PROCEDURE [dbo].[es_get_latest_payment_period] AS
begin

select top 1 Payment_period_id, Start_date, End_date, Create_date from Payment_period
order by End_date desc

end
GO
