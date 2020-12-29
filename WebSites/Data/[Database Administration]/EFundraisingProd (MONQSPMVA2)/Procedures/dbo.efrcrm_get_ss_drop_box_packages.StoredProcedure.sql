USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ss_drop_box_packages]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for SS_Drop_Box_Package
CREATE PROCEDURE [dbo].[efrcrm_get_ss_drop_box_packages] AS
begin

select SS_Drop_Box_Id, Package_Id, Display_Order from SS_Drop_Box_Package

end
GO
