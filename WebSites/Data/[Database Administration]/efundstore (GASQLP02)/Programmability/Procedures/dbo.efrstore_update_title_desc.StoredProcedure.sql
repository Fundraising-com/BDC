USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_title_desc]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Title_desc
CREATE PROCEDURE [dbo].[efrstore_update_title_desc] @Title_id tinyint, @Culture_code nvarchar(10), @Description varchar(100) AS
begin

update Title_desc set Culture_code=@Culture_code, Description=@Description where Title_id=@Title_id

end
GO
