USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_web_form]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_web_form
CREATE PROCEDURE [dbo].[efrstore_update_partner_web_form] @Partner_id int, @Web_form_id int, @Recipient varchar(600), @Web_from_type_id int AS
begin

update Partner_web_form set Web_form_id=@Web_form_id, Recipient=@Recipient, Web_from_type_id=@Web_from_type_id where Partner_id=@Partner_id

end
GO
