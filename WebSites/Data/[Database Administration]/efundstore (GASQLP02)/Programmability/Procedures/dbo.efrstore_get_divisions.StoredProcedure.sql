USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_divisions]    Script Date: 02/14/2014 13:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Division
CREATE PROCEDURE [dbo].[efrstore_get_divisions] AS
begin

select Division_id, Name, Logo, Short_name from Division

end
GO
