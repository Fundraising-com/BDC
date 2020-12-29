USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_contact]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_contact
CREATE PROCEDURE [dbo].[efrstore_update_partner_contact] @Partner_contact_id smallint, @Partner_id int, @Culture_code nvarchar(10), @Section_name varchar(50), @Section_value varchar(500), @Display_order tinyint AS
begin

update Partner_contact set Partner_id=@Partner_id, Culture_code=@Culture_code, Section_name=@Section_name, Section_value=@Section_value, Display_order=@Display_order where Partner_contact_id=@Partner_contact_id

end
GO
