USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_question_entry_forms]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Question_Entry_Form
CREATE PROCEDURE [dbo].[efrcrm_get_question_entry_forms] AS
begin

select Entry_Form_ID, Question_ID, Is_Required from Question_Entry_Form

end
GO
