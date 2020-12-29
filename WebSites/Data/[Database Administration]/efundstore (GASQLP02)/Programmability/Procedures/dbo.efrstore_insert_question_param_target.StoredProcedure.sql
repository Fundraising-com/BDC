USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_question_param_target]    Script Date: 02/14/2014 13:06:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Question_param_target
CREATE PROCEDURE [dbo].[efrstore_insert_question_param_target] @Question_id int OUTPUT, @Web_form_id int, @Parameter_target varchar(75) AS
begin

insert into Question_param_target(Web_form_id, Parameter_target) values(@Web_form_id, @Parameter_target)

select @Question_id = SCOPE_IDENTITY()

end
GO
