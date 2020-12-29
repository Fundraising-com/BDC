USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sc_section]    Script Date: 02/14/2014 13:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for SC_SECTION
CREATE PROCEDURE [dbo].[efrcrm_update_sc_section] @Section_Id int, @Section_Title varchar(100), @Section_Image varchar(200), @Section_Text text, @Section_Template varchar(200), @Section_Sub_Title varchar(100) AS
begin

update SC_SECTION set Section_Title=@Section_Title, Section_Image=@Section_Image, Section_Text=@Section_Text, Section_Template=@Section_Template, Section_Sub_Title=@Section_Sub_Title where Section_Id=@Section_Id

end
GO
