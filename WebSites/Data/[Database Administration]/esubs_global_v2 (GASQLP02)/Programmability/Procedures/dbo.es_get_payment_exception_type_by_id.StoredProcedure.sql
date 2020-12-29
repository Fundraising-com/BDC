USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_exception_type_by_id]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment_exception_type
CREATE   PROCEDURE [dbo].[es_get_payment_exception_type_by_id] @Payment_id int, @Exception_type_id int AS
begin

select Payment_id, Exception_type_id, Create_date, Validated_date, is_corrected from Payment_exception_type 
where Payment_id=@Payment_id and exception_type_id = Isnull(@exception_type_id, exception_type_id)

end
GO
