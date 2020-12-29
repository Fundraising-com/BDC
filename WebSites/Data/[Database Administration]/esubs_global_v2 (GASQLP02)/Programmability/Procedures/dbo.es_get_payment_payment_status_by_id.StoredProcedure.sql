USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_payment_status_by_id]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment_payment_status
CREATE PROCEDURE [dbo].[es_get_payment_payment_status_by_id] @Payment_id int AS
begin

select Payment_id, Payment_status_id, Create_date from Payment_payment_status where Payment_id=@Payment_id

end
GO
