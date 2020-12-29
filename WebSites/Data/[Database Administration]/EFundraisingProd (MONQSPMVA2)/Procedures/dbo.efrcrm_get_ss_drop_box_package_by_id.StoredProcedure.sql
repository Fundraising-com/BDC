USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ss_drop_box_package_by_id]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for SS_Drop_Box_Package
CREATE PROCEDURE [dbo].[efrcrm_get_ss_drop_box_package_by_id] @SS_Drop_Box_Id int AS
begin

select SS_Drop_Box_Id, Package_Id, Display_Order from SS_Drop_Box_Package where SS_Drop_Box_Id=@SS_Drop_Box_Id

end
GO
