USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_questions_entry_form]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Questions_entry_form
CREATE PROCEDURE [dbo].[efrstore_update_questions_entry_form] @Question_id int, @Web_form_id int, @Required bit, @Question_order int, @Insert_table varchar(100), @Insert_column varchar(100), @Default_value varchar(200) AS
begin

update Questions_entry_form set Web_form_id=@Web_form_id, Required=@Required, Question_order=@Question_order, Insert_table=@Insert_table, Insert_column=@Insert_column, Default_value=@Default_value where Question_id=@Question_id

end
GO
