USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_web_forms]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_web_form
CREATE PROCEDURE [dbo].[efrstore_get_partner_web_forms] AS
begin

select Partner_id, Web_form_id, Recipient, Web_from_type_id from Partner_web_form

end
GO
