USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_web_forms]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Web_form
CREATE PROCEDURE [dbo].[efrstore_get_web_forms] AS
begin

select Web_form_id, Web_form_desc, Web_form_type_id, Lead_status_id, Stored_proc_to_call, Datestamp from Web_form

end
GO
