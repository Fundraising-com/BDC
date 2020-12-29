USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner_contact]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_contact
CREATE PROCEDURE [dbo].[efrstore_insert_partner_contact] @Partner_contact_id int OUTPUT, @Partner_id int, @Culture_code nvarchar(10), @Section_name varchar(50), @Section_value varchar(500), @Display_order tinyint AS
begin

insert into Partner_contact(Partner_id, Culture_code, Section_name, Section_value, Display_order) values(@Partner_id, @Culture_code, @Section_name, @Section_value, @Display_order)

select @Partner_contact_id = SCOPE_IDENTITY()

end
GO
