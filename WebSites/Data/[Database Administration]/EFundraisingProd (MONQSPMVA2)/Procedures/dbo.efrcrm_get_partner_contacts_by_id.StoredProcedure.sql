USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_partner_contacts_by_id]    Script Date: 02/14/2014 13:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner_contacts
CREATE PROCEDURE [dbo].[efrcrm_get_partner_contacts_by_id] @Partner_contact_id int AS
begin

select Partner_contact_id, Partner_id, Language_id, Section_name, Section_value, Display_order from Partner_contacts where Partner_contact_id=@Partner_contact_id

end
GO
