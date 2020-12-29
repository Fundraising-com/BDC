USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_package]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_package
CREATE PROCEDURE [dbo].[efrstore_update_partner_package] @Partner_id int, @Package_id tinyint AS
begin

update Partner_package set Package_id=@Package_id where Partner_id=@Partner_id

end
GO
