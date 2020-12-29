USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_division_by_id]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Division
CREATE PROCEDURE [dbo].[efrstore_get_division_by_id] @Division_id int AS
begin

select Division_id, Name, Logo, Short_name from Division where Division_id=@Division_id

end
GO
