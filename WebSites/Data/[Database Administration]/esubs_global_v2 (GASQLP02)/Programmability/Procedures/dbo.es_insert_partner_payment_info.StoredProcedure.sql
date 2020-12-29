USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_partner_payment_info]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_payment_info
CREATE PROCEDURE [dbo].[es_insert_partner_payment_info] @Partner_payment_info_id int OUTPUT, @Partner_id int, @Payment_info_id int, @Active bit, @Create_date datetime AS
begin

insert into Partner_payment_info(Partner_id, Payment_info_id, Active, Create_date) values(@Partner_id, @Payment_info_id, @Active, @Create_date)

select @Partner_payment_info_id = SCOPE_IDENTITY()

end
GO
