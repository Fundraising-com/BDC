USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_question_by_id]    Script Date: 02/14/2014 13:05:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Question
CREATE PROCEDURE [dbo].[efrstore_get_question_by_id] @Question_id int AS
begin

select Question_id, Name, Description, Control_type_id, Field_name, Default_value, Min_lenght, Max_lenght, Nbr_value, Datestamp, Stored_proc_to_call, Answer_type, Field_value from Question where Question_id=@Question_id

end
GO
