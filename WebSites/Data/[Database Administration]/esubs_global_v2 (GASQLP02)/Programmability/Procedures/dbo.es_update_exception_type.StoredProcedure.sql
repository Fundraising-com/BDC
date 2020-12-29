USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_exception_type]    Script Date: 02/14/2014 13:07:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Exception_type
CREATE PROCEDURE [dbo].[es_update_exception_type] @Exception_type_id int, @Description varchar(50) AS
begin

update Exception_type set Description=@Description where Exception_type_id=@Exception_type_id

end
GO
