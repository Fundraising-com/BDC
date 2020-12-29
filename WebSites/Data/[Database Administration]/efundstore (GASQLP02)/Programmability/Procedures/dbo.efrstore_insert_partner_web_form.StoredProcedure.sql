USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner_web_form]    Script Date: 02/14/2014 13:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_web_form
CREATE PROCEDURE [dbo].[efrstore_insert_partner_web_form] @Partner_id int OUTPUT, @Web_form_id int, @Recipient varchar(600), @Web_from_type_id int AS
begin

insert into Partner_web_form(Web_form_id, Recipient, Web_from_type_id) values(@Web_form_id, @Recipient, @Web_from_type_id)

select @Partner_id = SCOPE_IDENTITY()

end
GO
