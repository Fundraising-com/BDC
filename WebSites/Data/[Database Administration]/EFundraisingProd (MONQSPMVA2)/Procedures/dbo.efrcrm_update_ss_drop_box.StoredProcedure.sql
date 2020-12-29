USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_ss_drop_box]    Script Date: 02/14/2014 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for SS_Drop_Box
CREATE PROCEDURE [dbo].[efrcrm_update_ss_drop_box] @SS_Drop_Box_Id int, @SS_Drop_Box_Name varchar(50), @Display_Order int AS
begin

update SS_Drop_Box set SS_Drop_Box_Name=@SS_Drop_Box_Name, Display_Order=@Display_Order where SS_Drop_Box_Id=@SS_Drop_Box_Id

end
GO
