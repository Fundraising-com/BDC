USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_question_param_targets]    Script Date: 02/14/2014 13:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Question_param_target
CREATE PROCEDURE [dbo].[efrstore_get_question_param_targets] AS
begin

select Question_id, Web_form_id, Parameter_target from Question_param_target

end
GO
