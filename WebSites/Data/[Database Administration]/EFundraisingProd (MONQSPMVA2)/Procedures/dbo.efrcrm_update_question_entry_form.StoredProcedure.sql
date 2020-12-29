USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_question_entry_form]    Script Date: 02/14/2014 13:08:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Question_Entry_Form
CREATE PROCEDURE [dbo].[efrcrm_update_question_entry_form] @Entry_Form_ID int, @Question_ID int, @Is_Required bit AS
begin

update Question_Entry_Form set Question_ID=@Question_ID, Is_Required=@Is_Required where Entry_Form_ID=@Entry_Form_ID

end
GO
