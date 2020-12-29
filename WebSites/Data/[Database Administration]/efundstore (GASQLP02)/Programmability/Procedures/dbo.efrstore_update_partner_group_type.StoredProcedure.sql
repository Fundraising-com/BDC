USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_group_type]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_group_type
CREATE PROCEDURE [dbo].[efrstore_update_partner_group_type] @Partner_group_type_id tinyint, @Description varchar(20) AS
begin

update Partner_group_type set Description=@Description where Partner_group_type_id=@Partner_group_type_id

end
GO
