USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_type]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_type
CREATE PROCEDURE [dbo].[efrstore_update_partner_type] @Partner_type_id int, @Name varchar(50), @Create_date datetime AS
begin

update Partner_type set Name=@Name, Create_date=@Create_date where Partner_type_id=@Partner_type_id

end
GO
