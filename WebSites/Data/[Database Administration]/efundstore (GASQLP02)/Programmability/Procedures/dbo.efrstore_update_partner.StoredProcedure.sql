USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner
CREATE PROCEDURE [dbo].[efrstore_update_partner] @Partner_id int, @Partner_type_id int, @Partner_name varchar(50), @Has_collection_site bit, @Guid uniqueidentifier, @Create_date datetime AS
begin

update Partner set Partner_type_id=@Partner_type_id, Partner_name=@Partner_name, Has_collection_site=@Has_collection_site, Guid=@Guid, Create_date=@Create_date where Partner_id=@Partner_id

end
GO
