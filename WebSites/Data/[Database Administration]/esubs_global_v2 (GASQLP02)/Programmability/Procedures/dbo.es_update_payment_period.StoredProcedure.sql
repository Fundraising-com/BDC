USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_period]    Script Date: 02/14/2014 13:07:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Payment_period
CREATE PROCEDURE [dbo].[es_update_payment_period] @Payment_period_id int, @Start_date datetime, @End_date datetime, @Create_date datetime AS
begin

update Payment_period set Start_date=@Start_date, End_date=@End_date, Create_date=@Create_date where Payment_period_id=@Payment_period_id

end
GO
