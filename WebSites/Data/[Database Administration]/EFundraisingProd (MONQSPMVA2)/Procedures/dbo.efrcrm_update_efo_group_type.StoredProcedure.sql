USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_group_type]    Script Date: 02/14/2014 13:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Group_Type
CREATE PROCEDURE [dbo].[efrcrm_update_efo_group_type] @Group_Type_ID int, @Description varchar(50) AS
begin

update EFO_Group_Type set Description=@Description where Group_Type_ID=@Group_Type_ID

end
GO
