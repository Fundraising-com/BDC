USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_partner_contacts]    Script Date: 02/14/2014 13:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_contacts
CREATE PROCEDURE [dbo].[efrcrm_insert_partner_contacts] @Partner_contact_id int OUTPUT, @Partner_id int, @Language_id tinyint, @Section_name varchar(50), @Section_value varchar(500), @Display_order tinyint AS
begin

insert into Partner_contacts(Partner_id, Language_id, Section_name, Section_value, Display_order) values(@Partner_id, @Language_id, @Section_name, @Section_value, @Display_order)

select @Partner_contact_id = SCOPE_IDENTITY()

end
GO
