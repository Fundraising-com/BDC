USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_contacts]    Script Date: 02/14/2014 13:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_contact
CREATE PROCEDURE [dbo].[efrstore_get_partner_contacts] AS
begin

select Partner_contact_id, Partner_id, Culture_code, Section_name, Section_value, Display_order from Partner_contact

end
GO
