USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_partner_payment_config]    Script Date: 02/14/2014 13:07:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_payment_config
CREATE PROCEDURE [dbo].[es_update_partner_payment_config] @Partner_id int, @Profit_percentage int, @Payment_to int, @Email_template_id int, 
@First_email_template_id int, @Is_default bit, @Partner_payment_info_id int AS
begin

update Partner_payment_config set Profit_percentage=@Profit_percentage, Payment_to=@Payment_to, Email_template_id=@Email_template_id, First_email_template_id=@First_email_template_id, Is_default=@Is_default, Partner_payment_info_id=@Partner_payment_info_id where Partner_id=@Partner_id

end
GO
