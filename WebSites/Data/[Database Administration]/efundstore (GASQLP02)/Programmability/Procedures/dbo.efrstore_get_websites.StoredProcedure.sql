USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_websites]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Website
CREATE PROCEDURE [dbo].[efrstore_get_websites] AS
begin

select Website_id, Partner_id, Webproject_id, Website_dns from Website

end
GO
