USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_questions_entry_form_by_id]    Script Date: 02/14/2014 13:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Questions_entry_form
CREATE PROCEDURE [dbo].[efrstore_get_questions_entry_form_by_id] @Question_id int AS
begin

select Question_id, Web_form_id, Required, Question_order, Insert_table, Insert_column, Default_value from Questions_entry_form where Question_id=@Question_id

end
GO
