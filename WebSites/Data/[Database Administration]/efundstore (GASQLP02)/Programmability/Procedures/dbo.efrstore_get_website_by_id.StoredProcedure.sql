USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_website_by_id]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Website
CREATE PROCEDURE [dbo].[efrstore_get_website_by_id] @Website_id int AS
begin

select Website_id, Partner_id, Webproject_id, Website_dns from Website where Website_id=@Website_id

end
GO
