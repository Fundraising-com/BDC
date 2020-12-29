USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_type_by_id]    Script Date: 02/14/2014 13:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment_type
CREATE PROCEDURE [dbo].[es_get_payment_type_by_id] @Payment_type_id int AS
begin

select Payment_type_id, Payment_type_name, Create_date from Payment_type where Payment_type_id=@Payment_type_id

end
GO
