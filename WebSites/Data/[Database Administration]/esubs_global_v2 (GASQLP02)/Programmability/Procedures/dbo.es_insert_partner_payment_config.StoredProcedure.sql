USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_partner_payment_config]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_payment_config
CREATE PROCEDURE [dbo].[es_insert_partner_payment_config] @Partner_id int, @Profit_percentage int, @Payment_to int, @Email_template_id int, @First_email_template_id int, 
@Is_default bit, @Partner_payment_info_id int AS
begin

insert into Partner_payment_config(Partner_id, Profit_percentage, Payment_to, Email_template_id, First_email_template_id, Is_default, Partner_payment_info_id) values(@Partner_id, @Profit_percentage, @Payment_to, @Email_template_id, @First_email_template_id, @Is_default, @Partner_payment_info_id)

select @Partner_id = SCOPE_IDENTITY()

end
GO
