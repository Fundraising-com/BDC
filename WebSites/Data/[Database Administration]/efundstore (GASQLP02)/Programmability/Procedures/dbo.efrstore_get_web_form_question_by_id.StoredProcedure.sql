USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_web_form_question_by_id]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Web_form_question
CREATE PROCEDURE [dbo].[efrstore_get_web_form_question_by_id] @Question_id int AS
begin

select Question_id, Web_form_id, Required, Question_order, Datestamp from Web_form_question where Question_id=@Question_id

end
GO
