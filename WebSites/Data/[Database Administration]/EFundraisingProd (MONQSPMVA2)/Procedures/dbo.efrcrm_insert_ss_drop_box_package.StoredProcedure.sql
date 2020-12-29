USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_ss_drop_box_package]    Script Date: 02/14/2014 13:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for SS_Drop_Box_Package
CREATE PROCEDURE [dbo].[efrcrm_insert_ss_drop_box_package] @SS_Drop_Box_Id int OUTPUT, @Package_Id int, @Display_Order smallint AS
begin

insert into SS_Drop_Box_Package(Package_Id, Display_Order) values(@Package_Id, @Display_Order)

select @SS_Drop_Box_Id = SCOPE_IDENTITY()

end
GO
