USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_web_form_question]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Web_form_question
CREATE PROCEDURE [dbo].[efrstore_insert_web_form_question] @Question_id int OUTPUT, @Web_form_id int, @Required bit, @Question_order int, @Datestamp datetime AS
begin

insert into Web_form_question(Web_form_id, Required, Question_order, Datestamp) values(@Web_form_id, @Required, @Question_order, @Datestamp)

select @Question_id = SCOPE_IDENTITY()

end
GO
