USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_questions_entry_form]    Script Date: 02/14/2014 13:06:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Questions_entry_form
CREATE PROCEDURE [dbo].[efrstore_insert_questions_entry_form] @Question_id int OUTPUT, @Web_form_id int, @Required bit, @Question_order int, @Insert_table varchar(100), @Insert_column varchar(100), @Default_value varchar(200) AS
begin

insert into Questions_entry_form(Web_form_id, Required, Question_order, Insert_table, Insert_column, Default_value) values(@Web_form_id, @Required, @Question_order, @Insert_table, @Insert_column, @Default_value)

select @Question_id = SCOPE_IDENTITY()

end
GO
