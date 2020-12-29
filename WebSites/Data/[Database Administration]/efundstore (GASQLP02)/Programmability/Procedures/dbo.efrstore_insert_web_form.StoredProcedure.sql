USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_web_form]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Web_form
CREATE PROCEDURE [dbo].[efrstore_insert_web_form] @Web_form_id int OUTPUT, @Web_form_desc varchar(600), @Web_form_type_id int, @Lead_status_id int, @Stored_proc_to_call varchar(200), @Datestamp datetime AS
begin

insert into Web_form(Web_form_desc, Web_form_type_id, Lead_status_id, Stored_proc_to_call, Datestamp) values(@Web_form_desc, @Web_form_type_id, @Lead_status_id, @Stored_proc_to_call, @Datestamp)

select @Web_form_id = SCOPE_IDENTITY()

end
GO
