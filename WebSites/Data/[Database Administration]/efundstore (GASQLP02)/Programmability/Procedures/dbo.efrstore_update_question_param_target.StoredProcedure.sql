USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_question_param_target]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Question_param_target
CREATE PROCEDURE [dbo].[efrstore_update_question_param_target] @Question_id int, @Web_form_id int, @Parameter_target varchar(75) AS
begin

update Question_param_target set Web_form_id=@Web_form_id, Parameter_target=@Parameter_target where Question_id=@Question_id

end
GO
