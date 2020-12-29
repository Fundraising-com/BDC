USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_website]    Script Date: 02/14/2014 13:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Website
CREATE PROCEDURE [dbo].[efrstore_insert_website] @Website_id int OUTPUT, @Partner_id int, @Webproject_id tinyint, @Website_dns varchar(50) AS
begin

insert into Website(Partner_id, Webproject_id, Website_dns) values(@Partner_id, @Webproject_id, @Website_dns)

select @Website_id = SCOPE_IDENTITY()

end
GO
