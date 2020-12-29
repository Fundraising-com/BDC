USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_division]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Division
CREATE PROCEDURE [dbo].[efrstore_update_division] @Division_id int, @Name varchar(50), @Logo text, @Short_name varchar(10) AS
begin

update Division set Name=@Name, Logo=@Logo, Short_name=@Short_name where Division_id=@Division_id

end
GO
