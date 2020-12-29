USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_question_param_target_by_id]    Script Date: 02/14/2014 13:05:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Question_param_target
CREATE PROCEDURE [dbo].[efrstore_get_question_param_target_by_id] @Question_id int AS
begin

select Question_id, Web_form_id, Parameter_target from Question_param_target where Question_id=@Question_id

end
GO
