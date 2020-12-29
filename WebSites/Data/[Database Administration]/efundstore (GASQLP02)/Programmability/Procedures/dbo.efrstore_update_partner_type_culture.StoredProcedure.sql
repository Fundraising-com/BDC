USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_type_culture]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_type_culture
CREATE PROCEDURE [dbo].[efrstore_update_partner_type_culture] @Partner_type_id int, @Culture_code nvarchar(10), @Name varchar(255), @Create_date datetime AS
begin

update Partner_type_culture set Culture_code=@Culture_code, Name=@Name, Create_date=@Create_date where Partner_type_id=@Partner_type_id

end
GO
