USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_web_form]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Web_form
CREATE PROCEDURE [dbo].[efrstore_update_web_form] @Web_form_id int, @Web_form_desc varchar(600), @Web_form_type_id int, @Lead_status_id int, @Stored_proc_to_call varchar(200), @Datestamp datetime AS
begin

update Web_form set Web_form_desc=@Web_form_desc, Web_form_type_id=@Web_form_type_id, Lead_status_id=@Lead_status_id, Stored_proc_to_call=@Stored_proc_to_call, Datestamp=@Datestamp where Web_form_id=@Web_form_id

end
GO
