USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment_payment_status]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment_payment_status
CREATE PROCEDURE [dbo].[es_insert_payment_payment_status] @Payment_id int OUTPUT, @Payment_status_id int, @Create_date datetime AS
begin

insert into Payment_payment_status(Payment_id,Payment_status_id, Create_date) values(@Payment_id, @Payment_status_id, @Create_date)



end
GO
