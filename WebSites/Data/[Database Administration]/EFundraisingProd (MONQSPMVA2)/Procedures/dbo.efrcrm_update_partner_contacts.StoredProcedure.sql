USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_partner_contacts]    Script Date: 02/14/2014 13:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_contacts
CREATE PROCEDURE [dbo].[efrcrm_update_partner_contacts] @Partner_contact_id smallint, @Partner_id int, @Language_id tinyint, @Section_name varchar(50), @Section_value varchar(500), @Display_order tinyint AS
begin

update Partner_contacts set Partner_id=@Partner_id, Language_id=@Language_id, Section_name=@Section_name, Section_value=@Section_value, Display_order=@Display_order where Partner_contact_id=@Partner_contact_id

end
GO
