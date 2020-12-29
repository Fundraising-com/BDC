USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_title_desc_by_id]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Title_desc
CREATE PROCEDURE [dbo].[efrstore_get_title_desc_by_id] @Title_id int AS
begin

select Title_id, Culture_code, Description from Title_desc where Title_id=@Title_id

end
GO
