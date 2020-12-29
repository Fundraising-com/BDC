USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_web_form_by_id]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner_web_form
CREATE PROCEDURE [dbo].[efrstore_get_partner_web_form_by_id] @Partner_id int AS
begin

select Partner_id, Web_form_id, Recipient, Web_from_type_id from Partner_web_form where Partner_id=@Partner_id

end
GO
