USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_title_by_id]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Title
CREATE PROCEDURE [dbo].[efrstore_get_title_by_id] @Title_id int AS
begin

select Title_id, Party_type_id, Title_desc from Title where Title_id=@Title_id

end
GO
