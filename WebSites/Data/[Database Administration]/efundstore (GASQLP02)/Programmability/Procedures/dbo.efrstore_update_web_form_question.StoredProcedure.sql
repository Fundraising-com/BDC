USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_web_form_question]    Script Date: 02/14/2014 13:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Web_form_question
CREATE PROCEDURE [dbo].[efrstore_update_web_form_question] @Question_id int, @Web_form_id int, @Required bit, @Question_order int, @Datestamp datetime AS
begin

update Web_form_question set Web_form_id=@Web_form_id, Required=@Required, Question_order=@Question_order, Datestamp=@Datestamp where Question_id=@Question_id

end
GO
