USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment_period]    Script Date: 02/14/2014 13:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment_period
CREATE PROCEDURE [dbo].[es_insert_payment_period] @Payment_period_id int OUTPUT, @Start_date datetime, @End_date datetime, @Create_date datetime AS
begin

insert into Payment_period(Start_date, End_date, Create_date) values(@Start_date, @End_date, @Create_date)

select @Payment_period_id = SCOPE_IDENTITY()

end
GO
