USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sc_section]    Script Date: 02/14/2014 13:07:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for SC_SECTION
CREATE PROCEDURE [dbo].[efrcrm_insert_sc_section] @Section_Id int OUTPUT, @Section_Title varchar(100), @Section_Image varchar(200), @Section_Text text, @Section_Template varchar(200), @Section_Sub_Title varchar(100) AS
begin

insert into SC_SECTION(Section_Title, Section_Image, Section_Text, Section_Template, Section_Sub_Title) values(@Section_Title, @Section_Image, @Section_Text, @Section_Template, @Section_Sub_Title)

select @Section_Id = SCOPE_IDENTITY()

end
GO
