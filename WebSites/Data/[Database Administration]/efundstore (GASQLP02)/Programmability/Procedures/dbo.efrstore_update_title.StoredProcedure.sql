USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_title]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Title
CREATE PROCEDURE [dbo].[efrstore_update_title] @Title_id tinyint, @Party_type_id tinyint, @Title_desc varchar(50) AS
begin

update Title set Party_type_id=@Party_type_id, Title_desc=@Title_desc where Title_id=@Title_id

end
GO
