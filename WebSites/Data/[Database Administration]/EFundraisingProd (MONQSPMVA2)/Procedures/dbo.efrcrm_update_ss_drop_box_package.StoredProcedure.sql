USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_ss_drop_box_package]    Script Date: 02/14/2014 13:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for SS_Drop_Box_Package
CREATE PROCEDURE [dbo].[efrcrm_update_ss_drop_box_package] @SS_Drop_Box_Id int, @Package_Id int, @Display_Order smallint AS
begin

update SS_Drop_Box_Package set Package_Id=@Package_Id, Display_Order=@Display_Order where SS_Drop_Box_Id=@SS_Drop_Box_Id

end
GO
