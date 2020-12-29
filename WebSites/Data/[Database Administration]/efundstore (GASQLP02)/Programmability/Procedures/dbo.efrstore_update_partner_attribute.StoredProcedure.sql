USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_attribute]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_attribute
CREATE PROCEDURE [dbo].[efrstore_update_partner_attribute] @Partner_attribute_id int, @Name varchar(50), @Create_date datetime AS
begin

update Partner_attribute set Name=@Name, Create_date=@Create_date where Partner_attribute_id=@Partner_attribute_id

end
GO
