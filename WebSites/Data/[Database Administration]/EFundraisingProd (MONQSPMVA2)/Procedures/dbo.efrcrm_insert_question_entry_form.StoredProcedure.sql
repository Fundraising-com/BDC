USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_question_entry_form]    Script Date: 02/14/2014 13:07:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Question_Entry_Form
CREATE PROCEDURE [dbo].[efrcrm_insert_question_entry_form] @Entry_Form_ID int OUTPUT, @Question_ID int, @Is_Required bit AS
begin

insert into Question_Entry_Form(Question_ID, Is_Required) values(@Question_ID, @Is_Required)

select @Entry_Form_ID = SCOPE_IDENTITY()

end
GO
