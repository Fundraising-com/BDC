USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_partner_contactss]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_contacts
CREATE PROCEDURE [dbo].[efrcrm_get_partner_contactss] AS
begin

select Partner_contact_id, Partner_id, Language_id, Section_name, Section_value, Display_order from Partner_contacts

end
GO
