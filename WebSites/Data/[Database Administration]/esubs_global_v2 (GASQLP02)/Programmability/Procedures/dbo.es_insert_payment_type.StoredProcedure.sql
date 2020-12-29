USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment_type]    Script Date: 02/14/2014 13:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment_type
CREATE PROCEDURE [dbo].[es_insert_payment_type] @Payment_type_id int OUTPUT, @Payment_type_name varchar(50), @Create_date datetime AS
begin

insert into Payment_type(Payment_type_name, Create_date) values(@Payment_type_name, @Create_date)

select @Payment_type_id = SCOPE_IDENTITY()

end
GO
