USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_web_form_questions]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Web_form_question
CREATE PROCEDURE [dbo].[efrstore_get_web_form_questions] AS
begin

select Question_id, Web_form_id, Required, Question_order, Datestamp from Web_form_question

end
GO
