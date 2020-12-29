USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment_exception_type]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment_exception_type
CREATE PROCEDURE [dbo].[es_insert_payment_exception_type] @Payment_id int , @Exception_type_id int, @Create_date datetime, @Validated_date datetime AS
begin

insert into Payment_exception_type(Payment_id, Exception_type_id, Create_date, Validated_date) values(@Payment_id, @Exception_type_id, @Create_date, @Validated_date)

--select @Payment_id = SCOPE_IDENTITY()

end
GO
