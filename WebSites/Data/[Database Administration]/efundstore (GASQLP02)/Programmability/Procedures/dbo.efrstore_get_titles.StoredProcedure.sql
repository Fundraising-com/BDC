USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_titles]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Title
CREATE PROCEDURE [dbo].[efrstore_get_titles] AS
begin

select Title_id, Party_type_id, Title_desc from Title

end
GO
