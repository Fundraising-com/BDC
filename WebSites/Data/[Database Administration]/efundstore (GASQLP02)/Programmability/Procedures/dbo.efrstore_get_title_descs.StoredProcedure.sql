USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_title_descs]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Title_desc
CREATE PROCEDURE [dbo].[efrstore_get_title_descs] AS
begin

select Title_id, Culture_code, Description from Title_desc

end
GO
