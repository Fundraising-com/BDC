USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_entry_form_by_id]    Script Date: 02/14/2014 13:04:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Entry_Form
CREATE PROCEDURE [dbo].[efrcrm_get_entry_form_by_id] @Entry_Form_ID int AS
begin

select Entry_Form_ID, Entry_Form_Desc, Master_Template, Content_Template, Web_Site_ID from Entry_Form where Entry_Form_ID=@Entry_Form_ID

end
GO
