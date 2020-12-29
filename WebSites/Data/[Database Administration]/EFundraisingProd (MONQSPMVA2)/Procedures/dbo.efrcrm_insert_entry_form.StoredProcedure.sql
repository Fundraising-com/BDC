USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_entry_form]    Script Date: 02/14/2014 13:06:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Entry_Form
CREATE PROCEDURE [dbo].[efrcrm_insert_entry_form] @Entry_Form_ID int OUTPUT, @Entry_Form_Desc varchar(255), @Master_Template varchar(255), @Content_Template varchar(255), @Web_Site_ID int AS
begin

insert into Entry_Form(Entry_Form_Desc, Master_Template, Content_Template, Web_Site_ID) values(@Entry_Form_Desc, @Master_Template, @Content_Template, @Web_Site_ID)

select @Entry_Form_ID = SCOPE_IDENTITY()

end
GO
