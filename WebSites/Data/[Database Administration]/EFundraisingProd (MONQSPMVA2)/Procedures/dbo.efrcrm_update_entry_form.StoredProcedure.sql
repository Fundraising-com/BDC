USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_entry_form]    Script Date: 02/14/2014 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Entry_Form
CREATE PROCEDURE [dbo].[efrcrm_update_entry_form] @Entry_Form_ID int, @Entry_Form_Desc varchar(255), @Master_Template varchar(255), @Content_Template varchar(255), @Web_Site_ID int AS
begin

update Entry_Form set Entry_Form_Desc=@Entry_Form_Desc, Master_Template=@Master_Template, Content_Template=@Content_Template, Web_Site_ID=@Web_Site_ID where Entry_Form_ID=@Entry_Form_ID

end
GO
